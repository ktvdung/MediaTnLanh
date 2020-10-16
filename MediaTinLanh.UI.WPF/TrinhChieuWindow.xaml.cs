using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MediaTinLanh.Model;
using MediaTinLanh.Control;
using System.Diagnostics;
using System.Collections.ObjectModel;
using MediaTinLanh.UI.WPF.ViewModel;
using MediaTinLanh.UI.WPF.Useful;
using System.IO;
using MaterialDesignThemes.Wpf;

namespace MediaTinLanh.UI.WPF
{
    /// <summary>
    /// Interaction logic for TrinhChieuWindow.xaml
    /// </summary>
    public partial class TrinhChieuWindow : Window
    {
        public TrinhChieuWindow()
        {
            InitializeComponent();

            var danhSachLoaiThanhCa = Factory.LoaiThanhCaService.All();
            listBoxLoaiThanhCa.ItemsSource = danhSachLoaiThanhCa;
            listBoxLoaiThanhCa.SelectedItem = danhSachLoaiThanhCa.ToList()[0];

            btnThanCa.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximizeRestore_Click(object sender, RoutedEventArgs e)
        {
            if (btnMaximizeRestore.Content == FindResource("Restore"))
            {
                btnMaximizeRestore.Content = FindResource("Maximize");
                this.WindowState = WindowState.Normal;
            }
            else
            {
                btnMaximizeRestore.Content = FindResource("Restore");
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnThanhCa_Click(object sender, RoutedEventArgs e)
        {
            buttonStyle_Reset();
            currentContent_Close();

            Color darkerColor = (Color)this.Resources["ExtGrayDarkerColor"];
            SolidColorBrush backgroundButton = new SolidColorBrush(darkerColor);

            Button btn = sender as Button;
            if (btn.Background != backgroundButton)
            {
                buttonStyle_Click(btn);

                mainThanhCa.Visibility = Visibility.Visible;
                accountButtons.Visibility = Visibility.Visible;
            }
        }

        private void btnKinhThanh_Click(object sender, RoutedEventArgs e)
        {
            buttonStyle_Reset();
            currentContent_Close();

            Color darkerColor = (Color)this.Resources["ExtGrayDarkerColor"];
            SolidColorBrush backgroundButton = new SolidColorBrush(darkerColor);

            Button btn = sender as Button;
            if (btn.Background != backgroundButton)
            {
                buttonStyle_Click(btn);
            }
        }

        private void buttonStyle_Click(Button btnInput)
        {
            Color grayColor = (Color)this.Resources["ExtGrayColor"];
            SolidColorBrush backgroundButton = new SolidColorBrush(grayColor);

            Color darkerColor = (Color)this.Resources["ExtGrayDarkerColor"];
            SolidColorBrush backgroundDarkerButton = new SolidColorBrush(darkerColor);


            Button button = new Button();
            Style style = new Style(typeof(Button), btnInput.Style);

            //Background button normal
            Setter setterNormal = new Setter();
            setterNormal.Property = Button.BackgroundProperty;
            setterNormal.Value = backgroundDarkerButton;
            style.Setters.Add(setterNormal);

            //Template button
            Setter setterTemplate = new Setter();
            setterTemplate.Property = Button.TemplateProperty;

            ControlTemplate template = new ControlTemplate(typeof(Button));

            template.VisualTree = new FrameworkElementFactory(typeof(Border));

            template.VisualTree.SetValue(Border.BackgroundProperty, (SolidColorBrush)setterNormal.Value);
            template.VisualTree.SetValue(Border.BorderThicknessProperty, new Thickness(1, 0, 1, 0));
            template.VisualTree.SetValue(Border.BorderBrushProperty, backgroundDarkerButton);

            template.VisualTree.AppendChild(new FrameworkElementFactory(typeof(ContentPresenter)));

            template.VisualTree.FirstChild.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            template.VisualTree.FirstChild.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);

            setterTemplate.Value = template;
            style.Setters.Add(setterTemplate);

            button.Style = style;

            btnInput.Style = button.Style;
        }

        private void buttonStyle_Reset()
        {
            btnThanCa.Style = (Style)this.Resources["MenuButton"];
            btnKinhThanh.Style = (Style)this.Resources["MenuButton"];
            btnVideo.Style = (Style)this.Resources["MenuButton"];
            btnHinhAnh.Style = (Style)this.Resources["MenuButton"];
            btnDoKinhThanh.Style = (Style)this.Resources["MenuButton"];
            btnTruyenTranh.Style = (Style)this.Resources["MenuButton"];
            btnTranhToMau.Style = (Style)this.Resources["MenuButton"];
            btnThietKe.Style = (Style)this.Resources["MenuButton"];
        }

        private void currentContent_Close()
        {
            mainThanhCa.Visibility = Visibility.Hidden;
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            var dbThanhCa = (ThanhCaViewModel)this.Resources["dbForThanhCa"];

            LoaiThanhCaModel selectedLoaiThanhCa = (LoaiThanhCaModel)listBoxLoaiThanhCa.SelectedItem;
            dbThanhCa.Items = new ObservableCollection<ThanhCaModel>(Factory.ThanhCaService.Query("Loai = 1 AND Ten COLLATE UTF8CI LIKE '%" + txtTimBaiHat.Text + "%'").ToList());

            if (selectedLoaiThanhCa != null)
            {
                dbThanhCa.Items = new ObservableCollection<ThanhCaModel>(Factory.ThanhCaService.Query("Loai = @0 AND Ten COLLATE UTF8CI LIKE '%" + txtTimBaiHat.Text + "%'", paramaters: new object[] { selectedLoaiThanhCa.Id }).ToList());
            }
        }

        private void ckbLoaiThanhCa_Checked(object sender, RoutedEventArgs e)
        {
            LoaiThanhCaModel selectedLoaiThanhCa = (LoaiThanhCaModel)listBoxLoaiThanhCa.SelectedItem;

            var dbThanhCa = (ThanhCaViewModel)this.Resources["dbForThanhCa"];
            dbThanhCa.Items = new ObservableCollection<ThanhCaModel>(Factory.ThanhCaService.Query("Loai = @0", paramaters: new object[] { selectedLoaiThanhCa.Id }).ToList());
        }

        private void ckbLoaiThanhCa_Unchecked(object sender, RoutedEventArgs e)
        {
            var dbThanhCa = (ThanhCaViewModel)this.Resources["dbForThanhCa"];
            dbThanhCa.Items = new ObservableCollection<ThanhCaModel>();
        }

        private void btnTaiVe_Click(object sender, RoutedEventArgs e)
        {
            Button btnTaiVe = sender as Button;

            var inputfilepath = "";
            var filePathOnRemote = "";

            var dbThanhCa = (ThanhCaViewModel)this.Resources["dbForThanhCa"];
            var selectedThanhCa = dbThanhCa.SelectedItem;

            if (selectedThanhCa != null)
            {
                var media = new MediaModel();
                switch (btnTaiVe.Name)
                {
                    case "btnTaiVe":

                        //Hiện circle waiting
                        grdWaiting.Visibility = Visibility.Visible;
                        Task.Factory.StartNew(() =>
                        {
                            //YourAction();

                            foreach (var md in selectedThanhCa.Medias)
                            {
                                inputfilepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\" + md.Link.Substring(md.Link.LastIndexOf("."), md.Link.Length - md.Link.LastIndexOf(".")) + "\\" + md.Link.Remove(0, md.Link.IndexOf("/") + 1);
                                filePathOnRemote = md.Link;

                                //Nếu file không tồn tại thì cho phép tải xuống.
                                if (!Control_Files.CheckExit(inputfilepath))
                                {
                                    Control_FTP.Download_files(inputfilepath, filePathOnRemote);

                                    if (md != null)
                                    {
                                        md.TrangThai = true;
                                        md.LocalLink = inputfilepath;
                                    }
                                    Factory.MediaService.Update(md.Id, md);
                                }

                            }

                            var myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(3000));
                            myMessageQueue.Enqueue("Đã tải xong!");
                            mySnackbar.MessageQueue = myMessageQueue;

                        }).ContinueWith(Task =>
                        {
                            //Ẩn circle waiting
                            grdWaiting.Visibility = Visibility.Hidden;
                        }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                        break;
                    case "btnTaiPPTX169":
                        media = selectedThanhCa.Medias.Single(x => x.Loai == 4);

                        inputfilepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\" + media.Link.Substring(media.Link.LastIndexOf("."), media.Link.Length - media.Link.LastIndexOf(".")) + "\\" + media.Link.Remove(0, media.Link.IndexOf("/") + 1);
                        filePathOnRemote = media.Link;

                        //Hiện circle waiting
                        grdWaiting.Visibility = Visibility.Visible;
                        Task.Factory.StartNew(() =>
                        {
                            //YourAction();

                            //Nếu file không tồn tại thì cho phép tải xuống.
                            if (!Control_Files.CheckExit(inputfilepath))
                            {
                                Control_FTP.Download_files(inputfilepath, filePathOnRemote);

                                if (media != null)
                                {
                                    media.TrangThai = true;
                                    media.LocalLink = inputfilepath;
                                }
                                Factory.MediaService.Update(media.Id, media);
                            }

                            readFilePPTXAsync(media.LocalLink, "169");

                        }).ContinueWith(Task =>
                        {
                            //Ẩn circle waiting
                            grdWaiting.Visibility = Visibility.Hidden;
                        }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                        break;
                    case "btnTaiPPTX43":
                        media = selectedThanhCa.Medias.Single(x => x.Loai == 4);

                        inputfilepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\" + media.Link.Substring(media.Link.LastIndexOf("."), media.Link.Length - media.Link.LastIndexOf(".")) + "\\" + media.Link.Remove(0, media.Link.IndexOf("/") + 1);
                        filePathOnRemote = media.Link;

                        //Hiện circle waiting
                        grdWaiting.Visibility = Visibility.Visible;
                        Task.Factory.StartNew(() =>
                        {
                            //YourAction();

                            //Nếu file không tồn tại thì cho phép tải xuống.
                            if (!Control_Files.CheckExit(inputfilepath))
                            {
                                Control_FTP.Download_files(inputfilepath, filePathOnRemote);

                                if (media != null)
                                {
                                    media.TrangThai = true;
                                    media.LocalLink = inputfilepath;
                                }
                                Factory.MediaService.Update(media.Id, media);
                            }

                            readFilePPTXAsync(media.LocalLink);

                        }).ContinueWith(Task =>
                        {
                            //Ẩn circle waiting
                            grdWaiting.Visibility = Visibility.Hidden;
                        }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
                        
                        break;
                    case "btnTaiTXT":
                        media = selectedThanhCa.Medias.Single(x => x.Loai == 1);

                        inputfilepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\" + media.Link.Substring(media.Link.LastIndexOf("."), media.Link.Length - media.Link.LastIndexOf(".")) + "\\" + media.Link.Remove(0, media.Link.IndexOf("/") + 1);
                        //filePathOnRemote = media.Link;

                        //Hiện circle waiting
                        grdWaiting.Visibility = Visibility.Visible;
                        Task.Factory.StartNew(() =>
                        {
                            //YourAction();
                            if (!Control_Files.CheckExit(inputfilepath))
                            {
                                FileInfo fileInfo = new FileInfo(inputfilepath);
                                fileInfo.Directory.Create();

                                // Create a new file     
                                using (FileStream fs = File.Create(inputfilepath))
                                {
                                    var content = "";
                                    foreach (var loibaihat in selectedThanhCa.LoiBaiHats)
                                    {
                                        content += loibaihat.STT + ". " + loibaihat.NoiDung + Environment.NewLine;
                                        if (!string.IsNullOrEmpty(selectedThanhCa.DiepKhuc))
                                            content += "ĐK: " + selectedThanhCa.DiepKhuc + Environment.NewLine;
                                        content += Environment.NewLine;
                                    }
                                    // Add some text to file
                                    Byte[] contentByte = new UTF8Encoding(true).GetBytes(content);
                                    fs.Write(contentByte, 0, contentByte.Length);
                                }

                                media = selectedThanhCa.Medias.Single(x => x.Loai == 1);
                                if (media != null)
                                {
                                    media.TrangThai = true;
                                    media.LocalLink = inputfilepath;
                                }
                                Factory.MediaService.Update(media.Id, media);
                            }

                            var myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(3000));
                            myMessageQueue.Enqueue("Đã tải xong file TXT!");
                            mySnackbar.MessageQueue = myMessageQueue;

                            Process.Start(inputfilepath);

                        }).ContinueWith(Task =>
                        {
                            //Ẩn circle waiting
                            grdWaiting.Visibility = Visibility.Hidden;
                        }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                        break;
                    case "btnTaiPDF":
                        media = selectedThanhCa.Medias.Single(x => x.Loai == 5);

                        inputfilepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\" + media.Link.Substring(media.Link.LastIndexOf("."), media.Link.Length - media.Link.LastIndexOf(".")) + "\\" + media.Link.Remove(0, media.Link.IndexOf("/") + 1);
                        filePathOnRemote = media.Link;

                        //Hiện circle waiting
                        grdWaiting.Visibility = Visibility.Visible;
                        Task.Factory.StartNew(() =>
                        {
                            //YourAction();

                            //Nếu file không tồn tại thì cho phép tải xuống.
                            if (!Control_Files.CheckExit(inputfilepath))
                            {
                                Control_FTP.Download_files(inputfilepath, filePathOnRemote);

                                if (media != null)
                                {
                                    media.TrangThai = true;
                                    media.LocalLink = inputfilepath;
                                }
                                Factory.MediaService.Update(media.Id, media);

                                var myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(3000));
                                myMessageQueue.Enqueue("Đã tải xong file PDF!");
                                mySnackbar.MessageQueue = myMessageQueue;
                            }

                        }).ContinueWith(Task =>
                        {
                            //Ẩn circle waiting
                            grdWaiting.Visibility = Visibility.Hidden;
                        }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                        break;
                }
            }
            else
            {
                var myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(3000));
                myMessageQueue.Enqueue("Vui lòng chọn bài hát trước khi tải!");
                mySnackbar.MessageQueue = myMessageQueue;
            }


        }

        private void btnXem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnXem = sender as Button;

                var dbThanhCa = (ThanhCaViewModel)this.Resources["dbForThanhCa"];
                var selectedThanhCa = dbThanhCa.SelectedItem;
                var media = selectedThanhCa.Medias.Single(x => x.Loai == 4);

                readFilePPTXAsync(media.LocalLink);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void readFilePPTXAsync(string filePath = null, string slideSize = "OnScreen")
        {
            //Nếu file tồn tại thì cho phép xem.
            if (Control_Files.CheckExit(filePath))
            {
                var inputfilepath = filePath;

                Microsoft.Office.Interop.PowerPoint.Application pptApp = new Microsoft.Office.Interop.PowerPoint.Application();
                Microsoft.Office.Core.MsoTriState ofalse = Microsoft.Office.Core.MsoTriState.msoFalse;
                Microsoft.Office.Core.MsoTriState otrue = Microsoft.Office.Core.MsoTriState.msoTrue;
                pptApp.Visible = otrue;
                pptApp.Activate();
                Microsoft.Office.Interop.PowerPoint.Presentations ps = pptApp.Presentations;
                Microsoft.Office.Interop.PowerPoint.Presentation p = ps.Open(inputfilepath, ofalse, ofalse, otrue);
                if (slideSize == "OnScreen")
                {
                    p.PageSetup.SlideSize = Microsoft.Office.Interop.PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen;
                }
                else
                {
                    p.PageSetup.SlideSize = Microsoft.Office.Interop.PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen16x9;
                }
            }
        }

        private void listViewThanhCa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var dbThanhCa = (ThanhCaViewModel)this.Resources["dbForThanhCa"];
                var selectedThanhCa = dbThanhCa.SelectedItem;

                tblNoiDungBaiHat.TextAlignment = TextAlignment.Center;
                tblNoiDungBaiHat.Text = string.Empty;

                foreach (var cau in selectedThanhCa.LoiBaiHats)
                {
                    tblNoiDungBaiHat.Text += cau.STT + ". " + cau.NoiDung + Environment.NewLine;
                    if (!string.IsNullOrEmpty(selectedThanhCa.DiepKhuc))
                        tblNoiDungBaiHat.Text += "ĐK: " + selectedThanhCa.DiepKhuc + Environment.NewLine;
                    tblNoiDungBaiHat.Text += Environment.NewLine;
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnBaoLoi_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
