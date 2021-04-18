using AutoMapper;
using MaterialDesignThemes.Wpf;
using MediaTinLanh.Control;
using MediaTinLanh.Data;
using MediaTinLanh.Model;
using MediaTinLanh.UI.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using dbContext = MediaTinLanh.Data.MediaTinLanhContext;

namespace MediaTinLanh.UI.WPF.Controls
{
    /// <summary>
    /// Interaction logic for ucThuVienBaiHat.xaml
    /// </summary>
    public partial class ucThuVienBaiHat : MDTLBaseUserControl
    {
        public ucThuVienBaiHat()
        {
            InitializeComponent();

            var danhSachLoaiThanhCa = Mapper.Map<IEnumerable<LoaiThanhCa>, IEnumerable<LoaiThanhCaModel>>(dbContext.LoaiThanhCas.All());
            listBoxLoaiThanhCa.ItemsSource = danhSachLoaiThanhCa;
            listBoxLoaiThanhCa.SelectedItem = danhSachLoaiThanhCa.ToList()[0];
        }
        public ucThuVienBaiHat(bool initial) : base(initial)
        {
            InitializeComponent();

            var danhSachLoaiThanhCa = Mapper.Map<IEnumerable<LoaiThanhCa>, IEnumerable<LoaiThanhCaModel>>(dbContext.LoaiThanhCas.All());
            listBoxLoaiThanhCa.ItemsSource = danhSachLoaiThanhCa;
            listBoxLoaiThanhCa.SelectedItem = danhSachLoaiThanhCa.ToList()[0];
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            var dbThanhCa = (ThanhCaViewModel)this.Resources["dbForThanhCa"];

            LoaiThanhCaModel selectedLoaiThanhCa = (LoaiThanhCaModel)listBoxLoaiThanhCa.SelectedItem;
            var listThanhCaModel = Mapper.Map<IEnumerable<ThanhCa>, IEnumerable<ThanhCaModel>>(dbContext.ThanhCas.All(where: "Loai = 1 AND Ten COLLATE UTF8CI LIKE '%" + txtTimBaiHat.Text + "%'").ToList());
            if (selectedLoaiThanhCa != null)
            {
                listThanhCaModel = Mapper.Map<IEnumerable<ThanhCa>, IEnumerable<ThanhCaModel>>(dbContext.ThanhCas.All(where: "Loai = @0 AND Ten COLLATE UTF8CI LIKE '%" + txtTimBaiHat.Text + "%'", parms: new object[] { selectedLoaiThanhCa.Id }).ToList());
            }

            dbThanhCa.Items = new ObservableCollection<ThanhCaModel>(listThanhCaModel);
        }

        private void ckbLoaiThanhCa_Checked(object sender, RoutedEventArgs e)
        {
            LoaiThanhCaModel selectedLoaiThanhCa = (LoaiThanhCaModel)listBoxLoaiThanhCa.SelectedItem;

            var dbThanhCa = (ThanhCaViewModel)this.Resources["dbForThanhCa"];
            var listThanhCaModel = Mapper.Map<IEnumerable<ThanhCa>, IEnumerable<ThanhCaModel>>(dbContext.ThanhCas.All(where: "Loai = @0", parms: new object[] { selectedLoaiThanhCa.Id }).ToList());
            dbThanhCa.Items = new ObservableCollection<ThanhCaModel>(listThanhCaModel);
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

            try
            {
                if (selectedThanhCa != null)
                {
                    var mediaModel = new MediaModel();
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
                                        var media = Mapper.Map<MediaModel, Media>(md);
                                        dbContext.Medias.Update(media);
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
                            mediaModel = selectedThanhCa.Medias.Single(x => x.Loai == 4);

                            inputfilepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\" + mediaModel.Link.Substring(mediaModel.Link.LastIndexOf("."), mediaModel.Link.Length - mediaModel.Link.LastIndexOf(".")) + "\\" + mediaModel.Link.Remove(0, mediaModel.Link.IndexOf("/") + 1);
                            filePathOnRemote = mediaModel.Link;

                            //Hiện circle waiting
                            grdWaiting.Visibility = Visibility.Visible;
                            Task.Factory.StartNew(() =>
                            {
                                //YourAction();

                                //Nếu file không tồn tại thì cho phép tải xuống.
                                if (!Control_Files.CheckExit(inputfilepath))
                                {
                                    Control_FTP.Download_files(inputfilepath, filePathOnRemote);

                                    if (mediaModel != null)
                                    {
                                        mediaModel.TrangThai = true;
                                        mediaModel.LocalLink = inputfilepath;
                                    }
                                    var media = Mapper.Map<MediaModel, Media>(mediaModel);
                                    dbContext.Medias.Update(media);
                                }

                                readFilePPTXAsync(mediaModel.LocalLink, "169");

                            }).ContinueWith(Task =>
                            {
                                //Ẩn circle waiting
                                grdWaiting.Visibility = Visibility.Hidden;
                            }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                            break;
                        case "btnTaiPPTX43":
                            mediaModel = selectedThanhCa.Medias.Single(x => x.Loai == 4);

                            inputfilepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\" + mediaModel.Link.Substring(mediaModel.Link.LastIndexOf("."), mediaModel.Link.Length - mediaModel.Link.LastIndexOf(".")) + "\\" + mediaModel.Link.Remove(0, mediaModel.Link.IndexOf("/") + 1);
                            filePathOnRemote = mediaModel.Link;

                            //Hiện circle waiting
                            grdWaiting.Visibility = Visibility.Visible;
                            Task.Factory.StartNew(() =>
                            {
                                //YourAction();

                                //Nếu file không tồn tại thì cho phép tải xuống.
                                if (!Control_Files.CheckExit(inputfilepath))
                                {
                                    Control_FTP.Download_files(inputfilepath, filePathOnRemote);

                                    if (mediaModel != null)
                                    {
                                        mediaModel.TrangThai = true;
                                        mediaModel.LocalLink = inputfilepath;
                                    }
                                    var media = Mapper.Map<MediaModel, Media>(mediaModel);
                                    dbContext.Medias.Update(media);
                                }

                                readFilePPTXAsync(mediaModel.LocalLink);

                            }).ContinueWith(Task =>
                            {
                                //Ẩn circle waiting
                                grdWaiting.Visibility = Visibility.Hidden;
                            }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

                            break;
                        case "btnTaiTXT":
                            mediaModel = selectedThanhCa.Medias.Single(x => x.Loai == 1);

                            inputfilepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\" + mediaModel.Link.Substring(mediaModel.Link.LastIndexOf("."), mediaModel.Link.Length - mediaModel.Link.LastIndexOf(".")) + "\\" + mediaModel.Link.Remove(0, mediaModel.Link.IndexOf("/") + 1);
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

                                    mediaModel = selectedThanhCa.Medias.Single(x => x.Loai == 1);
                                    if (mediaModel != null)
                                    {
                                        mediaModel.TrangThai = true;
                                        mediaModel.LocalLink = inputfilepath;
                                    }
                                    var media = Mapper.Map<MediaModel, Media>(mediaModel);
                                    dbContext.Medias.Update(media);
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
                            mediaModel = selectedThanhCa.Medias.Single(x => x.Loai == 5);

                            inputfilepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\" + mediaModel.Link.Substring(mediaModel.Link.LastIndexOf("."), mediaModel.Link.Length - mediaModel.Link.LastIndexOf(".")) + "\\" + mediaModel.Link.Remove(0, mediaModel.Link.IndexOf("/") + 1);
                            filePathOnRemote = mediaModel.Link;

                            //Hiện circle waiting
                            grdWaiting.Visibility = Visibility.Visible;
                            Task.Factory.StartNew(() =>
                            {
                                //YourAction();

                                //Nếu file không tồn tại thì cho phép tải xuống.
                                if (!Control_Files.CheckExit(inputfilepath))
                                {
                                    Control_FTP.Download_files(inputfilepath, filePathOnRemote);

                                    if (mediaModel != null)
                                    {
                                        mediaModel.TrangThai = true;
                                        mediaModel.LocalLink = inputfilepath;
                                    }
                                    var media = Mapper.Map<MediaModel, Media>(mediaModel);
                                    dbContext.Medias.Update(media);

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
                    ShowMessage("Lỗi tải file", "Vui lòng chọn bài hát trước khi tải!");
                }
            }
            catch(Exception ex)
            {
                ShowMessage("Lỗi tải file", ex.Message);
                throw ex;
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


        private async void ShowMessage(string title, string message)
        {
            var sampleMessageDialog = new MessageViewModel
            {
                Message = message,
                Title = title
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }
    }
}
