using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iServe.Models.F1APIServices;

namespace iServe.Models {
	public partial class UserQuery {
		//public User GetByAccessToken(Token accessToken) {
		//    User user = (from u in _query
		//                 where u.AccessToken == accessToken.Value
		//                 && u.AccessTokenSecret == accessToken.Secret
		//                 select u).SingleOrDefault();

		//    return user;
		//}

		//public TaskQuery GetOpenProspectTasksForIndividual(int individualID) {

		//    // Get all the open prospect tasks then filter but the individual id
		//    IQueryable<Task> openTaskQuery = from task in _query
		//                                     where task.CommunicationTargetID == individualID
		//                                     && task.ParentTaskID == null
		//                                     && this.OpenTaskStatuses.Contains(task.TaskStatusID)
		//                                     select task;

		//    return new TaskQuery(openTaskQuery);
		//}
	}
}
