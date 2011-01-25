using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iServe.Models.Security {
	public interface IServeUser {
		int ID { get; }
		string Name { get; }
	}
}
