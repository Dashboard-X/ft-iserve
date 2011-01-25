using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using iServe.Models;
using iServe.Models.dotNailsCommon;
using iServe.Controllers;
using System.Web.Mvc;

namespace iServe.Tests.Controllers {
	/// <summary>
	/// Summary description for NeedControllerTest
	/// </summary>
	[TestClass]
	public class NeedControllerTest {
		public NeedControllerTest() {
			//
			// TODO: Add constructor logic here
			//
		}

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

		//[TestMethod]
		//public void Testing_iServeDBSPs_Mock() {
		//    var mockDB = new Mock<iServeDBDataContext>();
		//    var mockSPs = new Mock<iServeDBProcedures>(mockDB.Object);
		//    mockSPs.Expect(m => m.ToString()).Returns("yomama");
		//    var mockModelFactory = new Mock<IModelFactory<iServeDBProcedures>>();
		//    mockModelFactory.Expect(m => m.SPs).Returns(mockSPs.Object);

		//    NeedController controller = new NeedController(mockModelFactory.Object);
		//    ActionResult result = controller.SPTest();
		//    Assert.IsTrue(true);

		//}
	}
}
