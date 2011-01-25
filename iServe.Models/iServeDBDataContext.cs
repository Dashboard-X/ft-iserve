using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;

namespace iServe.Models {
	public partial class iServeDBDataContext {
		[Function(Name = "dbo.GetNeeds")]
		public ISingleResult<GetNeedsResult> GetNeeds(
																				[Parameter(DbType = "Int")] System.Nullable<int> churchID

																				, [Parameter(DbType = "Int")] System.Nullable<int> userID

																				, [Parameter(DbType = "Int")] System.Nullable<int> pageNumber

																				, [Parameter(DbType = "Int")] System.Nullable<int> recordsPerPage

																				, [Parameter(DbType = "VarChar(300)")] string orderBy

																				, [Parameter(DbType = "Bit")] System.Nullable<bool> asc

																				, [Parameter(DbType = "Int")] ref System.Nullable<int> rowCount
																				) {
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), churchID, userID, pageNumber, recordsPerPage, orderBy, asc, rowCount);
			churchID = ((System.Nullable<int>)(result.GetParameterValue(0)));
			userID = ((System.Nullable<int>)(result.GetParameterValue(1)));
			pageNumber = ((System.Nullable<int>)(result.GetParameterValue(2)));
			recordsPerPage = ((System.Nullable<int>)(result.GetParameterValue(3)));
			orderBy = ((string)(result.GetParameterValue(4)));
			asc = ((System.Nullable<bool>)(result.GetParameterValue(5)));
			rowCount = ((System.Nullable<int>)(result.GetParameterValue(6)));
			return ((ISingleResult<GetNeedsResult>)(result.ReturnValue));
		}
	}

	public partial class iServeDBProcedures {
		public GetNeedsResultList GetNeeds(System.Nullable<int> churchID, System.Nullable<int> userID, System.Nullable<int> pageNumber, System.Nullable<int> recordsPerPage, string orderBy, System.Nullable<bool> asc, ref System.Nullable<int> rowCount) {
			ISingleResult<GetNeedsResult> needs = _db.GetNeeds(churchID, userID, pageNumber, recordsPerPage, orderBy, asc, ref rowCount);
			return new GetNeedsResultList(needs.ToList(), pageNumber.Value, recordsPerPage.Value, rowCount.Value);
		}
	}

	public class GetNeedsResultList : IList<GetNeedsResult>, IPagedList<GetNeedsResult> {
		List<GetNeedsResult> source = null;

		#region IPagedList Members
		public int PageCount { get; private set; }
		public int TotalItemCount { get; private set; }
		public int PageIndex { get; private set; }
		public int PageNumber { get { return PageIndex; } }
		public int PageSize { get; private set; }
		public bool HasPreviousPage { get; private set; }
		public bool HasNextPage { get; private set; }
		public bool IsFirstPage { get; private set; }
		public bool IsLastPage { get; private set; }
		#endregion

		public GetNeedsResultList(List<GetNeedsResult> results, int index, int pageSize, int rowCount) {
			source = results;
			TotalItemCount = rowCount;
			PageSize = pageSize;
			PageIndex = index;
			if (TotalItemCount > 0)
				PageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
			else
				PageCount = 0;
			HasPreviousPage = (PageIndex > 1);
			HasNextPage = (PageIndex < (PageCount));
			IsFirstPage = (PageIndex <= 0);
			IsLastPage = (PageIndex >= (PageCount));

			//### argument checking
			if (index < 0)
				throw new ArgumentOutOfRangeException("PageIndex cannot be below 0.");
			if (pageSize < 1)
				throw new ArgumentOutOfRangeException("PageSize cannot be less than 1.");
		}

		#region IList<GetNeedsResult> Members

		public int IndexOf(GetNeedsResult item) {
			return source.IndexOf(item);
		}

		public void Insert(int index, GetNeedsResult item) {
			source.Insert(index, item);
		}

		public void RemoveAt(int index) {
			source.RemoveAt(index);
		}

		public GetNeedsResult this[int index] {
			get {
				return source[index];
			}
			set {
				source[index] = value;
			}
		}

		#endregion

		#region ICollection<GetNeedsResult> Members

		public void Add(GetNeedsResult item) {
			source.Add(item);
		}

		public void Clear() {
			source.Clear();
		}

		public bool Contains(GetNeedsResult item) {
			return source.Contains(item);
		}

		public void CopyTo(GetNeedsResult[] array, int arrayIndex) {
			source.CopyTo(array, arrayIndex);
		}

		public int Count {
			get { return source.Count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public bool Remove(GetNeedsResult item) {
			return source.Remove(item);
		}

		#endregion

		#region IEnumerable<GetNeedsResult> Members

		public IEnumerator<GetNeedsResult> GetEnumerator() {
			return source.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return source.GetEnumerator();
		}

		#endregion
	}


	public partial class GetNeedsResult {
		public GetNeedsResult() { }

		private int _ID = Int32.MinValue;
		[Column(Storage = "_ID", DbType = "int NOT NULL", CanBeNull = false)]
		public int ID {
			get { return _ID; }
			set { _ID = value; }
		}
		private int _ChurchID = Int32.MinValue;
		[Column(Storage = "_ChurchID", DbType = "int NOT NULL", CanBeNull = false)]
		public int ChurchID {
			get { return _ChurchID; }
			set { _ChurchID = value; }
		}
		private int _CategoryID = Int32.MinValue;
		[Column(Storage = "_CategoryID", DbType = "int NOT NULL", CanBeNull = false)]
		public int CategoryID {
			get { return _CategoryID; }
			set { _CategoryID = value; }
		}
		private int _SubmittedByID = Int32.MinValue;
		[Column(Storage = "_SubmittedByID", DbType = "int NOT NULL", CanBeNull = false)]
		public int SubmittedByID {
			get { return _SubmittedByID; }
			set { _SubmittedByID = value; }
		}
		private string _Name;
		[Column(Storage = "_Name", DbType = "nvarchar(100) NOT NULL", CanBeNull = false)]
		public string Name {
			get { return _Name; }
			set { _Name = value; }
		}
		private string _Description;
		[Column(Storage = "_Description", DbType = "nvarchar(3000)", CanBeNull = true)]
		public string Description {
			get { return _Description; }
			set { _Description = value; }
		}
		private System.Nullable<DateTime> _RequiredDate;
		[Column(Storage = "_RequiredDate", DbType = "datetime", CanBeNull = true)]
		public System.Nullable<DateTime> RequiredDate {
			get { return _RequiredDate; }
			set { _RequiredDate = value; }
		}
		private bool _IsRequiredOnDate;
		[Column(Storage = "_IsRequiredOnDate", DbType = "bit NOT NULL", CanBeNull = false)]
		public bool IsRequiredOnDate {
			get { return _IsRequiredOnDate; }
			set { _IsRequiredOnDate = value; }
		}
		private string _PostalCode;
		[Column(Storage = "_PostalCode", DbType = "varchar(11)", CanBeNull = true)]
		public string PostalCode {
			get { return _PostalCode; }
			set { _PostalCode = value; }
		}
		private string _ImageName;
		[Column(Storage = "_ImageName", DbType = "varchar(80)", CanBeNull = true)]
		public string ImageName {
			get { return _ImageName; }
			set { _ImageName = value; }
		}
		private int _HelpersNeeded = Int32.MinValue;
		[Column(Storage = "_HelpersNeeded", DbType = "int NOT NULL", CanBeNull = false)]
		public int HelpersNeeded {
			get { return _HelpersNeeded; }
			set { _HelpersNeeded = value; }
		}
		private int _NeedStatusID = Int32.MinValue;
		[Column(Storage = "_NeedStatusID", DbType = "int NOT NULL", CanBeNull = false)]
		public int NeedStatusID {
			get { return _NeedStatusID; }
			set { _NeedStatusID = value; }
		}
		private DateTime _Created;
		[Column(Storage = "_Created", DbType = "datetime NOT NULL", CanBeNull = false)]
		public DateTime Created {
			get { return _Created; }
			set { _Created = value; }
		}
		private System.Nullable<int> _CreatedBy;
		[Column(Storage = "_CreatedBy", DbType = "int", CanBeNull = true)]
		public System.Nullable<int> CreatedBy {
			get { return _CreatedBy; }
			set { _CreatedBy = value; }
		}
		private System.Nullable<DateTime> _Updated;
		[Column(Storage = "_Updated", DbType = "datetime", CanBeNull = true)]
		public System.Nullable<DateTime> Updated {
			get { return _Updated; }
			set { _Updated = value; }
		}
		private System.Nullable<int> _UpdatedBy;
		[Column(Storage = "_UpdatedBy", DbType = "int", CanBeNull = true)]
		public System.Nullable<int> UpdatedBy {
			get { return _UpdatedBy; }
			set { _UpdatedBy = value; }
		}
		private int _Interested = Int32.MinValue;
		[Column(Storage = "_Interested", DbType = "int NOT NULL", CanBeNull = false)]
		public int Interested {
			get { return _Interested; }
			set { _Interested = value; }
		}
		private int _Accepted = Int32.MinValue;
		[Column(Storage = "_Accepted", DbType = "int NOT NULL", CanBeNull = false)]
		public int Accepted {
			get { return _Accepted; }
			set { _Accepted = value; }
		}
		private int _Committed = Int32.MinValue;
		[Column(Storage = "_Committed", DbType = "int NOT NULL", CanBeNull = false)]
		public int Committed {
			get { return _Committed; }
			set { _Committed = value; }
		}
		private int _SubmitterDeclined = Int32.MinValue;
		[Column(Storage = "_SubmitterDeclined", DbType = "int NOT NULL", CanBeNull = false)]
		public int SubmitterDeclined {
			get { return _SubmitterDeclined; }
			set { _SubmitterDeclined = value; }
		}
		private int _HelperDeclined = Int32.MinValue;
		[Column(Storage = "_HelperDeclined", DbType = "int NOT NULL", CanBeNull = false)]
		public int HelperDeclined {
			get { return _HelperDeclined; }
			set { _HelperDeclined = value; }
		}

	}
}
