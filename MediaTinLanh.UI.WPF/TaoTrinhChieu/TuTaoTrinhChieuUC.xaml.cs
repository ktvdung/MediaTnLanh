using MediaTinLanh.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;

namespace MediaTinLanh.UI.WPF.TaoTrinhChieu
{
    /// <summary>
    /// Interaction logic for TuTaoTrinhChieuUC.xaml
    /// </summary>
    public partial class TuTaoTrinhChieuUC : System.Windows.Controls.UserControl
    {
        TaoTrinhChieuViewModel viewModel = new TaoTrinhChieuViewModel();
        private List<ImageSource> slideImageSources = new List<ImageSource>();
        private List<ImageSource> thumbnailImageSource = new List<ImageSource>();
        private string templateFilePath = string.Empty;
        private string tempFilePath = string.Empty;
        Control_Presentation _controller;

        public TuTaoTrinhChieuUC()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            _controller = new Control_Presentation();
            templateFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\template.pptx";
            tempFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\temp\\temp.pptx";
            OpenTempFile();
            OkMan.Source = thumbnailImageSource[0];
        }

        private void btnTaiPPTX_Click(object sender, System.Windows.RoutedEventArgs e)
        {
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
            tempFilePath,
            viewModel.Slides.Select(slide => slide.NoiDung).ToArray(), new string[] { "Arial", "70", "Bold" }, img);
        }

        private void OpenTempFile()
        {
            slideImageSources.Clear();
            thumbnailImageSource.Clear();

            _controller.PptxFileToImages(
                templateFilePath,
                slideImageSources,
                thumbnailImageSource
                );
        }
    }
}
