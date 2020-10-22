using System.Collections.Generic;
using System.ComponentModel;

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
        private List<SlideData> slides { get; set; }

        public string NoiDungNhap
        {
            get { return _noiDungNhap; }
            set
            {
                _noiDungNhap = value;
                OnPropertyChanged(nameof(NoiDungNhap));
            }
        }

        public List<SlideData> Slides
        {
            get { return slides; }
            set
            {
                slides = value;
                OnPropertyChanged(nameof(Slides));
            }
        }

        public void NoiDungToSlide()
        {
            string[] stringSlits = _noiDungNhap.Split(new[] { "\n\n" }, System.StringSplitOptions.None);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
