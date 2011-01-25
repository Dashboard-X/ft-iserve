using System;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using iServe.Models.Security;

namespace iServe.Models.F1APIServices {
    public class PersonAPI {
        #region Constructors

		/// <summary>
        /// Initializes a new instance of the iServe.Models.F1APIServices.PersonAPI class.
        /// </summary>
        public PersonAPI() {
			if (CurrentUser != null) {
				OAuthHelper = new OAuthUtil(Config.BaseAPIUrl, CurrentUser.ChurchCode, Config.ApiVersion, Config.F1LoginMethod, Config.ConsumerKey, Config.ConsumerSecret);
			}
			else {
				throw new Exception("Cannot create PersonAPI object without a church code");
			}
        }
		public PersonAPI(string churchCode) {
			OAuthHelper = new OAuthUtil(Config.BaseAPIUrl, churchCode, Config.ApiVersion, Config.F1LoginMethod, Config.ConsumerKey, Config.ConsumerSecret);
		}
        #endregion

		protected OAuthUtil OAuthHelper { get; set; }

		protected User CurrentUser {
			get {
				User user = null;
				Principal principal = System.Threading.Thread.CurrentPrincipal as Principal;

				if (principal != null) {
					user = principal.CurrentUser as User;
				}

				return user;
			}
		}

        #region Public Methods
        /// <summary>
        /// Gets the user's profile information through the API.
        /// </summary>
        /// <param name="individualID">
        /// The person id of the user.
        /// </param>
        /// <returns>
        /// An iServe.Models.Person containing the user's profile information.
        /// </returns>
        public Person GetPerson(int personID, Token accessToken) {
            try {
				// Create a request to the API to obtain the person.
				HttpWebRequest webRequest = OAuthHelper.CreateWebRequestFromPartialUrl(string.Format("People/{0}", personID), accessToken, HttpRequestMethod.GET);
                               
                using (WebResponse webResponse = webRequest.GetResponse()) {
                    using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream())) {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
                        
                        // Deserialize the response into a Person object.
                        Person person = xmlSerializer.Deserialize(streamReader) as Person;

                        PopulateCommunicationValues(person, accessToken);
                        PopulatePrimaryAddress(person, accessToken);

                        return person;
                    }
                }
            }
            catch (Exception ex) {
                // TODO: add logging.
                throw;
            }
        }

		/// <summary>
        /// Udpates the user's profile information through the API.
        /// </summary>
        /// <param name="person">
        /// An iServe.Models.Person containing the user's profile information.
        /// </param>
        public void UpdatePerson(Person person, Token accessToken) {
            try {
                #region Person
                // Get the person from the API.
                Person personAPI = GetPerson(int.Parse(person.ID), accessToken);

                // Add the basic form values to the person object.
                personAPI.FirstName = person.FirstName;
                personAPI.LastName = person.LastName;
                personAPI.DateOfBirth = person.DateOfBirth;

                // Save to database via the API.
                Save<Person>(personAPI, HttpRequestMethod.PUT, accessToken);
                #endregion

                #region Communication
                // Create a dictionary to store the four communication types.
                Dictionary<string, Communication> communications = new Dictionary<string, Communication>();

                // Get the communication values from the API and add to the dictionary.
                communications.Add(CommunicationType.IndividualEmail.Key, GetCommunicationValueOrDefault(CommunicationType.IndividualEmail, person.IndividualEmailID, person, accessToken));
                communications.Add(CommunicationType.IndividualPhoneNumber.Key, GetCommunicationValueOrDefault(CommunicationType.IndividualPhoneNumber, person.IndividualPhoneNumberID, person, accessToken));
                communications.Add(CommunicationType.HouseholdEmail.Key, GetCommunicationValueOrDefault(CommunicationType.HouseholdEmail, person.HouseholdEmailID, person, accessToken));
                communications.Add(CommunicationType.HouseholdPhoneNumber.Key, GetCommunicationValueOrDefault(CommunicationType.HouseholdPhoneNumber, person.HouseholdPhoneNumberID, person, accessToken));
                                 
                // Populate the individual email with the value from the form.
                // Include the person id to ensure that the individual email is attributed at the person level and not the household level.
                communications[CommunicationType.IndividualEmail.Key].CommunicationValue = person.IndividualEmail;
                
                // Populate the individual phone number with the value from the form.
                communications[CommunicationType.IndividualPhoneNumber.Key].CommunicationValue = person.IndividualPhoneNumber;
                
                // Populate the household email with the value from the form.
                communications[CommunicationType.HouseholdEmail.Key].CommunicationValue = person.HouseholdEmail;
                
                // Populate the household phone number with the value from the form.
                communications[CommunicationType.HouseholdPhoneNumber.Key].CommunicationValue = person.HouseholdPhoneNumber;
                
                // Save to database via the API.
                foreach (var communication in communications.Values) {
                    // If the communication id doesn't exist and the communication value is populated, create a new communication.
                    if (communication.ID == null && !string.IsNullOrEmpty(communication.CommunicationValue)) {
                        communication.CreatedDate = DateTime.Now;
                        Save<Communication>(communication, HttpRequestMethod.POST, accessToken);
                    }
                    // If the communication id exists and the communication value is populated, update the communication.
                    else if (communication.ID != null && !string.IsNullOrEmpty(communication.CommunicationValue)) {
                        communication.LastUpdatedDate = DateTime.Now;
                        Save<Communication>(communication, HttpRequestMethod.PUT, accessToken);
                    }
                    // If the communication id exists but the communication value is empty, delete the communication.                
                    else if (communication.ID != null && string.IsNullOrEmpty(communication.CommunicationValue)) {
                        Delete<Communication>(communication, accessToken);
                    }
                }                
                #endregion

                #region Address
                // Get the address from the API.
                Address address = GetAddress(person.Address.ID, accessToken);
                
                // Add the address form values to the address object.
                address.Address1 = person.Address.Address1;
                address.Address2 = person.Address.Address2;
                address.City = person.Address.City;
                address.StProvince = person.Address.StProvince;
                address.PostalCode = person.Address.PostalCode;           

                // Save to database via the API.
                Save<Address>(address, HttpRequestMethod.PUT, accessToken);
                #endregion
            }
            catch (Exception ex) {
                // TODO: add logging.
                throw;
            }
        }

        /// <summary>
        /// Deletes a resource from the database through the API.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the resource.
        /// <para>
        /// The resource type must implement the IF1APIResource interface.
        /// </para>
        /// </typeparam>
        /// <param name="resource">
        /// The resource to delete from the database.
        /// </param>
        private void Delete<T>(T resource, Token accessToken) where T : IF1APIResource {
            try {
                if (resource == null) {
                    throw new NullReferenceException("Resource object is null in iServe.Models.F1APIServices.PersonAPI.Delete<T>().");
                }
                               
                // Initialize a web request to the API so that the resource can be written to it.
				HttpWebRequest webRequest = OAuthHelper.CreateWebRequest(resource.Uri, accessToken, HttpRequestMethod.DELETE);

                // Set headers.
                webRequest.Method = "DELETE";

                // Get the response to finish the request.
                using (HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse) {
                    if (webResponse.StatusCode == HttpStatusCode.NoContent) {
                        // Success
                    }
                    else {
                        // Failed
                    }
                }
            }
            catch (Exception ex) {
                // TODO: add logging.

                throw;
            }
        }
                
        /// <summary>
        /// Saves a resource to the database through the API by serializing it into xml and streaming it via a web request.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the resource.
        /// <para>
        /// The resource type must implement the IF1APIResource interface.
        /// </para>
        /// </typeparam>
        /// <param name="resource">
        /// The resource to save to the database.
        /// </param>
        /// <param name="requestMethod">
        /// The http request method (POST or PUT).
        /// </param>
        private void Save<T>(T resource, HttpRequestMethod requestMethod, Token accessToken) where T : IF1APIResource {
            try {
                if (resource == null) {
                    throw new NullReferenceException("Resource object is null in iServe.Models.F1APIServices.PersonAPI.Save<T>().");
                }

                MemoryStream stream = new MemoryStream();
                string content;
                
                // Serialize the person into an xml stream.
                XmlSerializer xmlSerializer = new XmlSerializer(resource.GetType());
                xmlSerializer.Serialize(stream, resource);

                // Reset the index position in the stream.
                stream.Position = 0;

                using (StreamReader streamReader = new StreamReader(stream)) {
                    // Read the contents of the stream into a string.
                    content = streamReader.ReadToEnd();
                }

                // Strip out "xsi:nil="true"" from the xml since the API doesn't like it for now.
                content = content.Replace("xsi:nil=\"true\"", string.Empty);

                // Convert the contents into a byte array.
                ASCIIEncoding asciiEncoding = new ASCIIEncoding();
                byte[] contentInBytes = asciiEncoding.GetBytes(content);
                               
                // Initialize a web request to the API so that the resource can be written to it.
				HttpWebRequest webRequest = OAuthHelper.CreateWebRequest(resource.Uri, accessToken, requestMethod);

                // Set headers.
                webRequest.Method = requestMethod.ToString();
                webRequest.Accept = "application/xml";
                webRequest.KeepAlive = false;
                webRequest.ContentLength = contentInBytes.LongLength;

                // Get the request stream to write the resource to.
                using (Stream requestStream = webRequest.GetRequestStream()) {
                    requestStream.Write(contentInBytes, 0, contentInBytes.Length);

                    // Get the response to finish the request.
                    using (HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse) {
                        if (webResponse.StatusCode == HttpStatusCode.OK || webResponse.StatusCode == HttpStatusCode.Created) {
                            // Success
                        }
                        else {
                            // Failed
                        }
                    }
                }
            }
            catch (Exception ex) {
                // TODO: add logging.

                throw;
            }
        }       

        /// <summary>
        /// Gets a communication value or the default (empty) if none is found.
        /// </summary>
        /// <param name="communicationType">
        /// The communication type.
        /// </param>
        /// <param name="communicationID">
        /// The communication id. If null, a default (empty) communication object is returned.
        /// </param>
        /// <returns>
        /// An iServe.Models.Communication containing one of the user's communication values or a default communication object if none is found.
        /// </returns>
        public Communication GetCommunicationValueOrDefault(KeyValuePair<string, int> communicationType, string communicationID, Person person, Token accessToken) {
            try {
                // Initialize a default communication object with the specified communication type.
                Communication communication = new Communication() {
                    CommunicationType = new CommunicationType {
                        ID = communicationType.Value.ToString()
                    },
                    Listed = true,
                    Person = new Person(),
                    Household = new Household(),
                };

                // Determine whether the communication is attributed to the household or the person based on the communication type.
                switch (communicationType.Key) {
                    case "IndividualEmail":
                    case "IndividualPhoneNumber":
						communication.Uri = OAuthHelper.CreateAPIUrl(CurrentUser.ChurchCode, Config.BaseAPIUrl, Config.ApiVersion, string.Format("People/{0}/Communications", person.ID));
                        communication.Person.ID = person.ID;
                        break;

                    case "HouseholdEmail":
                    case "HouseholdPhoneNumber":
						communication.Uri = OAuthHelper.CreateAPIUrl(CurrentUser.ChurchCode, Config.BaseAPIUrl, Config.ApiVersion, string.Format("Households/{0}/Communications", person.HouseholdID));
                        communication.Household.ID = person.HouseholdID;
                        break;
                }

                if (communicationID != null) {
                    // Create a request to the API to obtain the communcation values.
					WebRequest webRequest = OAuthHelper.CreateWebRequestFromPartialUrl(string.Format("Communications/{0}", communicationID), accessToken, HttpRequestMethod.GET);

                    using (WebResponse webResponse = webRequest.GetResponse()) {
                        using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream())) {
                            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Communication));

                            // Deserialize the response into a Communication object.
                            communication = xmlSerializer.Deserialize(streamReader) as Communication;
                        }
                    }
                }

                return communication;
            }
            catch (Exception ex) {
                // TODO: add logging.
                throw;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Populates the user's communication values retrieved through the API. The values are separated by type (phone/email) and by owner (household/individual).
        /// </summary>
        private void PopulateCommunicationValues(Person person, Token accessToken) {
            try {
                // Create a request to the API to obtain the communcation values.
				WebRequest webRequest = OAuthHelper.CreateWebRequestFromPartialUrl(string.Format("People/{0}/Communications", person.ID), accessToken, HttpRequestMethod.GET);

                using (WebResponse webResponse = webRequest.GetResponse()) {
                    using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream())) {
                        // Load the xml response from the API into an XElement so that the communication values can be traversed.
                        XElement xml = XElement.Load(streamReader);

                        if (xml.Element("communication") != null) {
                            // Find the first email element of type Email that is associated to the user.
                            var individualEmail = xml.Elements("communication").FirstOrDefault(communication => !string.IsNullOrEmpty(communication.Element("person").Attribute("id").Value) && communication.Element("communicationType").Value.Equals("Email"));

                            // Find the first phone element of type Mobile that is associated to the user.
                            var individualPhoneNumber = xml.Elements("communication").FirstOrDefault(communication => !string.IsNullOrEmpty(communication.Element("person").Attribute("id").Value) && communication.Element("communicationType").Value.Equals("Mobile"));

                            // Find the first email element of type Email that is associated to the user's household.
                            var householdEmail = xml.Elements("communication").FirstOrDefault(communication => string.IsNullOrEmpty(communication.Element("person").Attribute("id").Value) && communication.Element("communicationType").Value.Contains("Email"));

                            // Find the first phone element of type Home that is associated to the user's household.
                            var householdPhoneNumber = xml.Elements("communication").FirstOrDefault(communication => string.IsNullOrEmpty(communication.Element("person").Attribute("id").Value) && communication.Element("communicationType").Value.Contains("Home"));

                            //
                            // Add the communication values into their respective properties.
                            //
                            if (individualEmail != null) {
                                person.IndividualEmailID = individualEmail.Attribute("id").Value;
                                person.IndividualEmail = individualEmail.Element("communicationValue").Value;
                            }

                            if (individualPhoneNumber != null) {
                                person.IndividualPhoneNumberID = individualPhoneNumber.Attribute("id").Value;
                                person.IndividualPhoneNumber = individualPhoneNumber.Element("communicationValue").Value;
                            }

                            if (householdEmail != null) {
                                person.HouseholdEmailID = householdEmail.Attribute("id").Value;
                                person.HouseholdEmail = householdEmail.Element("communicationValue").Value;
                            }

                            if (householdPhoneNumber != null) {
                                person.HouseholdPhoneNumberID = householdPhoneNumber.Attribute("id").Value;
                                person.HouseholdPhoneNumber = householdPhoneNumber.Element("communicationValue").Value;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                // TODO: add logging.
                throw;
            }
        }

        /// <summary>
        /// Gets the primary address.
        /// </summary>
        /// <param name="communicationID">
        /// The address id.
        /// </param>
        /// <returns>
        /// An iServe.Models.Address containing the user's primary address.
        /// </returns>
        private Address GetAddress(string addressID, Token accessToken) {
            try {
                // Create a request to the API to obtain the address.
				WebRequest webRequest = OAuthHelper.CreateWebRequestFromPartialUrl(string.Format("Addresses/{0}", addressID), accessToken, HttpRequestMethod.GET);

                using (WebResponse webResponse = webRequest.GetResponse()) {
                    using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream())) {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Address));

                        // Deserialize the response into a Address object.
                        Address address = xmlSerializer.Deserialize(streamReader) as Address;

                        return address;
                    }
                }
            }
            catch (Exception ex) {
                // TODO: add logging.
                throw;
            }
        }

        /// <summary>
        /// Populates the user's primary address that is retrieved through the API.
        /// </summary>
        private void PopulatePrimaryAddress(Person person, Token accessToken) {
            try {
                // Create a request to the API to obtain the primary address.
				WebRequest webRequest = OAuthHelper.CreateWebRequestFromPartialUrl(string.Format("Households/{0}/Addresses", person.HouseholdID), accessToken, HttpRequestMethod.GET);

                using (WebResponse webResponse = webRequest.GetResponse()) {
                    using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream())) {
                        // Load the xml response from the API into an XElement so that the primary address can be located.
                        XElement xml = XElement.Load(streamReader);

                        if (xml.Element("address") != null) {
                            // Find the primary address.
                            var primaryAddress = xml.Elements("address").Where(address => address.Element("addressType").Value.Equals("Primary"));

                            if (primaryAddress != null) {
                                // Create a new Address instance with the primary address values obtained from the API response.
                                person.Address = new Address {
                                    ID = primaryAddress.Single().Attribute("id").Value,
                                    Address1 = (primaryAddress.Single().Element("address1") != null) ? primaryAddress.Single().Element("address1").Value : string.Empty,
                                    Address2 = (primaryAddress.Single().Element("address2") != null) ? primaryAddress.Single().Element("address2").Value : string.Empty,
                                    //Address3 = (primaryAddress.Single().Element("address3") != null) ? primaryAddress.Single().Element("address3").Value : string.Empty,
                                    City = (primaryAddress.Single().Element("city") != null) ? primaryAddress.Single().Element("city").Value : string.Empty,
                                    StProvince = (primaryAddress.Single().Element("stProvince") != null) ? primaryAddress.Single().Element("stProvince").Value : string.Empty,
                                    PostalCode = (primaryAddress.Single().Element("postalCode") != null) ? primaryAddress.Single().Element("postalCode").Value : string.Empty
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                // TODO: add logging.
                throw;
            }
        }
        #endregion
    }
}
