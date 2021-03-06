﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using iServe.Models.F1APIServices;
using iServe.Models.dotNailsCommon;
using iServe.Models;

namespace iServe.Controllers {

	[HandleError]
	public class AccountController : iServeController {
        public IModelFactory<iServeDBProcedures> Model { get; set; }
		private PersonAPI PersonAPI { get; set; }
		protected OAuthUtil OAuthHelper { get; set; }

        public AccountController() {
			string churchCode = GetRequestedChurchCode();
            Model = new ModelFactory<iServeDBProcedures>(new iServeDBDataContext(), GetCurrentUserID());
			OAuthHelper = new OAuthUtil(Config.BaseAPIUrl, churchCode, Config.ApiVersion, Config.F1LoginMethod, Config.ConsumerKey, Config.ConsumerSecret);
			PersonAPI = new PersonAPI(churchCode);
        }

        public AccountController(IModelFactory<iServeDBProcedures> modelFactory, PersonAPI personAPI, OAuthUtil oauthHelper) {
            Model = modelFactory;
			PersonAPI = PersonAPI;
			OAuthHelper = oauthHelper;
        }

		public ActionResult LogOn() {
			try {
				// Ask the API for a request token, so we can begin talking to the API
				Token requestToken = OAuthHelper.GetRequestToken();
				this.RequestToken = requestToken;

				if (requestToken != null) {
					// Get a link for user login
                    string callbackUrl = string.Format("http://{0}/Account/RequestAccessToken", Request.Url.Host);
					string authorizationLink = OAuthHelper.RequestUserAuth(requestToken.Value, callbackUrl);

					// Ask the API to log the user in (user will enter username and password on a page generated by the API - we never see the credentials).
					//  Once the user has logged in, the API will redirect to our page/action (/Account/RequestAccessToken)
					return new RedirectResult(authorizationLink);
				}
			}
			catch (Exception ex) {
				ViewData["error"] = ex.Message;
			}

			return View("Error");

		}

		public ActionResult RequestAccessToken() {
			try {
				Token accessToken = null;
				int? individualID = null;
				
				string personUrl;
				// Exchange the request token for an access token (we'll store the access token in the db and reuse it for as long as the user allows us)
				accessToken = OAuthHelper.GetAccessToken(this.RequestToken, out personUrl);

				string indID = personUrl.Substring(personUrl.LastIndexOf("/")+1);
				individualID = Convert.ToInt32(indID);

				User user = null;

				if (individualID != null) {
					// Get user from database
					user = Model.New<User>().GetByKey(individualID.Value);

					if (user != null) {
						// Ensure that the user's saved accessToken is the same as the one we just received from the API
						if (user.AccessToken != accessToken.Value) {
							// Save the new accessToken
							user.AccessToken = accessToken.Value;
							user.AccessTokenSecret = accessToken.Secret;
							user.Save();
						}
					}
					else {
						// User doesn't exist
						// Get the Person via the API, so we can create a new user
						Person person = PersonAPI.GetPerson(individualID.Value, accessToken);

						if (person != null) {
							// Create a new user record
							user = Model.New<User>();
							user.ID = individualID.Value;
							user.ChurchID = 501;
							user.Name = person.Weblink.UserID;
							user.Email = person.IndividualEmail;
							user.Rating = 0;
							user.UserStatusID = (int)UserStatusEnum.Active;
							user.AccessToken = accessToken.Value;
							user.AccessTokenSecret = accessToken.Secret;
							user.EntityState = EntityStateEnum.Added;
							user.Save();
						}
					}

					// Save accessToken in Session
					AccessToken = accessToken;

					// Write auth cookie
					//WriteAuthCookie(individualID.Value, user.Name);
					user.ChurchCode = GetRequestedChurchCode();

					WriteAuthCookie(user);

					// Redirect user to the "home" page for logged-in users (whatever that is)
					return RedirectToAction("Index", "Need");
				}
			
			}
			catch (Exception ex) {
				ViewData["error"] = ex.Message;
			}

			return View("Error");
		}

		public ActionResult LogOut() {
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Need");
		}

		private void WriteAuthCookie(User user) {//int userID, string username) {
			double formsAuthTimeout = 40;
			string userData = user.ToDelimitedString();

			FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
															1,				// version    
															user.Name,		// user name    
															DateTime.Now,	// creation
															DateTime.Now.AddMinutes(formsAuthTimeout),  // Expiration
															false,			// isPersistent
															user.ToDelimitedString() // user data (just user object in simple delimited string)
															);
			string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
			HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

			// Make sure we mark the cookie as "Secure" if RequireSSL is set in the web.config.
			//  If we don't, the FIRST issuing of this cookie will not be secure 
			//  (as we are the ones that did it) while the second issuing (when it's
			//  being refreshed) will be secure. That would cause intermittent problems with 
			//  timeout-like behaviors around "timeout/2" minutes into the user's session.
			authCookie.Secure = FormsAuthentication.RequireSSL;
			authCookie.HttpOnly = true;
			HttpContext.Response.Cookies.Add(authCookie);
		}

	}
}
