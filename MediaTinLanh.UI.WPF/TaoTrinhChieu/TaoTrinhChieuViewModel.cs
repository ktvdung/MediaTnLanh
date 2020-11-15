using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
        private List<SlideData> _slides;

        public TaoTrinhChieuViewModel()
        {
            _slides = new List<SlideData>();
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

        public List<SlideData> Slides
        {
            get { return _slides; }
            set
            {
                _slides = value;
                OnPropertyChanged(nameof(Slides));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
