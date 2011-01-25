using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iServe.Models.dotNailsCommon;

namespace iServe.Models {
	public partial class UserNeed : IAuditable {
        /// <summary>
        /// Gets all the needs that the specified user has expressed interest in.
        /// </summary>
        /// <param name="userID">
        /// The user id of the user that expressed interest in a need.
        /// </param>
        /// <returns>
        /// An IQueryable that returns all the needs that the specified user has expressed interest in.
        /// </returns>
		public UserNeedQuery GetByUserID(int userID) {
			IQueryable<UserNeed> userNeeds = from un in _db.UserNeeds
											 where un.UserID == userID
											 select un;

			return new UserNeedQuery(userNeeds, _db);
		}

		public int GetUnratedCommittedUsersCount(int needID) {
			int count = (from un in _db.UserNeeds
						 where un.NeedID == needID
						 && un.UserNeedStatusID == (int)UserNeedStatusEnum.Committed
						 && un.HasBeenRated == false
						 select un.UserID).Count();

			return count;
		}

        public int GetInterestedUsersCount(int needID) {
            int count = (from un in _db.UserNeeds
                         where un.NeedID == needID
                         && un.UserNeedStatusID == (int)UserNeedStatusEnum.Interested
                         select un.UserID).Count();

            return count;
        }
	}
}
