using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iServe.Models.dotNailsCommon {
	public class ModelFactory<TProcedures> : IModelFactory<TProcedures>
											 where TProcedures : DotNailsProcedures {
		private DotNailsDataContext _db = null;
		public DotNailsDataContext DataContext {
			get { return _db; }
			set { _db = value; }
		}
		private int? _userID = null;

		public ModelFactory(DotNailsDataContext dataContext) : this(dataContext, null) { }
		public ModelFactory(DotNailsDataContext dataContext, int? userID) {
			_db = dataContext;
			_userID = userID;
		}

		T IModelFactory<TProcedures>.New<T>() {
			T entity = new T();
			InitializeCreatedEntity(entity);
			return entity;
		}

		IEntity IModelFactory<TProcedures>.New(Type type) {
			IEntity entity = Activator.CreateInstance(type) as IEntity;
			if (entity != null) {
				InitializeCreatedEntity(entity);
			}
			return entity;
		}

		public TProcedures SPs {
			get { return (TProcedures)_db.Procedures;  }
		}

		private void InitializeCreatedEntity(IEntity entity) {
			entity.EntityState = EntityStateEnum.NotSet;
			entity.CurrentUserID = _userID;
			entity.SetDataContext(_db);
		}
	}
}
