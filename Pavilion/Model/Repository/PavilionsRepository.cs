using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Pavilion.Model.Repository
{
    public class PavilionsRepository : BaseRepository<pavilions, string>
    {
        public override pavilions Delete(pavilions model)
        {
            try
            {
                if (model == null) return null;
                using (db_kingEntities db = new db_kingEntities())
                {
                    var entity = db.pavilions.FirstOrDefault(x => x.num_pavilion == model.num_pavilion);
                    if (entity.status == 3)
                    {
                        MessageBox.Show("Уже удален!");
                    }
                    else
                    {
                        entity.status = 3;
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

        public override IEnumerable<pavilions> GetMany()
        {
            try
            {
                using (db_kingEntities db = new db_kingEntities())
                {
                    return db.pavilions.Where(x => x.status != 3).ToList();
                }
            }
            catch
            {
                return Enumerable.Empty<pavilions>();
            }
        }

        public string Update(pavilions model)
        {
            try
            {
                if (model == null) return null;
                using (db_kingEntities db = new db_kingEntities())
                {
                    var entity = db.pavilions.FirstOrDefault(x => x.num_pavilion == model.num_pavilion);

                    /*entity.shop_center_id = model.shop_center_id;
                    entity.address = model.address;
                    entity.title = model.title;
                    entity.icon = model.icon;
                    entity.cost = model.cost;
                    entity.floors_count = model.floors_count;
                    entity.pavilions_count = model.pavilions_count;
                    entity.status_id = model.status_id;
                    entity.value_added_factor = model.value_added_factor;  ==  model.Adapt(entity)*/

                    model.Adapt(entity); // маппинг данных

                    db.SaveChanges();
                    return null;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateStatus(pavilions model)
        {
            try
            {
                if (model == null) return null;
                using (db_kingEntities db = new db_kingEntities())
                {
                    var entity = db.pavilions.FirstOrDefault(x => x.num_pavilion == model.num_pavilion);
                    entity.status = 6;
                    /*entity.shop_center_id = model.shop_center_id;
                    entity.address = model.address;
                    entity.title = model.title;
                    entity.icon = model.icon;
                    entity.cost = model.cost;
                    entity.floors_count = model.floors_count;
                    entity.pavilions_count = model.pavilions_count;
                    entity.status_id = model.status_id;
                    entity.value_added_factor = model.value_added_factor;  ==  model.Adapt(entity)*/

                    model.Adapt(entity); // маппинг данных

                    db.SaveChanges();
                    return null;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<pavilions> SearchPavilions(string str)
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
                    return db.pavilions.Where(pavilion => pavilion.num_pavilion.Contains(str)).OrderBy(pavilion => pavilion.shop_centr_id).ToList();

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

