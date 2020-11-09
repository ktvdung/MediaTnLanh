using MediaTinLanh.Control;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MediaTinLanh.UI.WPF.TaoTrinhChieu
{
    /// <summary>
    /// Interaction logic for TuTaoTrinhChieuUC.xaml
    /// </summary>
    public partial class TuTaoTrinhChieuUC : System.Windows.Controls.UserControl
    {
        TaoTrinhChieuViewModel viewModel = new TaoTrinhChieuViewModel();
        public TuTaoTrinhChieuUC()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void btnTaiPPTX_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.NoiDungToSlide();

            if (viewModel.Slides.Count > 0)
            {
                FileStream img = new FileStream(
                    Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Skin\Images\trinh-chieu\", "bg.jpg"),
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

        private void SaveTempFile(object sender, System.Windows.RoutedEventArgs e)
        {
            FileStream img = new FileStream(
                    Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Skin\Images\trinh-chieu\", "bg.jpg"),
                    FileMode.Open);
            Control_Presentation.CreateFiles(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\temp.pptx",
            viewModel.Slides.Select(slide => slide.NoiDung).ToArray(), new string[] { "Arial", "70", "Bold" }, img);
        }
    }
}
