using System;
using System.Text;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iServe.Models.dotNailsCommon;
using iServe.Models;

namespace iServe.Tests.Models {
	/// <summary>
	/// Summary description for NeedModelWorks
	/// </summary>
	[TestClass]
	public class NeedCRUDWorks : iServeTest {
		
		public NeedCRUDWorks() {
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
		public void Save_on_existing_need_adding_existing_users_creates_records_in_UserNeed_table() {
			using (TransactionScope scope = new TransactionScope()) {
				
				// Given: an existing Need 
				Need existingNeed = GetExistingNeed(_churchID, CategoryEnum.Clothes, _userID, "I need clothes", "Because I am NAKED", "75240", new DateTime(2009, 5, 15), 1);

				// And: two existing Users
				User existingUser1 = GetExistingUser(-999, _churchID, "Bobo the Clown", "bobo@aol.com", UserStatusEnum.Active);
				User existingUser2 = GetExistingUser(-998, _churchID, "Mr Bojangles", "bojangles@yahoo.com", UserStatusEnum.Active);

				// When: I associate those two Users to the need
				Model.DataContext = GetNewDataContext();  // Simulate atomic unit of work by starting from scratch
				Model.DataContext.Log = new DebugTextWriter();
				Need newNeed = Model.New<Need>();
				newNeed.ID = existingNeed.ID;
				newNeed.EntityState = EntityStateEnum.UnModified;
				newNeed.Users.Add(new User { ID = existingUser1.ID, EntityState = EntityStateEnum.UnModified });
				newNeed.Users.Add(new User { ID = existingUser2.ID, EntityState = EntityStateEnum.UnModified });
				newNeed.Save();

				// And: I get that need from the database
				Model.DataContext = GetNewDataContext(); // without this, it doesn't even hit the db because it already has an object with that key in the context 
				Need need = Model.New<Need>().GetByKey(existingNeed.ID);
				Assert.IsNotNull(need);
				
				// And: access the Users property of my need
				//List<User> relatedUsers = need.Users.ToList();
				User user1 = need.Users.First(u => u.ID == existingUser1.ID);
				User user2 = need.Users.First(u => u.ID == existingUser2.ID);

				// Then: I have the users that are related to the need
				Assert.IsNotNull(user1);
				Assert.IsNotNull(user2);

				scope.Dispose();
			}

		}

		
	}
}

