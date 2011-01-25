using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iServe.Models.dotNailsCommon {
	public interface IModelFactory<TProcedures> where TProcedures : DotNailsProcedures {
		DotNailsDataContext DataContext { get; set; }
		T New<T>() where T : IEntity, new();
		IEntity New(Type t); // consider making internal and putting the dotNailsModelBinder in the iServe.Models.dotNailsCommon namespace
		TProcedures SPs { get; }
	}
}
