using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace MediaTinLanh.UI.Controls
{
    public class SlideData
    {
        public string NoiDung { get; set; }
    }

    public class TaoTrinhChieuViewModel : INotifyPropertyChanged
    {
        private string _tieuDe;
        private string _moTa;
        private string _noiDungNhap;
        private ObservableCollection<string> _slides;

        public TaoTrinhChieuViewModel()
        {
            _tieuDe = "Nhập tựa đề";
            _moTa = "Nhập thông tin Nhạc & lời, thơ, chuyển ngữ, năm sáng tác (nếu có)";
            _noiDungNhap = "Nhập nội dung";
            _slides = new ObservableCollection<string>();
        }

        public string TieuDe
        {
            get { return _tieuDe; }
            set
            {
                _tieuDe = value;
                OnPropertyChanged(nameof(TieuDe));
            }
        }
        public string MoTa
        {
            get { return _moTa; }
            set
            {
                _moTa = value;
                OnPropertyChanged(nameof(MoTa));
            }
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

        public ObservableCollection<string> Slides
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
                        _slides.Add(stringSlits[i]);
                    }
                }
            }
        }
    }
}
