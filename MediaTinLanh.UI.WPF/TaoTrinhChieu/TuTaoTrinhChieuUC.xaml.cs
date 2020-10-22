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

        private void NoiDungNhap_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                var lastword = viewModel.NoiDungNhap.Substring(viewModel.NoiDungNhap.Length - 1);
                var loiBaiHatArray = viewModel.NoiDungNhap.Split(new[] { "\n\n" }, System.StringSplitOptions.None);
            }
        }
    }
}
