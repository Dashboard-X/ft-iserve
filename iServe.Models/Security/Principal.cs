using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iServe.Models;
using iServe.Models.dotNailsCommon;
using System.Security.Principal;

namespace iServe.Models.Security {
	public class Principal : IServePrincipal {
		public IServeUser CurrentUser { get; set; }
		
		#region IPrincipal Members
		// IPrincipal is incredibly limited (IIdentity doesn't even contain an ID), so we just satisfy it, then sidestep it in order to use a more useful interface
		private IIdentity _identity;
		public IIdentity Identity {
			get { return _identity; }
		}
		public bool IsInRole(string role) {
			return false;
		}
		#endregion IPrincipal Members

		#region Constructors
		public Principal(IIdentity identity) : this(identity, null) { }
		public Principal(IIdentity identity, IServeUser user) {
			_identity = identity;
			CurrentUser = user;
		}
		#endregion Constructors
		
	}
}
