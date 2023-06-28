using Pavilion.Model;
using Pavilion.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pavilion.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private static readonly AuthViewModel authViewModel = new AuthViewModel();
        private static readonly employers employers = new employers();

        private TabItem _selectedItemMain;
        public TabItem SelectesTabMain
        {
            get { return _selectedItemMain; }
            set { _selectedItemMain = value; OnPropertyChanged(nameof(SelectesTabMain)); }
        }

        public MainViewModel()
        {
            string postid1 = "yntiRS";
            string postid2 = "7SP9CV";
            string postid3 = "07i7Lb";

            Tabs = new ObservableCollection<TabItem>();
            using (var db = new db_kingEntities())
            {
                var item1 = db.employers.FirstOrDefault(x => x.login == authViewModel.Login);
                var item2 = db.employers.FirstOrDefault(x => x.login == authViewModel.Login);
                var item3 = db.employers.FirstOrDefault(x => x.login == authViewModel.Login);

                

                if (item1.password == postid1) // Менеджер С
                {
                    Tabs.Add(new TabItem { Header = "(ИЗМ)(МС) Торговые центры", Content = new ShopsCentersView() });
                    Tabs.Add(new TabItem { Header = "(ИЗМ)(МС) Павильоны", Content = new PavilionsView() });
                }
                if (item3.password == postid3) // Администратор
                {
                    Tabs.Add(new TabItem { Header = "(А) Торговые центры", Content = new ShopsCentersViewUser() });
                    Tabs.Add(new TabItem { Header = "(А) Павильоны", Content = new PavilionsViewUser() });
                    Tabs.Add(new TabItem { Header = "(ИЗМ)(А) Сотрудники", Content = new UserProfile() });
                }
/*                else if (item2.password == postid2) // Пользователь
                {
                    Tabs.Add(new TabItem { Header = "Торговые центры", Content = new ShopsCentersViewUser() });
                    Tabs.Add(new TabItem { Header = "Павильоны", Content = new PavilionsViewUser() });
                }
*/            }

            _selectedItemMain = Tabs.FirstOrDefault();
        }

        public ObservableCollection<TabItem> Tabs { get; set; }


        public string _title = $"{CurrentUser.surname} {CurrentUser.name} - {DateTime.Now}";
        public string Title { get { return _title; } set { _title = value; OnPropertyChanged(nameof(Title)); } }
    }

    public class TabItem
    {
        public string Header { get; set; }

        public object Content { get; set; }

    }
}
