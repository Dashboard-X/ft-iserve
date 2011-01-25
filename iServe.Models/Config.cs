using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using iServe.Models.dotNailsCommon;

namespace iServe.Models {
	public class Config {

		// Static vars
		public readonly static string DomainName;
		public readonly static string DefaultChurchCode;
		public readonly static string AppVersion;
		public readonly static int RecordsPerPage;
		public readonly static string FromEmail;
		public readonly static string SMTPServer;
		public readonly static string SMTPUsername;
		public readonly static string SMTPPassword;
		public readonly static string ConsumerKey;
		public readonly static string ConsumerSecret;
		public readonly static string F1LoginMethod;
		public readonly static string BaseAPIUrl;
		public readonly static string ApiVersion;

		// Constants
		//public const string SampleConstant = "somethingImportant";

		// Constructor is run once, when it is first requested
		static Config() {
			IModelFactory<iServeDBProcedures> model = new ModelFactory<iServeDBProcedures>(new iServeDBDataContext());
			List<ConfigSetting> configSettings = model.New<ConfigSetting>().GetAll().ToList();

			// Get all our configuration settings from config file and put them into our static variables.
			AppVersion = configSettings.Single(c => c.Name =="AppVersion").Value;
			ConfigSetting recordsPerPage = configSettings.SingleOrDefault(c => c.Name == "RecordsPerPage");
			if (recordsPerPage != null) {
				RecordsPerPage = int.Parse(recordsPerPage.Value);
			}
			else {
				RecordsPerPage = 10;
			}
			DomainName = configSettings.Single(c => c.Name == "DomainName").Value;
			DefaultChurchCode = configSettings.Single(c => c.Name == "DefaultChurchCode").Value;
			FromEmail = configSettings.Single(c => c.Name == "FromEmail").Value;
			SMTPServer = configSettings.Single(c => c.Name == "SMTPServer").Value;
			SMTPUsername = configSettings.Single(c => c.Name == "SMTPUsername").Value;
			SMTPPassword = configSettings.Single(c => c.Name == "SMTPPassword").Value;
			ConsumerKey = configSettings.Single(c => c.Name == "ConsumerKey").Value;
			ConsumerSecret = configSettings.Single(c => c.Name == "ConsumerSecret").Value;
			F1LoginMethod = configSettings.Single(c => c.Name == "F1LoginMethod").Value;
			BaseAPIUrl = configSettings.Single(c => c.Name == "BaseAPIUrl").Value;
			ApiVersion = configSettings.Single(c => c.Name == "ApiVersion").Value;
		}
	}
}
