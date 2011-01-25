using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iServe.Models.dotNailsCommon {
	public interface IEntity {
		void SetDataContext(System.Data.Linq.DataContext dataContext);
		int? CurrentUserID { get; set; }
		EntityStateEnum EntityState { get; set; }
		IEntity Save();
		IEntity Save(IEntity parent, System.Data.Linq.DataContext dataContext);
	}
}
