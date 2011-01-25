using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iServe.Models.Security {
	public interface IServePrincipal : System.Security.Principal.IPrincipal {
		IServeUser CurrentUser { get; }

	}
}
