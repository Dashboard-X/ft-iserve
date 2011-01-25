using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iServe.Models.dotNailsCommon;

namespace iServe.Models {
	public enum UserNeedStatusEnum {
		Interested = 1,
		Accepted = 2,
		Committed = 3,
		SubmitterDeclined = 4,
		HelperDeclined = 5
	}

	[Serializable()]
	public class UserNeedStatusEnumList {
		public static readonly DataItem Interested = new DataItem("Interested", "1");
		public static readonly DataItem Accepted = new DataItem("Accepted", "2");
		public static readonly DataItem Committed = new DataItem("Committed", "3");
		public static readonly DataItem SubmitterDeclined = new DataItem("Submitter declined", "4");
		public static readonly DataItem HelperDeclined = new DataItem("Helper declined", "5");
		public static readonly List<DataItem> DataSource = new List<DataItem> { Interested, Accepted, Committed, SubmitterDeclined, HelperDeclined };
	}
}
