using Pavilion.View;
using Pavilion.ViewModel;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Pavilion.Commands
{
    public class AuthCommand : ICommand
    {
        private readonly AuthViewModel _viewModel;

        public AuthCommand(AuthViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private DelegateCommand _showWindow;
        public DelegateCommand ShowWindow => _showWindow ?? (_showWindow = new DelegateCommand(Show));

        IView _view;

        private void Show()
        {
            _view.Open();
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            try
            {
                return !string.IsNullOrEmpty(_viewModel.Login) &&
                    !string.IsNullOrEmpty(_viewModel.Password.ToString());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
        }

        public void Execute(object parameter)
        {
            var obj = _viewModel.AuthRepository.Auth(_viewModel.Login, _viewModel.Password);
            if (obj == null)
            {
                var win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                _view = new MainView();
                win.Close();
                _view.Open();
            }
            else
            {
                MessageBox.Show(obj);
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
