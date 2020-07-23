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

namespace MediaTinLanh.UI.WPF
{
    /// <summary>
    /// Interaction logic for TrinhChieuWindow.xaml
    /// </summary>
    public partial class TrinhChieuWindow : Window
    {
        public class LoaiThanhCaModel
        {
            public int? Id { get; set; }
            public string Ten { get; set; }
        }
        public class ThanhCaModel
        {
            public int? Id { get; set; }
            public int? STT { get; set; }
            public string Ten { get; set; }
            public bool?  TrangThai { get; set; } // false - Chưa tải về, true - Đã tải về
            public LoaiThanhCaModel Loai { get; set; }
        }

        public TrinhChieuWindow()
        {
            InitializeComponent();
            var danhSachLoaiThanhCa = new List<LoaiThanhCaModel>();
            var danhSachThanhCa = new List<ThanhCaModel>();

            danhSachLoaiThanhCa.Add(new LoaiThanhCaModel() { Id = 1, Ten = "Thánh Ca"});
            danhSachLoaiThanhCa.Add(new LoaiThanhCaModel() { Id = 2, Ten = "Tôn Vinh Chúa Hằng Hữu"});
            danhSachLoaiThanhCa.Add(new LoaiThanhCaModel() { Id = 3, Ten = "Biệt Thánh Ca"});
            danhSachLoaiThanhCa.Add(new LoaiThanhCaModel() { Id = 4, Ten = "Kinh Thánh Đối Đáp"});

            danhSachThanhCa.Add(new ThanhCaModel() { Id = 1, STT = 1, Ten = "CUỐI XIN VUA THÁNH NGỰ LAI", TrangThai = false});
            danhSachThanhCa[0].Loai = danhSachLoaiThanhCa[0];
            danhSachThanhCa.Add(new ThanhCaModel() { Id = 2, STT = 2, Ten = "NGUYỆN TỤNG MỸ CHÚA LINH NĂNG", TrangThai = false });
            danhSachThanhCa[1].Loai = danhSachLoaiThanhCa[0];
            danhSachThanhCa.Add(new ThanhCaModel() { Id = 3, STT = 3, Ten = "NGỢI GIÊ-HÔ-VA THÁNH ĐẾ", TrangThai = true });
            danhSachThanhCa[2].Loai = danhSachLoaiThanhCa[0];
            danhSachThanhCa.Add(new ThanhCaModel() { Id = 4, STT = 4, Ten = "HA-LÊ-LU-GIA! VINH DANH NGÀI", TrangThai = false });
            danhSachThanhCa[3].Loai = danhSachLoaiThanhCa[0];
            danhSachThanhCa.Add(new ThanhCaModel() { Id = 5, STT = 5, Ten = "MUÔN DÂN TRÊN HOÀN CẦU NÊN CA XƯỚNG", TrangThai = true });
            danhSachThanhCa[4].Loai = danhSachLoaiThanhCa[0];
            danhSachThanhCa.Add(new ThanhCaModel() { Id = 6, STT = 6, Ten = "THÀNH TÂM TÔN VUA THÁNH", TrangThai = true });
            danhSachThanhCa[5].Loai = danhSachLoaiThanhCa[0];
            danhSachThanhCa.Add(new ThanhCaModel() { Id = 7, STT = 7, Ten = "CA CẢM TẠ", TrangThai = false });
            danhSachThanhCa[6].Loai = danhSachLoaiThanhCa[0];
            danhSachThanhCa.Add(new ThanhCaModel() { Id = 8, STT = 8, Ten = "NGỢI DANH GIÊ-XU RẤT OAI QUYỀN", TrangThai = false });
            danhSachThanhCa[7].Loai = danhSachLoaiThanhCa[0];

            listBoxLoaiThanhCa.ItemsSource = danhSachLoaiThanhCa;
            listViewThanhCa.ItemsSource = danhSachThanhCa;



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
            if(btn.Background != backgroundButton)
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
    }
}
