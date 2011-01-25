using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iServe.Models.dotNailsCommon;

namespace iServe.Models {
	public partial class User : iServe.Models.Security.IServeUser, IAuditable {
		public string ChurchCode { get; set; }

		public User(string delimitedString) {
			string[] input = delimitedString.Split('~');

			if (input.Length < 5) {
				// log an error
				throw new Exception("Invalid User delimited string sent to User constructor.");
			}
			else {
				ID = Convert.ToInt32(input[0]);
				ChurchID = Convert.ToInt32(input[1]);
				Name = input[2];
				Email = input[3];
				Rating = Convert.ToInt32(input[4]);
			}
		}

		public string ToDelimitedString() {
			StringBuilder output = new StringBuilder();

			//User user = new User{ ID=1, ChurchID=1, Name, Email, Rating}
			output.Append(ID);
			output.Append("~").Append(ChurchID);
			output.Append("~").Append(Name);
			output.Append("~").Append(Email);
			output.Append("~").Append(Rating);

			return output.ToString();
		}

		public User GetSubmitterByNeedID(int id) {
			User user = (from u in _db.Users
						 from n in u.Needs
						 where n.ID == id
						 && n.SubmittedByID == u.ID
						 select u).SingleOrDefault();
            
			return user;
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
	}
}
