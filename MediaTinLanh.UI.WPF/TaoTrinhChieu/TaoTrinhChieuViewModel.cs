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
        private ObservableCollection<SlideData> _slides;

        public TaoTrinhChieuViewModel()
        {
            _slides = new ObservableCollection<SlideData>();
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
    }
}
