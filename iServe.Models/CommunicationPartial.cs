using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iServe.Models.F1APIServices;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace iServe.Models {
    public partial class Communication : IF1APIResource, IXmlSerializable {

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
            XElement communicationXml = XElement.Load(reader);

            this.ID = communicationXml.Attribute("id").Value;
            this.Uri = communicationXml.Attribute("uri").Value;

            this.Household = new Household {
                ID = communicationXml.Element("household").Attribute("id").Value,
                Uri = communicationXml.Element("household").Attribute("uri").Value
            };

            this.Person = new Person {
                ID = communicationXml.Element("person").Attribute("id").Value,
                Uri = communicationXml.Element("person").Attribute("uri").Value
            };

            this.CommunicationType = new CommunicationType {
                Name = communicationXml.Element("communicationType").Element("name").Value,
                ID = communicationXml.Element("communicationType").Attribute("id").Value,
                Uri = communicationXml.Element("communicationType").Attribute("uri").Value,
            };

            this.CommunicationGeneralType = communicationXml.Element("communicationGeneralType").Value;
            this.CommunicationValue = communicationXml.Element("communicationValue").Value;
            this.SearchCommunicationValue = communicationXml.Element("searchCommunicationValue").Value;
            this.Listed = bool.Parse(communicationXml.Element("listed").Value);
            this.CommunicationComment = communicationXml.Element("communicationComment").Value;
            this.CreatedDate = !string.IsNullOrEmpty(communicationXml.Element("createdDate").Value) ? (DateTime?)DateTime.Parse(communicationXml.Element("createdDate").Value) : null;
            this.LastUpdatedDate = !string.IsNullOrEmpty(communicationXml.Element("lastUpdatedDate").Value) ? (DateTime?)DateTime.Parse(communicationXml.Element("lastUpdatedDate").Value) : null;
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">
        /// The System.Xml.XmlWriter stream to which the object is serialized.
        /// </param>
        public void WriteXml(System.Xml.XmlWriter writer) {
            // Communication attributes.
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

            // CommunicationType element.
            writer.WriteStartElement("communicationType");
            writer.WriteAttributeString("id", this.CommunicationType.ID);
            writer.WriteAttributeString("uri", this.CommunicationType.Uri);
            // Name child element
            writer.WriteStartElement("name");
            if (this.CommunicationType.Name != null) {
                writer.WriteValue(this.CommunicationType.Name);
            }
            writer.WriteEndElement();
            writer.WriteEndElement();

            // CommunicationGeneralType element.
            writer.WriteStartElement("communicationGeneralType");
            if (this.CommunicationGeneralType != null) {
                writer.WriteValue(this.CommunicationGeneralType);
            }
            writer.WriteEndElement();

            // CommunicationValue element.
            writer.WriteStartElement("communicationValue");
            writer.WriteValue(this.CommunicationValue);
            writer.WriteEndElement();

            // SearchCommunicationValue element.
            writer.WriteStartElement("searchCommunicationValue");
            if (this.SearchCommunicationValue != null) {
                writer.WriteValue(this.SearchCommunicationValue);
            }
            writer.WriteEndElement();

            // Listed element.
            writer.WriteStartElement("listed");
            writer.WriteValue(this.Listed);
            writer.WriteEndElement();

            // CommunicationComment element.
            writer.WriteStartElement("communicationComment");
            if (this.CommunicationComment != null) {
                writer.WriteValue(this.CommunicationComment);
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
