using System.Data.Entity;

namespace MC.Core.Dal
{
	public abstract class DaoBase<TEntity> where TEntity : class
	{
        private MCEntities model;

        public MCEntities Model
        {
            get
            {
                if (model == null)
                {
                    model = new ModelManager().Model;
                }
                return model;
            }
        }

        public void Add(TEntity entity)
        {
            EntitySet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            EntitySet.Remove(entity);
        }

		public void SaveChanges()
		{
			Model.SaveChanges();
		}

        public void Detach(object entity)
        {
            Model.Entry(entity).State = System.Data.EntityState.Detached;
        }
    
        protected DbSet<TEntity> EntitySet
        {
            get
            {
                return Model.Set<TEntity>();
            }
        }
    }
}
