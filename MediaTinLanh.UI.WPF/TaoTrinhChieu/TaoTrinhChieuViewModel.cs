using MediaTinLanh.Control;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace MediaTinLanh.UI.WPF
{
    public class SlideData
    {
        public string NoiDung { get; set; }
        public int ViTri { get; set; }
    }

    public class TaoTrinhChieuViewModel : INotifyPropertyChanged
    {
        private string _noiDungNhap;
        private ImageSource _backgroundImage;
        private ObservableCollection<ImageSource> _slideImageSources;
        private ObservableCollection<SlideData> _slides;

        public TaoTrinhChieuViewModel()
        {
            _slides = new ObservableCollection<SlideData>();
            _slideImageSources = new ObservableCollection<ImageSource>();
            // Create new file
            // Change files
            // Save file when done
        }

        public string NoiDungNhap
        {
            get { return _noiDungNhap; }
            set
            {
                _noiDungNhap = value;
                OnPropertyChanged(nameof(NoiDungNhap));
            }
        }

        public ObservableCollection<SlideData> Slides
        {
            get { return _slides; }
            set
            {
                _slides = value;
                OnPropertyChanged(nameof(Slides));
            }
        }

        public ObservableCollection<ImageSource> SlideImageSources
        {
            get { return _slideImageSources; }
            set
            {
                _slideImageSources = value;
                OnPropertyChanged(nameof(SlideImageSources));
            }
        }

        public ImageSource BackgroundImage
        {
            get { return _backgroundImage; }
            set
            {
                _backgroundImage = value;
                OnPropertyChanged(nameof(BackgroundImage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NoiDungToSlide()
        {
            _slides.Clear();
            if (!String.IsNullOrWhiteSpace(_noiDungNhap))
            {
                string[] stringSlits = _noiDungNhap.Split(new[] { Environment.NewLine + Environment.NewLine }, System.StringSplitOptions.None);

                if (stringSlits.Count() != 0)
                {
                    for (int i = 0; i < stringSlits.Length; i++)
                    {
                        var slidedata = new SlideData
                        {
                            NoiDung = stringSlits[i],
                            ViTri = _noiDungNhap.IndexOf(stringSlits[i])
                    };
                        _slides.Add(slidedata);
                    }
                }
            }
        }

        private void OpenFile(string location)
        {
        }
    }
}
