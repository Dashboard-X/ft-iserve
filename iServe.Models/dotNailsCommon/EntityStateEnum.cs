using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iServe.Models.dotNailsCommon {
	[Serializable]
	public enum EntityStateEnum {
		NotSet = 0,
		Added = 1,
		Deleted = 2,
		Modified = 3,
		UnModified = 4,
		Destroyed = 5,
		DefaultLinqToSql = 99
	}
}
