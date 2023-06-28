using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pavilion.Model.Repository
{
    public abstract class BaseRepository<Entity, Type> where Entity : class
    {
        public virtual Entity Delete(Entity model)
        {
            try
            {
                if (model == null) return null;
                using (db_kingEntities db = new db_kingEntities())
                {
                    db.Set<Entity>().Remove(model);
                    db.SaveChanges();
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }

        public virtual IEnumerable<Entity> GetMany()
        {
            try
            {
                using (db_kingEntities db = new db_kingEntities())
                {
                    return db.Set<Entity>().ToList();
                }
            }
            catch
            {
                return Enumerable.Empty<Entity>();
            }
        }

        public virtual Entity GetOne(Type item)
        {
            try
            {
                if (item == null) return null;
                using (db_kingEntities db = new db_kingEntities())
                {
                    return db.Set<Entity>().Find(item);
                }
            }
            catch
            {
                return null;
            }
        }

        public virtual string Insert(Entity model)
        {
            try
            {
                if (model == null) return null;
                using (db_kingEntities db = new db_kingEntities())
                {
                    db.Set<Entity>().Add(model);
                    db.SaveChanges();
                    return null;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
