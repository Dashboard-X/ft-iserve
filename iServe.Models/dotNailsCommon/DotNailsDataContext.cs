using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace iServe.Models.dotNailsCommon {
	public partial class DotNailsDataContext : System.Data.Linq.DataContext {
		protected static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		public virtual DotNailsProcedures Procedures {
			get { throw new NotImplementedException(); } 
		}

		protected virtual void OnCreated() { }
		
		public DotNailsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DotNailsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DotNailsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}

		public DotNailsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
	}
	
	public class ScalarResult<T> {
		private T _scalarValue;
		public T ScalarValue {
			get { return _scalarValue; }
			set { _scalarValue = value; }
		}
	}
	
}
