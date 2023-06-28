using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Pavilion.Model.Repository
{
    public class UserRepository : BaseRepository<employers, int>
    {
        public override employers Delete(employers model)
        {
            try
            {
                if (model == null) return null;
                using (db_kingEntities db = new db_kingEntities())
                {
                    var entity = db.employers.FirstOrDefault(x => x.employer_id == model.employer_id);
                    if (entity.post_id == 4)
                    {
                        MessageBox.Show("Пользователь уже удален!");
                    }
                    else
                    {
                        entity.post_id = 4;
                    }
                    db.SaveChanges();
                    return model;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public override IEnumerable<employers> GetMany()
        {
            try
            {
                using (db_kingEntities db = new db_kingEntities())
                {
                    return db.employers.Where(x => x.employer_id != 3).ToList();
                }
            }
            catch
            {
                return Enumerable.Empty<employers>();
            }
        }

        public string Update(employers model)
        {
            try
            {
                if (model == null) return null;
                using (db_kingEntities db = new db_kingEntities())
                {
                    var entity = db.employers.FirstOrDefault(x => x.employer_id == model.employer_id);

                    /*entity.shop_center_id = model.shop_center_id;
                    entity.address = model.address;
                    entity.title = model.title;
                    entity.icon = model.icon;
                    entity.cost = model.cost;
                    entity.floors_count = model.floors_count;
                    entity.pavilions_count = model.pavilions_count;
                    entity.status_id = model.status_id;
                    entity.value_added_factor = model.value_added_factor;  ==  model.Adapt(entity)*/

                    model.Adapt(entity);

                    db.SaveChanges();
                    return null;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<employers> SearchSM(string str)
        {
            try
            {
                using (db_kingEntities db = new db_kingEntities())
                {
                    /*                switch (idSort)
                                    {
                                        case 1:
                                            return db.pavilions.Where(pavilion => pavilion.num_pavilion.Contains(str)).OrderBy(pavilion => pavilion.floor).ToList();
                                        default:
                    */
                    return db.employers.Where(pavilion => pavilion.name.Contains(str)).OrderBy(pavilion => pavilion.name).ToList();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return null;
            }
        }
    }

}
