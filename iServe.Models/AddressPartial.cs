using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iServe.Models.F1APIServices;
using System.Xml.Serialization;
using System.Xml.Linq;
using iServe.Models.dotNailsCommon;

namespace iServe.Models {
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata))]
    public partial class Address : IF1APIResource, IXmlSerializable {
        #region Public Methods
        /// <summary>
        /// Formats the way the address is displayed on the page and converts the value of this instance to a System.String.
        /// </summary>
        /// <returns>
        /// The formatted, string representation of the address.
        /// </returns>
        public override string ToString() {
            StringBuilder address = new StringBuilder();

            address.Append(this.Address1).Append("<br />");

            if (!string.IsNullOrEmpty(this.Address2)) {
                address.Append(this.Address2).Append("<br />");
            }
                       
            address.Append(this.City).Append(", ").Append(this.StProvince).Append(" ").Append(this.PostalCode);

            return address.ToString();
        }
        #endregion

        #region Metadata class for validation
        private class Metadata {
            [Required]
            [StringLength(40)]
            public string Address1 {
                get;
                set;
            }

            [Required]
            [StringLength(30)]
            public string City {
                get;
                set;
            }

            [Required]
            [StringLength(125)]
            public string StProvince {
                get;
                set;
            }

            [Required]
            [StringLength(10)]
            public string PostalCode {
                get;
                set;
            }
        }
        #endregion

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema() {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">
        /// The System.Xml.XmlReader stream from which the object is deserialized.
        /// </param>
        public void ReadXml(System.Xml.XmlReader reader) {
            XElement addressXml = XElement.Load(reader);

            this.ID = addressXml.Attribute("id").Value;
            this.Uri = addressXml.Attribute("uri").Value;

            this.Household = new Household {
                ID = addressXml.Element("household").Attribute("id").Value,
                Uri = addressXml.Element("household").Attribute("uri").Value
            };

            this.Person = new Person {
                ID = addressXml.Element("person").Attribute("id").Value,
                Uri = addressXml.Element("person").Attribute("uri").Value
            };

            this.AddressType = new AddressType {
                Name = addressXml.Element("addressType").Element("name").Value,
                ID = addressXml.Element("addressType").Attribute("id").Value,
                Uri = addressXml.Element("addressType").Attribute("uri").Value,
            };

            this.Address1 = addressXml.Element("address1").Value;
            this.Address2 = addressXml.Element("address2").Value;
            this.Address3 = addressXml.Element("address3").Value;
            this.City = addressXml.Element("city").Value;
            this.PostalCode = addressXml.Element("postalCode").Value;
            this.County = addressXml.Element("county").Value;
            this.Country = addressXml.Element("country").Value;
            this.StProvince = addressXml.Element("stProvince").Value;
            this.CarrierRoute = addressXml.Element("carrierRoute").Value;
            this.DeliveryPoint = addressXml.Element("deliveryPoint").Value;
            this.AddressDate = !string.IsNullOrEmpty(addressXml.Element("addressDate").Value) ? (DateTime?)DateTime.Parse(addressXml.Element("addressDate").Value) : null;
            this.AddressComment = addressXml.Element("addressComment").Value;
            this.UspsVerified = addressXml.Element("uspsVerified").Value;
            this.AddressVerifiedDate = !string.IsNullOrEmpty(addressXml.Element("addressVerifiedDate").Value) ? (DateTime?)DateTime.Parse(addressXml.Element("addressVerifiedDate").Value) : null;
            this.LastVerificationAttemptDate = !string.IsNullOrEmpty(addressXml.Element("lastVerificationAttemptDate").Value) ? (DateTime?)DateTime.Parse(addressXml.Element("lastVerificationAttemptDate").Value) : null;

            this.CreatedDate = !string.IsNullOrEmpty(addressXml.Element("createdDate").Value) ? (DateTime?)DateTime.Parse(addressXml.Element("createdDate").Value) : null;
            this.LastUpdatedDate = !string.IsNullOrEmpty(addressXml.Element("lastUpdatedDate").Value) ? (DateTime?)DateTime.Parse(addressXml.Element("lastUpdatedDate").Value) : null;
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">
        /// The System.Xml.XmlWriter stream to which the object is serialized.
        /// </param>
        public void WriteXml(System.Xml.XmlWriter writer) {
            // Address attributes.
            writer.WriteAttributeString("id", this.ID);
            writer.WriteAttributeString("uri", this.Uri);

            // Household element.
            writer.WriteStartElement("household");
            writer.WriteAttributeString("id", this.Household.ID);
            writer.WriteAttributeString("uri", this.Household.Uri);
            writer.WriteEndElement();

            // Person element.
            writer.WriteStartElement("person");
            writer.WriteAttributeString("id", this.Person.ID);
            writer.WriteAttributeString("uri", this.Person.Uri);
            writer.WriteEndElement();

            // AddressType element.
            writer.WriteStartElement("addressType");
            writer.WriteAttributeString("id", this.AddressType.ID);
            writer.WriteAttributeString("uri", this.AddressType.Uri);
            // Name child element.
            writer.WriteStartElement("name");
            writer.WriteValue(this.AddressType.Name);
            writer.WriteEndElement();
            writer.WriteEndElement();

            // Address1 element.
            writer.WriteStartElement("address1");
            writer.WriteValue(this.Address1);
            writer.WriteEndElement();

            // Address2 element.
            writer.WriteStartElement("address2");
            if (!string.IsNullOrEmpty(this.Address2)) {
                writer.WriteValue(this.Address2);
            }
            writer.WriteEndElement();

            // Address3 element.
            writer.WriteStartElement("address3");
            if (!string.IsNullOrEmpty(this.Address3)) {
                writer.WriteValue(this.Address3);
            }
            writer.WriteEndElement();

            // City element.
            writer.WriteStartElement("city");
            writer.WriteValue(this.City);
            writer.WriteEndElement();

            // PostalCode element.
            writer.WriteStartElement("postalCode");
            writer.WriteValue(this.PostalCode);
            writer.WriteEndElement();

            // County element.
            writer.WriteStartElement("county");
            writer.WriteValue(this.County);
            writer.WriteEndElement();

            // Country element.
            writer.WriteStartElement("country");
            writer.WriteValue(this.Country);
            writer.WriteEndElement();

            // StProvince element.
            writer.WriteStartElement("stProvince");
            writer.WriteValue(this.StProvince);
            writer.WriteEndElement();

            // CarrierRoute element.
            writer.WriteStartElement("carrierRoute");
            writer.WriteValue(this.CarrierRoute);
            writer.WriteEndElement();

            // DeliveryPoint element.
            writer.WriteStartElement("deliveryPoint");
            writer.WriteValue(this.DeliveryPoint);
            writer.WriteEndElement();

            // AddressDate element.
            writer.WriteStartElement("addressDate");
            if (this.AddressDate != null) {
                writer.WriteValue(this.AddressDate.Value);
            }
            writer.WriteEndElement();

            // AddressComment element.
            writer.WriteStartElement("addressComment");
            writer.WriteValue(this.AddressComment);
            writer.WriteEndElement();

            // UspsVerified element.
            writer.WriteStartElement("uspsVerified");
            writer.WriteValue(this.UspsVerified);
            writer.WriteEndElement();

            // AddressVerifiedDate element.
            writer.WriteStartElement("addressVerifiedDate");
            if (this.AddressVerifiedDate != null) {
                writer.WriteValue(this.AddressVerifiedDate.Value);
            }
            writer.WriteEndElement();

            // LastVerificationAttemptDate element.
            writer.WriteStartElement("lastVerificationAttemptDate");
            if (this.LastVerificationAttemptDate != null) {
                writer.WriteValue(this.LastVerificationAttemptDate.Value);
            }
            writer.WriteEndElement();

            // CreatedDate element.
            writer.WriteStartElement("createdDate");
            if (this.CreatedDate != null) {
                writer.WriteValue(this.CreatedDate.Value);
            }
            writer.WriteEndElement();

            // LastUpdatedDate element.
            writer.WriteStartElement("lastUpdatedDate");
            if (this.LastUpdatedDate != null) {
                writer.WriteValue(this.LastUpdatedDate.Value);
            }
            writer.WriteEndElement();
        }

        #endregion
    }
}
