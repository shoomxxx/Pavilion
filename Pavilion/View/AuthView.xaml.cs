using Pavilion.ViewModel;
using System.Windows;

namespace Pavilion.View
{
    /// <summary>
    /// Логика взаимодействия для AuthView.xaml
    /// </summary>
    public partial class AuthView : Window
    {
        public AuthView()
        {
            InitializeComponent();
            DataContext = new AuthViewModel();
        }
    }
}
