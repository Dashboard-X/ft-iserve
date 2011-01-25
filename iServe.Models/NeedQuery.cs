using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iServe.Models {
	public partial class NeedQuery {
		public NeedQuery WhereUserHasNoInvolvement(int userID) {
			var needIDs = from un in _db.UserNeeds
						  where un.UserID == userID
						  select un.NeedID;

            // Get the needs that aren't associated to the user and that the user hasn't submitted.
			IQueryable<Need> needs = from n in _query
									 where !needIDs.Contains(n.ID)
                                     && userID != n.SubmittedByID
									 select n;

			return new NeedQuery(needs, _db);
		}

        /// <summary>
        /// Filters out the needs that are cancelled.
        /// </summary>
        public NeedQuery WhereNotCancelled() {
            IQueryable<Need> needs = from n in _query
                                     where (NeedStatusEnum)n.NeedStatusID != NeedStatusEnum.Cancelled
                                     select n;

            return new NeedQuery(needs, _db);
        }

        /// <summary>
        /// Filters out the needs that are expired.
        /// </summary>
        public NeedQuery WhereNotExpired() {
            IQueryable<Need> needs = from n in _query
                                     where n.Created <= DateTime.Now
                                     select n;

            return new NeedQuery(needs, _db);
        }
	}
}
