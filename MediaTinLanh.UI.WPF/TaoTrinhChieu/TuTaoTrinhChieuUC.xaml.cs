using MediaTinLanh.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;

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
        int currentSlideIndex = 0;

        private string backgroundImagePath = string.Empty;
        private string templateFilePath = string.Empty;
        private string tempFilePath = string.Empty;
        private FileStream backgroundImage;
        private readonly Control_Presentation _controller;
        private DispatcherTimer timer = new DispatcherTimer();

        public TuTaoTrinhChieuUC()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            _controller = new Control_Presentation();
            templateFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Files\template.pptx");
            tempFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\temp\\temp.pptx";
            backgroundImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Skin\Images\trinh-chieu\", "bg.jpg");
            backgroundImage = new FileStream(backgroundImagePath, FileMode.Open);
            OpenTempFile(templateFilePath);
            CurrentSlide.Source = slideImageSources[currentSlideIndex];
            SlidesListView.ItemsSource = thumbnailImageSource;
            
            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Update();
        }

        private void btnTaiPPTX_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.NoiDungToSlide();
            if (viewModel.Slides.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "MS Powerpoint file (*.pptx)|*.pptx|Text file (*.txt)|*.txt";
                saveFileDialog.FileName = "Sample.pptx";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Control_Presentation.CreateFiles(
                        saveFileDialog.FileName,
                        viewModel.Slides.Select(slide => slide.NoiDung).ToArray(), 
                        new string[] { "Arial", "70", "Bold" }, 
                        backgroundImage);
                }
            }

        }

        private void SaveTempFile()
        {
            if (viewModel.Slides.Count != 0)
            {
                Task.Run(() =>
                {
                    var tempFolder = tempFilePath.Substring(0, tempFilePath.LastIndexOf("\\"));
                    var exists = Directory.Exists(tempFolder);
                    if (!exists)
                    {
                        Directory.CreateDirectory(tempFolder);
                    }

                    if (viewModel.Slides.Count != 0)
                    {
                        Control_Presentation.CreateFiles(tempFilePath,
                            viewModel.Slides.Select(slide => slide.NoiDung).ToArray(), new string[] { "Arial", "70", "Bold" }, backgroundImage);
                    }
                }).ConfigureAwait(false).GetAwaiter();
            }
        }

        private void OpenTempFile(string filePath)
        {
            slideImageSources.Clear();
            thumbnailImageSource.Clear();

            try
            {
                _controller.PptxFileToImages(filePath, slideImageSources, thumbnailImageSource);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Update()
        {
            viewModel.NoiDungToSlide();
            SaveTempFile();
            OpenTempFile(tempFilePath);
            CurrentSlide.Source = slideImageSources[currentSlideIndex];
            SlidesListView.ItemsSource = thumbnailImageSource;
        }
    }
}
