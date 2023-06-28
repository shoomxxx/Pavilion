using Pavilion.Components.DataGrid;
using Pavilion.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Input;
using Pavilion.Model;
using Pavilion.Model.Repository;

namespace Pavilion.View
{
    /// <summary>
    /// Логика взаимодействия для ShopsCentersViewUser.xaml
    /// </summary>
    public partial class ShopsCentersViewUser : UserControl
    {
        private IEnumerable<DataGridColumnHidden> columns { get; set; }
        public ShopsCenterViewModel viewModel;
        static readonly ShopCenterRepository shopCenterRepository = new ShopCenterRepository();
        public ShopsCentersViewUser()
        {
            InitializeComponent();
            pnlUpdateData.Visibility = Visibility.Collapsed;
            viewModel = new ShopsCenterViewModel();
            DataContext = viewModel;

            List<DataGridColumnHidden> _columns = new List<DataGridColumnHidden>();
            foreach (var item in ShopsGrid.Columns)
            {
                var newColumn = new DataGridColumnHidden
                {
                    DataGridColumn = item
                };
                if (item.Visibility == Visibility.Visible) newColumn.IsVisible = true;
                else newColumn.IsVisible = false;
                _columns.Add(newColumn);
            }
            columns = _columns;
            Preferences.ItemsSource = columns;
            Preferences.Text = "Настройка столбцов";
        }
        private void Preferences_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        {
            var listColumn = new List<DataGridColumn>();
            foreach (DataGridColumnHidden column in Preferences.SelectedItems)
                listColumn.Add(column.DataGridColumn);

            foreach (var item in ShopsGrid.Columns)
            {
                if (listColumn.Contains(item))
                    item.Visibility = Visibility.Visible;
                else item.Visibility = Visibility.Hidden;
            }
            Preferences.Text = "Настройка столбцов";
        }

/*        private void btnTopMenuHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideTopMenu", btnTopMenuHide, btnTopMenuShow, pnlTopMenu);
        }

        private void btnTopMenuShow_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbShowTopMenu", btnTopMenuHide, btnTopMenuShow, pnlTopMenu);
        }
*/

        private void ShowHideMenu(string Storyboard, Button btnHide, Button btnShow, StackPanel pnl)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            sb.Begin(pnl);

            if (Storyboard.Contains("Show"))
            {
                btnHide.Visibility = System.Windows.Visibility.Visible;
                btnShow.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (Storyboard.Contains("Hide"))
            {
                btnHide.Visibility = System.Windows.Visibility.Hidden;
                btnShow.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            var obj = row.DataContext as shops_centers;
            viewModel.SelectOrderUpdate(obj);
            pnlUpdateData.Visibility = Visibility.Visible;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShopsGrid.ItemsSource = shopCenterRepository.SearchSM(SearchTxt.Text);
        }
    }
}

