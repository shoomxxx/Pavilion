using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pavilion.Model.Repository
{
    public class ShopCenterRepository : BaseRepository<shops_centers, int>
    {
        public override shops_centers Delete(shops_centers model)
        {
            try
            {
                if (model == null) return null;
                using (db_kingEntities db = new db_kingEntities())
                {
                    var entity = db.shops_centers.FirstOrDefault(x => x.shop_center_id == model.shop_center_id);
                    entity.status_id = 3;
                    db.SaveChanges();
                    return model;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public override IEnumerable<shops_centers> GetMany()
        {
            try
            {
                using (db_kingEntities db = new db_kingEntities())
                {
                    return db.shops_centers.Where(x => x.status_id != 3).ToList();
                }
            }
            catch
            {
                return Enumerable.Empty<shops_centers>();
            }
        }

        public string Update(shops_centers model)
        {
            try
            {
                if (model == null) return null;
                using (db_kingEntities db = new db_kingEntities())
                {
                    var entity = db.shops_centers.FirstOrDefault(x => x.shop_center_id == model.shop_center_id);

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
        public List<shops_centers> SearchSM(string str)
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
                    return db.shops_centers.Where(pavilion => pavilion.title.Contains(str)).OrderBy(pavilion => pavilion.floors_count).ToList();

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
