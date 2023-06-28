using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pavilion.Model.Repository
{
    public class AuthRepository : BaseRepository<employers, int>
    {
        public string Update(employers model)
        {
            try
            {
                if (model == null) return null;
                using (db_kingEntities db = new db_kingEntities())
                {
                    var entity = GetOne(model.employer_id);
                    entity = model;
                    db.SaveChanges();
                    return null;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Auth(string _login, string _password)
        {
            try
            {
                using (var db = new db_kingEntities())
                {
                    var item = db.employers.FirstOrDefault(x => x.password == _password && x.login == _login);
                    if (item != null)
                    {
                        CurrentUser.login = item.login;
                        CurrentUser.phone = item.phone;
                        CurrentUser.surname = item.surname;
                        CurrentUser.login = item.login;
                        CurrentUser.photo = item.photo;
                        CurrentUser.gender = item.gender;
                        return null;
                    }
                    else return "Пользователь не найден";
                }
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

    }
}
