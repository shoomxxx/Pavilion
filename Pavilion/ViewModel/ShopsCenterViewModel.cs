using Microsoft.Win32;
using Pavilion.Commands;
using Pavilion.Helper;
using Pavilion.Model;
using Pavilion.Model.Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Pavilion.ViewModel
{
    public class ShopsCenterViewModel : BaseViewModel
    {
        private ShopCenterRepository _repository;

        public ShopsCenterViewModel()
        {
            _repository = new ShopCenterRepository();
            RefreshList();

            UpdateShopCenterButtonCommand = new Command((s) => true, UpdateShopCenterCommand);

            BackToCollectionButtonCommand = new Command((s) => true, BackToCollectionnCommand);

            CreateShopCenterButtonCommand = new Command((s) => true, CreateShopCenterCommand);

            DeleteShopCenterButtonCommand = new Command((s) => true, DeleteShopCenterCommand);

            PhotoCommandButton = new Command((s) => true, PhotoCommand);
            PhotoUpdateCommandButton = new Command((s) => true, PhotoUpdateCommand);

        }

        #region List
        public void RefreshList()
        {
            Shops = new ObservableCollection<shops_centers>(_repository.GetMany());
        }

        private ObservableCollection<shops_centers> _shops;

        public ObservableCollection<shops_centers> Shops
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

        private shops_centers _shopCenterCreate { get; set; } = new shops_centers();
        public shops_centers ShopCenterCreate
        {
            get => _shopCenterCreate;
            set { _shopCenterCreate = value; OnPropertyChanged(nameof(ShopCenterCreate)); }
        }
        public ICommand CreateShopCenterButtonCommand { get; set; }
        public void CreateShopCenterCommand(object o)
        {
            if (PathToImage != null)
            {
                ShopCenterCreate.icon = ImgeConverter.ImageToBytes(PathToImage);
            }
            var item = _repository.Insert(ShopCenterCreate);
            if (item == null)
            {
                MessageBox.Show("Запись добавлена", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshList();
                ShopCenterCreate = new shops_centers();
                PathToImage = "";
                IsVisibleShopCenterPanel = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show(item, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Update

        private shops_centers _shopCenterUpdate { get; set; }
        public shops_centers ShopCenterUpdate
        {
            get => _shopCenterUpdate;
            set { _shopCenterUpdate = value; OnPropertyChanged(nameof(ShopCenterUpdate)); }
        }

        public ICommand UpdateShopCenterButtonCommand { get; set; }
        public void UpdateShopCenterCommand(object o)
        {
            var item = _repository.Update(ShopCenterUpdate);
            if (item == null)
            {
                MessageBox.Show("Запись обновлена", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                ShopCenterUpdate = _repository.GetOne(ShopCenterUpdate.shop_center_id);
                Shops = new ObservableCollection<shops_centers>(_repository.GetMany().Where(x => x.shop_center_id == ShopCenterUpdate.shop_center_id).ToList());
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

        internal void SelectOrderUpdate(shops_centers obj)
        {
            ShopCenterUpdate = obj;
            if (obj.icon != null)
                //PathToImageUpdate.StreamSource = ImgeConverter.BytesToImage(obj.icon);
            Shops = new ObservableCollection<shops_centers>(_repository.GetMany().Where(x => x.shop_center_id == ShopCenterUpdate.shop_center_id).ToList());
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

        #region Photo

        private string _pathToImage { get; set; }
        public string PathToImage
        {
            get => _pathToImage;
            set { _pathToImage = value; OnPropertyChanged(nameof(PathToImage)); }
        }

        private BitmapImage _pathToImageUpdate { get; set; } = null;
        public BitmapImage PathToImageUpdate
        {
            get => _pathToImageUpdate;
            set { _pathToImageUpdate = value; OnPropertyChanged(nameof(PathToImageUpdate)); }
        }

        public ICommand PhotoCommandButton { get; set; }
        public void PhotoCommand(object o)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                PathToImage = openFileDialog.FileName;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ICommand PhotoUpdateCommandButton { get; set; }
        public void PhotoUpdateCommand(object o)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                var buff = ImgeConverter.ImageToBytes(openFileDialog.FileName);
                PathToImageUpdate = ImgeConverter.BytesToImage(buff);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
