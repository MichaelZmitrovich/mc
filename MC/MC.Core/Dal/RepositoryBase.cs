using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Objects;

namespace MC.Core.Dal
{
	public abstract class RepositoryBase<TEntity> where TEntity : class
	{
        readonly private MCEntities model;
		private readonly ObjectSet<TEntity> objectSet;

        protected RepositoryBase(MCEntities model, ObjectSet<TEntity> objectSet)
		{
			this.model = model;
			this.objectSet = objectSet;
		}

        protected MCEntities Model
		{
			get
			{
				return model;
			}
		}

		protected ObjectSet<TEntity> ObjectSet
		{
			get
			{
				return objectSet;
			}
		}

		protected string EntitySetName
		{
			get
			{
				return objectSet.EntitySet.Name;
			}
		}

		public void Add(TEntity entity)
		{
			ObjectSet.AddObject(entity);
		}

		public void SaveChanges()
		{
			Model.SaveChanges();
		}

		public virtual void Delete(TEntity entity)
		{
			Model.DeleteObject(entity);
		}

		private DbCommand CreateSelectCommand(string spName, params object[] parameters)
		{
			EntityConnection entityConnection = (EntityConnection)Model.Connection;
			DbConnection storeConnection = entityConnection.StoreConnection;
			DbCommand storeCommand = storeConnection.CreateCommand();

			// setup command
			storeCommand.CommandText = spName;
			storeCommand.CommandType = CommandType.StoredProcedure;
			if (null != parameters)
			{
				storeCommand.Parameters.AddRange(parameters);
			}

			// pass through command timeout as appropriate
			if (Model.CommandTimeout.HasValue)
			{
				storeCommand.CommandTimeout = Model.CommandTimeout.Value;
			}

			return storeCommand;
		}

		protected IList<TEntity> Materialize(string spName, params object[] parameters)
		{
			DbCommand command = CreateSelectCommand(spName, parameters);
			IList<TEntity> result;
			using (command.Connection.CreateConnectionScope())
			{
				using (DbDataReader reader = command.ExecuteReader())
				{
					result = Materialize(reader);
				}
			}
			return result;
		}

		protected virtual IList<TEntity> Materialize(DbDataReader reader)
		{
			return Model.Translate<TEntity>(reader, EntitySetName, MergeOption.AppendOnly).ToList();
		}
	}
}
