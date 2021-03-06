﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=2.0.50727.42.
// 

namespace iServe.Models {
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "household", Namespace = "", IsNullable = false)]
    public partial class Household {

        private string uriField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "uri", DataType = "anyURI")]
        public string Uri {
            get {
                return this.uriField;
            }
            set {
                this.uriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "id")]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "addressType", Namespace = "", IsNullable = false)]
    public partial class AddressType {
        private string nameField;

        private string uriField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "name")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "uri", DataType = "anyURI")]
        public string Uri {
            get {
                return this.uriField;
            }
            set {
                this.uriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "id", DataType = "integer")]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "address", Namespace = "", IsNullable = false)]
    public partial class Address {

        private Household householdField;

        private Person personField;

        private AddressType addressTypeField;

        private string address1Field;

        private string address2Field;

        private string address3Field;

        private string cityField;

        private string postalCodeField;

        private string countyField;

        private string countryField;

        private string stProvinceField;

        private string carrierRouteField;

        private string deliveryPointField;

        private System.Nullable<System.DateTime> addressDateField;

        private string addressCommentField;

        private string uspsVerifiedField;

        private System.Nullable<System.DateTime> addressVerifiedDateField;

        private System.Nullable<System.DateTime> lastVerificationAttemptDateField;

        private System.Nullable<System.DateTime> createdDateField;

        private System.Nullable<System.DateTime> lastUpdatedDateField;

        private string uriField;

        private string idField;

        public Address() {
            this.addressDateField = new System.DateTime(0);
            this.addressVerifiedDateField = new System.DateTime(0);
            this.lastVerificationAttemptDateField = new System.DateTime(0);
            this.createdDateField = new System.DateTime(0);
            this.lastUpdatedDateField = new System.DateTime(0);
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "household")]
        public Household Household {
            get {
                return this.householdField;
            }
            set {
                this.householdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "person")]
        public Person Person {
            get {
                return this.personField;
            }
            set {
                this.personField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "addressType")]
        public AddressType AddressType {
            get {
                return this.addressTypeField;
            }
            set {
                this.addressTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "address1")]
        public string Address1 {
            get {
                return this.address1Field;
            }
            set {
                this.address1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "address2", IsNullable = true)]
        public string Address2 {
            get {
                return this.address2Field;
            }
            set {
                this.address2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "address3", IsNullable = true)]
        public string Address3 {
            get {
                return this.address3Field;
            }
            set {
                this.address3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "city", IsNullable = true)]
        public string City {
            get {
                return this.cityField;
            }
            set {
                this.cityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "postalCode", IsNullable = true)]
        public string PostalCode {
            get {
                return this.postalCodeField;
            }
            set {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "county", IsNullable = true)]
        public string County {
            get {
                return this.countyField;
            }
            set {
                this.countyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "country", IsNullable = true)]
        public string Country {
            get {
                return this.countryField;
            }
            set {
                this.countryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "stProvince", IsNullable = true)]
        public string StProvince {
            get {
                return this.stProvinceField;
            }
            set {
                this.stProvinceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "carrierRoute", IsNullable = true)]
        public string CarrierRoute {
            get {
                return this.carrierRouteField;
            }
            set {
                this.carrierRouteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "deliveryPoint", IsNullable = true)]
        public string DeliveryPoint {
            get {
                return this.deliveryPointField;
            }
            set {
                this.deliveryPointField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "addressDate", IsNullable = true)]
        public System.Nullable<System.DateTime> AddressDate {
            get {
                return this.addressDateField;
            }
            set {
                this.addressDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "addressComment", IsNullable = true)]
        public string AddressComment {
            get {
                return this.addressCommentField;
            }
            set {
                this.addressCommentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "uspsVerified", IsNullable = true)]
        public string UspsVerified {
            get {
                return this.uspsVerifiedField;
            }
            set {
                this.uspsVerifiedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "addressVerifiedDate", IsNullable = true)]
        public System.Nullable<System.DateTime> AddressVerifiedDate {
            get {
                return this.addressVerifiedDateField;
            }
            set {
                this.addressVerifiedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "lastVerificationAttemptDate", IsNullable = true)]
        public System.Nullable<System.DateTime> LastVerificationAttemptDate {
            get {
                return this.lastVerificationAttemptDateField;
            }
            set {
                this.lastVerificationAttemptDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "createdDate", IsNullable = true)]
        public System.Nullable<System.DateTime> CreatedDate {
            get {
                return this.createdDateField;
            }
            set {
                this.createdDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "lastUpdatedDate", IsNullable = true)]
        public System.Nullable<System.DateTime> LastUpdatedDate {
            get {
                return this.lastUpdatedDateField;
            }
            set {
                this.lastUpdatedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "uri", DataType = "anyURI")]
        public string Uri {
            get {
                return this.uriField;
            }
            set {
                this.uriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "id")]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
}