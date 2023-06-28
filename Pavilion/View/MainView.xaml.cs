using Pavilion.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace Pavilion.View
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window, IView
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        public bool? Open()
        {
            return this.ShowDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            e.Cancel = true;
            Application.Current.Shutdown();
        }
    }
}
