using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iServe.Models {
	public class InvolvementCount {
		public int Interested;
		public int Accepted;
		public int Committed;
		public int SubmitterDeclined;
		public int HelperDeclined;

		public InvolvementCount(int interested, int accepted, int committed, int submitterDeclined, int helperDeclined) {
			Interested = interested;
			Accepted = accepted;
			Committed = committed;
			SubmitterDeclined = submitterDeclined;
			HelperDeclined = helperDeclined;
		}
	}
}
