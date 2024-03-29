﻿using MediaTinLanh.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        private readonly BackgroundWorker displayBackroundWorker = new BackgroundWorker();

        private string backgroundImagePath = string.Empty;
        private readonly string templateFilePath = string.Empty;
        private readonly string tempPptxName = string.Empty;
        private readonly string tempFolderPath = string.Empty;
        
        private readonly string[] FORMAT = new string[] { "Arial", "70", "Bold" };
        
        List<ImageSource> slideImageSources;
        List<ImageSource> thumbnailImageSource;
        int currentSlideNumber = 0;
        FileStream img = null;

        public TuTaoTrinhChieuUC()
        {
            InitializeComponent();
            
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
