using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iServe.Models.dotNailsCommon;
using iServe.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iServe.Tests {
	public class iServeTest {

		protected int _userID = 24643456; // thardy
		protected int _churchID = 501; // ftapiwater

		public IModelFactory<iServeDBProcedures> Model = new ModelFactory<iServeDBProcedures>(new iServeDBDataContext());
		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext {
			get {
				return testContextInstance;
			}
			set {
				testContextInstance = value;
			}
		}

		protected iServeDBDataContext GetNewDataContext() {
			return new iServeDBDataContext();
		}

		protected User GetExistingUser(int id, int churchID, string name, string email, UserStatusEnum userStatus) {
			User user = null;

			try {
				user = Model.New<User>();
				user.ID = id;
				user.EntityState = EntityStateEnum.Added; // Have to manually set this because we set the ID for User (otherwise the presence of the PK will cause the model to assume an update)
				user.ChurchID = churchID;
				user.Name = name;
				user.Email = email;
				user.UserStatusID = (int)UserStatusEnum.Active;
				user.SetDataContext(GetNewDataContext());
				user.Save();

				// We have to change the EntityState back to NotSet or else all Save operations will think we're always inserting.
				user.EntityState = EntityStateEnum.NotSet;
			}
			catch (Exception ex) {
				Assert.IsTrue(false, "Failed to create existing User in setup - " + ex.Message);
			}

			return user;
		}

		protected Need GetExistingNeed(int churchID, CategoryEnum category, int submittedByID, string name, string description, string postalCode, DateTime requiredDate, int helpersNeeded) {
			Need need = null;

			try {
				need = Model.New<Need>();
				need.ChurchID = churchID;
				need.CategoryID = (int)category;
				need.SubmittedByID = submittedByID;
				need.Name = name;
				need.Description = description;
				need.PostalCode = postalCode;
				need.RequiredDate = requiredDate;
				need.NeedStatusID = 1;
				need.HelpersNeeded = helpersNeeded;
				need.SetDataContext(GetNewDataContext());
				need.Save();

				// We have to change the EntityState back to NotSet or else all Save operations will think we're always inserting.
				need.EntityState = EntityStateEnum.NotSet;
			}
			catch (Exception ex) {
				Assert.IsTrue(false, "Failed to create existing Need in setup - " + ex.Message);
			}

			return need;
		}

		//protected UserNeed GetExistingUserNeed(int needID, int userID, UserNeedStatusEnum userNeedStatus) {
		//    UserNeed userNeed = null;

		//    try {
		//        userNeed = Model.New<UserNeed>();
		//        userNeed.UserID = userID;
		//        userNeed.NeedID = needID;
		//        userNeed.UserNeedStatusID = (int)userNeedStatus;
		//        userNeed.SetDataContext(GetNewDataContext());
		//        userNeed.Save();

		//        // We have to change the EntityState back to NotSet or else all Save operations will think we're always inserting.
		//        userNeed.EntityState = EntityStateEnum.NotSet;
		//    }
		//    catch (Exception ex) {
		//        Assert.IsTrue(false, "Failed to create existing UserNeed in setup - " + ex.Message);
		//    }

		//    return userNeed;
		//}
	}
}
