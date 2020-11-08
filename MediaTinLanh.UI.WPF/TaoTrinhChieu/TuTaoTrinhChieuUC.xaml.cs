using MediaTinLanh.Control;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace MediaTinLanh.UI.WPF.TaoTrinhChieu
{
    /// <summary>
    /// Interaction logic for TuTaoTrinhChieuUC.xaml
    /// </summary>
    public partial class TuTaoTrinhChieuUC : System.Windows.Controls.UserControl
    {
        TaoTrinhChieuViewModel viewModel = new TaoTrinhChieuViewModel();
        ObservableCollection<string> SlidesData { get; set; }
        string backgroundImg { get; set; }
        
        public TuTaoTrinhChieuUC()
        {
            SlidesData = new ObservableCollection<string>() { "" };
            InitializeComponent();
            DataContext = viewModel;
            slideList.ItemsSource = SlidesData;
            backgroundImg = "/MediaTinLanh.UI.WPF;component/../../Images/bg.jpg";
        }

        private void btnTaiPPTX_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (viewModel.Slides.Count > 0)
            {
                FileStream img = new FileStream(
                    Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Images\", "bg.jpg"),
                    FileMode.Open);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "MS Powerpoint file (*.pptx)|*.pptx|Text file (*.txt)|*.txt";
                saveFileDialog.FileName = "Sample.pptx";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Control_Presentation.CreateFiles(
                    saveFileDialog.FileName,
                    viewModel.Slides.Select(slide => slide.NoiDung).ToArray(), new string[] { "Arial", "70", "Bold" }, img);
                }
            }

        }

        private void TxtboxNoiDung_KeyUp(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.NoiDungToSlide();
            SlidesData = new ObservableCollection<string>(viewModel.Slides.Select(slide => slide.NoiDung));
            slideList.ItemsSource = SlidesData;
        }
    }
}
