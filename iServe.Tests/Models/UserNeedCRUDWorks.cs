using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iServe.Models;
using System.Transactions;
using iServe.Models.dotNailsCommon;

namespace iServe.Tests.Models {
	/// <summary>
	/// Summary description for UserNeedCRUDWorks
	/// </summary>
	[TestClass]
	public class UserNeedCRUDWorks : iServeTest {
		public UserNeedCRUDWorks() {
			//
			// TODO: Add constructor logic here
			//
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void Save_on_new_UserNeed_creates_record_in_table() {
			using (TransactionScope scope = new TransactionScope()) {
				// Given: an existing Need 
				Need existingNeed = GetExistingNeed(_churchID, CategoryEnum.Clothes, _userID, "I need clothes", "Because I am NAKED", "75240", new DateTime(2009, 5, 15), 1);

				// When: I create a UserNeed
				Model.DataContext = GetNewDataContext(); 
				UserNeed userNeed = Model.New<UserNeed>();
				userNeed.EntityState = EntityStateEnum.Added;
				userNeed.UserID = _userID;
				userNeed.NeedID = existingNeed.ID;
				userNeed.UserNeedStatusID = (int)UserNeedStatusEnum.Interested;
				userNeed.Save(); 

				// Then: it should be present in the db
				Model.DataContext = GetNewDataContext();
				UserNeed retrievedUserNeed = Model.New<UserNeed>().GetByKey(userNeed.UserID, userNeed.NeedID);

				Assert.AreEqual(userNeed.UserID, retrievedUserNeed.UserID);
				Assert.AreEqual(userNeed.NeedID, retrievedUserNeed.NeedID);
				Assert.AreEqual(userNeed.Created.ToString(), retrievedUserNeed.Created.ToString());
				
				scope.Dispose();
			}
		}
	}
}
