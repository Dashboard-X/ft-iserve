using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace iServe.Models.Account {
	public class MembershipProvider {
		public int MinPasswordLength { get; set; }

		public MembershipCreateStatus CreateUser(string userName, string password, string email) {
			return new MembershipCreateStatus();
		}

		public bool ChangePassword(string userName, string oldPassword, string newPassword) {
			return false;
		}

		public bool ValidateUser(string userName, string password) {
			throw new NotImplementedException();
		}
	}
}
