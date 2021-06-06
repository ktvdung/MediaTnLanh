using MediaTinLanh.Control;
using MediaTinLanh.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MediaTinLanh.UI.Controls
{
    /// <summary>
    /// Interaction logic for ucTabTuTaoTrinhChieu.xaml
    /// </summary>
    public partial class ucTabTuTaoTrinhChieu : UserControl
    {
        private System.Windows.Data.ListCollectionView Context { get; }
        private ImageViewModel ImageDataContext { get; set; }
        private ObservableCollection<ImageSource> slideImageSources = new ObservableCollection<ImageSource>();
        
        TaoTrinhChieuViewModel viewModel = new TaoTrinhChieuViewModel();
        int currentSlideIndex = 0;

        private readonly Control_Presentation _controller;

        private Uri backgroundImagePath = new Uri("pack://application:,,,/Resources/images/trinh-chieu/bg.jpg");
        private Uri templateFilePath = new Uri("pack://application:,,,/Files/template.pptx");
        private string tempFilePath = string.Empty;
        private Stream backgroundImage;

        public ucTabTuTaoTrinhChieu()
        {
            InitializeComponent();
            this.DataContext = viewModel;

            ImageDataContext = (ImageViewModel)this.Resources["ImageContext"];

            ImageDataContext.Images.Add(new BitmapImage(new Uri("pack://application:,,,/Resources/images/main/Layer-169.png")));
            ImageDataContext.Images.Add(new BitmapImage(new Uri("pack://application:,,,/Resources/images/main/Layer-43.png")));
            ImageDataContext.Images.Add(new BitmapImage(new Uri("pack://application:,,,/Resources/images/main/Layer-30.png")));

            ImageDataContext.SelectedImage = ImageDataContext.Images[0];
            _controller = new Control_Presentation();

            tempFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\temp\\temp.pptx";
            backgroundImage = Application.GetResourceStream(backgroundImagePath).Stream;
            OpenTempFile(Application.GetResourceStream(templateFilePath).Stream);
            CurrentSlide.Source = slideImageSources[currentSlideIndex];
            SlidesListView.ItemsSource = slideImageSources;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            txtTieuDe.GotFocus += RemovePlaceholderTextBox;
            txtTieuDe.LostFocus += AddPlaceholderTextBox;

            txtMoTa.GotFocus += RemovePlaceholderTextBox;
            txtMoTa.LostFocus += AddPlaceholderTextBox;

            txtNoiDung.GotFocus += RemovePlaceholderTextBox;
            txtNoiDung.LostFocus += AddPlaceholderTextBox;


        }

        public void RemovePlaceholderTextBox(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
            }
        }

        public void AddPlaceholderTextBox(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrWhiteSpace(textBox.Text))
                textBox.Text = textBox.Tag.ToString();
        }

        private void btnTaiPPTX_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.NoiDungToSlide();
            if (viewModel.Slides.Count > 0)
            {
                System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog.Filter = "MS Powerpoint file (*.pptx)|*.pptx|Text file (*.txt)|*.txt";
                saveFileDialog.FileName = "Sample.pptx";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaTinLanh\\";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Control_Presentation.CreateFiles(
                        saveFileDialog.FileName,
                        viewModel.Slides.Select(slide => slide.NoiDung).ToArray(),
                        new string[] { "Arial", "70", "Bold" },
                        backgroundImage);
                }
            }
            
            DataContext = viewModel;

            displayBackroundWorker.DoWork += worker_DoWork;
            displayBackroundWorker.RunWorkerCompleted += worker_RunWorkerCompleted;

            // Load background image
            templateFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Assets\template.pptx");
            tempFolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\temp");
            tempPptxName = "temp.pptx";
            backgroundImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Skin\Images\trinh-chieu\", "bg.jpg");
            img = new FileStream(backgroundImagePath, FileMode.Open);

            slideImageSources = new List<ImageSource>();
            thumbnailImageSource = new List<ImageSource>();

            InitializeNonUITasks();
            InitializeUITasks();
        }

        #region Helper methods
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeNonUITasks();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InitializeUITasks();
        }

        private void InitializeNonUITasks()
        {
            slideImageSources.Clear();
            thumbnailImageSource.Clear();

            if (!String.IsNullOrEmpty(this.viewModel.NoiDungNhap))
            {
                this.viewModel.NoiDungToSlide();
                if (this.viewModel.Slides.Count > 0)
                {
                    // TODO: thumbnails
                    Control_Presentation.ConvertContentToImages(
                        tempFolderPath,
                        tempPptxName,
                        this.viewModel.Slides.ToArray(), 
                        slideImageSources, 
                        thumbnailImageSource, 
                        FORMAT, 
                        img
                    );
                }
            }
            else
            {
                Control_Presentation.PptxFileToImages(templateFilePath, slideImageSources, thumbnailImageSource);
            }
        }

        private void InitializeUITasks()
        {
            this.SlideList.Children.Clear();

            foreach (ImageSource imgSource in thumbnailImageSource)
            {
                StackPanel stackpanel = new StackPanel();

                Border myBorder1 = new Border(); 
                myBorder1.Background = Brushes.SkyBlue;
                myBorder1.Margin = new Thickness(0, 0, 0, 30);
                myBorder1.BorderBrush = new SolidColorBrush(Color.FromRgb(210, 71, 38));
                myBorder1.BorderThickness = new Thickness(0.7f);

                Image imageControl = new Image();
                imageControl.Source = imgSource;
                myBorder1.Child = imageControl;

                stackpanel.Children.Add(myBorder1);

                SlideList.Children.Add(stackpanel);
            }

            //Initiating the first slide
            if (slideImageSources.Count > 0)
            {
                CurrentSlide.Source = slideImageSources[0];
                currentSlideNumber = 1;
            }

        }
        #endregion

        private void OpenTempFile(Stream fileStream)
        {
            slideImageSources.Clear();

            try
            {
                _controller.PptxFileToImages(fileStream, slideImageSources);
                SlidesListView.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void btnTaiPPTX_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "MS Powerpoint file (*.pptx)|*.pptx|Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() ==  System.Windows.Forms.DialogResult.OK)
            {
                Control_Presentation.CreateFiles(saveFileDialog.FileName,
                viewModel.Slides.ToArray(), FORMAT, img);
            }
        }

        private void TxtboxNoiDung_KeyUp(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Phải có filePath 
            //displayBackroundWorker.RunWorkerAsync();
        }

        private void btnUploadBaiHai_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            grdWaiting.Visibility = Visibility.Visible;
            
            Task.Factory.StartNew(() =>
            {
                InitializeNonUITasks();
            }).ContinueWith(Task =>
            {
                InitializeUITasks();
                //Ẩn circle waiting
                grdWaiting.Visibility = Visibility.Hidden;
            }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

    }
}
