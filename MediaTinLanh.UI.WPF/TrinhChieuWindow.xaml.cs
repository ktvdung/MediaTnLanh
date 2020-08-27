﻿using System;
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
            LoaiThanhCaModel selectedLoaiThanhCa = (LoaiThanhCaModel)listBoxLoaiThanhCa.SelectedItem;
            var danhSachThanhCa = Factory.ThanhCaService.Query("Loai = 1 AND Ten COLLATE UTF8CI LIKE '%" + txtTimBaiHat.Text + "%'");

            if (selectedLoaiThanhCa != null)
            {
                danhSachThanhCa = Factory.ThanhCaService.Query("Loai = @0 AND Ten COLLATE UTF8CI LIKE '%" + txtTimBaiHat.Text + "%'", paramaters: new object[] { selectedLoaiThanhCa.Id });
            }
            
            foreach (var thanhCa in danhSachThanhCa)
            {
                thanhCa.LoaiThanhCa = selectedLoaiThanhCa;
                thanhCa.LoiBaiHats = Factory.LoiBaiHatService.GetDSLoiBaiHatByThanhCa(thanhCa.Id).ToList();
                thanhCa.Media = Factory.MediaService.GetByThanhCa(thanhCa.Id).FirstOrDefault();
            }

            listViewThanhCa.ItemsSource = danhSachThanhCa;
        }

        private void ckbLoaiThanhCa_Checked(object sender, RoutedEventArgs e)
        {
            LoaiThanhCaModel selectedLoaiThanhCa = (LoaiThanhCaModel)listBoxLoaiThanhCa.SelectedItem;
            var danhSachThanhCa = Factory.ThanhCaService.Query("Loai = @0", paramaters: new object[] { selectedLoaiThanhCa.Id });
            foreach (var thanhCa in danhSachThanhCa)
            {
                thanhCa.LoaiThanhCa = selectedLoaiThanhCa;
                thanhCa.LoiBaiHats = Factory.LoiBaiHatService.GetDSLoiBaiHatByThanhCa(thanhCa.Id).ToList();
                thanhCa.Media = Factory.MediaService.GetByThanhCa(thanhCa.Id).FirstOrDefault();
            }

            listViewThanhCa.ItemsSource = danhSachThanhCa;
        }

        private void ckbLoaiThanhCa_Unchecked(object sender, RoutedEventArgs e)
        {
            listViewThanhCa.ItemsSource = Enumerable.Empty<ThanhCaModel>();
        }

        private void btnTaiVe_Click(object sender, RoutedEventArgs e)
        {
            Button btnTaiVe = sender as Button;
            Hyperlink hyperLink = btnTaiVe.Content as Hyperlink;

            var inputfilepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\" + hyperLink.NavigateUri.ToString().Substring(hyperLink.NavigateUri.ToString().LastIndexOf("."), hyperLink.NavigateUri.ToString().Length - hyperLink.NavigateUri.ToString().LastIndexOf(".")) + "\\" + hyperLink.NavigateUri.ToString().Remove(0, hyperLink.NavigateUri.ToString().IndexOf("/") + 1);
            
            //Nếu file tồn tại thì cho phép mở, ngược lại phải tải xuống.
            if (Control_Files.CheckExit(inputfilepath))
            {
                Microsoft.Office.Interop.PowerPoint.Application pptApp = new Microsoft.Office.Interop.PowerPoint.Application();
                Microsoft.Office.Core.MsoTriState ofalse = Microsoft.Office.Core.MsoTriState.msoFalse;
                Microsoft.Office.Core.MsoTriState otrue = Microsoft.Office.Core.MsoTriState.msoTrue;
                pptApp.Visible = otrue;
                pptApp.Activate();
                Microsoft.Office.Interop.PowerPoint.Presentations ps = pptApp.Presentations;
                Microsoft.Office.Interop.PowerPoint.Presentation p = ps.Open(inputfilepath, ofalse, ofalse, otrue);
                System.Diagnostics.Debug.Print(p.Windows.Count.ToString());
                MessageBox.Show(pptApp.ActiveWindow.Caption);
            }
            else 
            {
                var filePathOnRemote = hyperLink.NavigateUri.ToString();
                Control_Upload_files.Download_files(inputfilepath, filePathOnRemote);
                btnTaiVe.Name = "Xem";
                //MessageBox.Show("File have been save to: " + inputfilepath, "Download complete!");
            }
                
            
        }

        private void listViewThanhCa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var seletedThanhCa = (ThanhCaModel)listViewThanhCa.SelectedItem;
            tblNoiDungBaiHat.TextAlignment = TextAlignment.Center;
            tblNoiDungBaiHat.Text = string.Empty;

            foreach (var cau in seletedThanhCa.LoiBaiHats)
            {
                tblNoiDungBaiHat.Text += cau.STT + ". " + cau.NoiDung + Environment.NewLine;
                if(!string.IsNullOrEmpty(seletedThanhCa.DiepKhuc))
                    tblNoiDungBaiHat.Text += "ĐK: " + seletedThanhCa.DiepKhuc + Environment.NewLine;
                tblNoiDungBaiHat.Text += Environment.NewLine;
            }
        }
    }
}
