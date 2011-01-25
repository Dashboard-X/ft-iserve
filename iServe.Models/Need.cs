using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iServe.Models.dotNailsCommon;
using System.Data.Linq;
//using xVal.ServerSide;

namespace iServe.Models {
	[System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata))]
	public partial class Need : IAuditable {
		#region Validation
		private class Metadata {
			[Required(RunWhenEnum.Update)]
			public int ID { get; set; }

			[Required]
			public int CategoryID { get; set; }

			[Required]
			[StringLength(100)]
			public string Name { get; set; }

			[Required]
			[StringLength(3000)]
			public string Description { get; set; }

			[Required]
			public int RequiredDate { get; set; }

			[Required]
			public int NeedStatusID { get; set; }

			[Required]
			public int HelpersNeeded { get; set; }
		}
		#endregion

		partial void OnCreated() {
			HelpersNeeded = 1;
		}

		//public IPagedList<Need> GetNeeds() {
		//    ((iServeDBProcedures)_db.Procedures)
		//}

        /// <summary>
        /// Gets all the needs that are submitted by the specified user.
        /// </summary>
        /// <param name="userID">
        /// The user id of the user that submitted the need.
        /// </param>
        /// <returns>
        /// An IQueryable that returns all the needs that are submitted by the specified user.
        /// </returns>
        public IQueryable<Need> GetByUserID(int userID) {
            IQueryable<Need> needs = _db.Needs.Where(need => need.SubmittedByID == userID);

            return needs;
        }

		public virtual NeedQuery GetAllByChurchID(int churchID) {
			IQueryable<Need> query = from need in _db.Needs
									 where need.ChurchID == churchID
									 select need;
			return new NeedQuery(query, _db);
		}

        /// <summary>
        /// Gets the need with the specified id which was submitted by the specified user id.
        /// </summary>
        /// <param name="iD">
        /// The id of the need.
        /// </param>
        /// <param name="userID">
        /// The id of the user that submitted the need.
        /// </param>
        /// <returns></returns>
        public Need GetByKeyAndUserID(int iD, int userID) {
            Need need = _db.Needs.SingleOrDefault(n => n.ID == iD && n.SubmittedByID == userID);

            need.SetDataContext(_db);

            return need;
        }

		//public List<User> GetRelatedUsersByStatus(UserNeedStatusEnum userNeedStatus) {
		//    //var committedUserIDs = need.UserNeeds.Where(userNeed => userNeed.UserNeedStatus.Name == "Committed").Select(userNeed => userNeed.UserID).ToList();
		//    //var committedUsers = Model.New<User>().GetAll().ToQuery().Where(user => committedUserIDs.Contains(user.ID)).ToList();
		//    var userIDs = from un in _db.UserNeeds
		//                  where un.UserNeedStatusID == (int)userNeedStatus
		//                  && un.NeedID == this.ID
		//                  select un.UserID;
		//    List<User> users = (from u in this.Users
		//                        where userIDs.Contains(u.ID)
		//                        select u).ToList();

		//    return users;
		//}

		public List<Need> GetAllByUser(int userID, out Dictionary<int, string> userNeedStatuses) {
			//DataLoadOptions dlo = new DataLoadOptions();
			//dlo.LoadWith<Need>(n => n.UserNeeds);
			//_db.LoadOptions = dlo;
			_db.Log = new DebugTextWriter();

			var needIDs = from un in _db.UserNeeds
						  where un.UserID == userID
						  select un.NeedID;

			List<Need> needs = (from n in _db.Needs
								where needIDs.Contains(n.ID)
								select n).ToList();

			userNeedStatuses = new Dictionary<int, string>();

			var statuses = (from un in _db.UserNeeds
						   where needIDs.Contains(un.NeedID)
						   && un.UserID == userID
						   select new { un.NeedID, un.UserNeedStatusID }).ToList();

			foreach (var status in statuses) {
				DataItem statusItem = UserNeedStatusEnumList.DataSource.Find(s => s.Value == status.UserNeedStatusID.ToString());
				userNeedStatuses.Add(status.NeedID, statusItem.Text);
			}

			return needs;
		}

		/// <summary>
		/// Takes a list of Needs and returns a dictionary of InvolvementCount objects with NeedID as key
		/// </summary>
		/// <param name="delimitedNeedIDs"></param>
		/// <returns></returns>
		public Dictionary<int, InvolvementCount> GetInvolvementCounts(List<Need> needs) {
			StringBuilder needIDs = new StringBuilder();
			bool first = true;
			foreach (Need need in needs) {
				if (!first) {
					needIDs.Append(",");
				}
				else {
					first = false;
				}
				needIDs.Append(need.ID.ToString());
			}

			List<GetInvolvementCountsResult> involvementCountResults = ((iServeDBProcedures)_db.Procedures).GetInvolvementCounts(needIDs.ToString()).ToList();
			Dictionary<int, InvolvementCount> involvementCounts = new Dictionary<int, InvolvementCount>();

			foreach (Need need in needs) {
				GetInvolvementCountsResult result = involvementCountResults.Find(r => r.NeedID == need.ID);
				if (result != null) {
					InvolvementCount involvementCount = new InvolvementCount(result.Interested.Value, result.Accepted.Value, result.Committed.Value, result.SubmitterDeclined.Value, result.HelperDeclined.Value);
					involvementCounts.Add(result.NeedID, involvementCount);
				}
				else {
					involvementCounts.Add(need.ID, new InvolvementCount(0, 0, 0, 0, 0));
				}
			}
			
			return involvementCounts;
		}
		
    }
}
