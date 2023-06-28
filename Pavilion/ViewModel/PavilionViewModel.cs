using Pavilion.Commands;
using Pavilion.Model.Repository;
using Pavilion.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;

namespace Pavilion.ViewModel
{
    public class PavilionViewModel : BaseViewModel
    {
        private PavilionsRepository _repository;

        public PavilionViewModel()
        {
            _repository = new PavilionsRepository();
            RefreshList();
           /* UpdateStatusButtonCommand = new Command((s) => true, UpdateStatusCommand);
*/
            UpdateShopCenterButtonCommand = new Command((s) => true, UpdateShopCenterCommand);

            BackToCollectionButtonCommand = new Command((s) => true, BackToCollectionnCommand);

            CreateShopCenterButtonCommand = new Command((s) => true, CreateShopCenterCommand);

            DeleteShopCenterButtonCommand = new Command((s) => true, DeleteShopCenterCommand);

        }

        #region List
        public void RefreshList()
        {
            Shops = new ObservableCollection<pavilions>(_repository.GetMany());
        }

        private ObservableCollection<pavilions> _shops;

        public ObservableCollection<pavilions> Shops
        {
            get { return _shops; }
            set { _shops = value; OnPropertyChanged(nameof(Shops)); }
        }

        public ICommand BackToCollectionButtonCommand { get; set; }
        public void BackToCollectionnCommand(object o)
        {
            IsVisibleShopCenterPanel = Visibility.Hidden;
            RefreshList();
        }
        #endregion

        #region Create

        private pavilions _shopCenterCreate { get; set; } = new pavilions();
        public pavilions ShopCenterCreate
        {
            get => _shopCenterCreate;
            set { _shopCenterCreate = value; OnPropertyChanged(nameof(ShopCenterCreate)); }
        }
        public ICommand CreateShopCenterButtonCommand { get; set; }
        public void CreateShopCenterCommand(object o)
        {
            /*            if (PathToImage != null)
                        {
                            ShopCenterCreate.icon = ImgeConverter.ImageToBytes(PathToImage);
                        } 
            */
            var item = _repository.Insert(ShopCenterCreate);
            if (item == null)
            {
                MessageBox.Show("Запись добавлена", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshList();
                ShopCenterCreate = new pavilions();
                //                PathToImage = "";
                IsVisibleShopCenterPanel = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show(item, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Update

        private pavilions _shopCenterUpdate { get; set; }
        public pavilions ShopCenterUpdate
        {
            get => _shopCenterUpdate;
            set { _shopCenterUpdate = value; OnPropertyChanged(nameof(ShopCenterUpdate)); }
        }

  /*      public ICommand UpdateStatusButtonCommand { get; set; }
        public void UpdateStatusCommand(object o)
        {
            var del = _repository.UpdateStatus(ShopCenterUpdate);
            if (del == null)
            {
                MessageBox.Show("Запись обновлена", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                ShopCenterUpdate = _repository.GetOne(ShopCenterUpdate.num_pavilion);
                Shops = new ObservableCollection<pavilions>(_repository.GetMany().Where(x => x.status == ShopCenterUpdate.status).ToList());
            }
            else
            {
                MessageBox.Show("Ошибка удаления", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            MessageBox.Show("Павильон забронирован успешно!");
        }
*/

        public ICommand UpdateShopCenterButtonCommand { get; set; }
        public void UpdateShopCenterCommand(object o)
        {
            var item = _repository.Update(ShopCenterUpdate);
            if (item == null)
            {
                MessageBox.Show("Запись обновлена", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                ShopCenterUpdate = _repository.GetOne(ShopCenterUpdate.num_pavilion);
                Shops = new ObservableCollection<pavilions>(_repository.GetMany().Where(x => x.num_pavilion == ShopCenterUpdate.num_pavilion).ToList());
            }
            else
            {
                MessageBox.Show(item, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Visibility _isVisibleShopCenterPanel = Visibility.Collapsed;

        public Visibility IsVisibleShopCenterPanel
        {
            get { return _isVisibleShopCenterPanel; }
            set { _isVisibleShopCenterPanel = value; OnPropertyChanged(nameof(IsVisibleShopCenterPanel)); }
        }

        internal void SelectOrderUpdate(pavilions obj)
        {
            ShopCenterUpdate = obj;
            Shops = new ObservableCollection<pavilions>(_repository.GetMany().Where(x => x.num_pavilion == ShopCenterUpdate.num_pavilion).ToList());
            IsVisibleShopCenterPanel = Visibility.Visible;
        }

        #endregion

        #region Delete

        public ICommand DeleteShopCenterButtonCommand { get; set; }
        public void DeleteShopCenterCommand(object o)
        {
            var sum = _repository.Delete(ShopCenterUpdate);
            if (sum != null)
            {
                MessageBox.Show("Запись удалена", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                IsVisibleShopCenterPanel = Visibility.Hidden;
                RefreshList();
            }
            else
            {
                MessageBox.Show("Ошибка удаления", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

    }
}
