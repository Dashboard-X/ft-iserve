using System.Xml.Serialization;
using iServe.Models.F1APIServices;
using iServe.Models.dotNailsCommon;
using System;

namespace iServe.Models {
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata))]
	public partial class Person : IF1APIResource {
        #region Properties
        [XmlIgnore]
        public Address Address {
            get;
            set;
        }

        [XmlIgnore]
        public string IndividualEmailID {
            get;
            set;
        }

        [XmlIgnore]
        public string IndividualEmail {
            get;
            set;
        }

        [XmlIgnore]
        public string IndividualPhoneNumberID {
            get;
            set;
        }

        [XmlIgnore]
        public string IndividualPhoneNumber {
            get;
            set;
        }

        [XmlIgnore]
        public string HouseholdEmailID {
            get;
            set;
        }

        [XmlIgnore]
        public string HouseholdEmail {
            get;
            set;
        }

        [XmlIgnore]
        public string HouseholdPhoneNumberID {
            get;
            set;
        }

        [XmlIgnore]
        public string HouseholdPhoneNumber {
            get;
            set;
        }
        #endregion

        #region Metadata class for validation
        private class Metadata {
            [Required]
            [StringLength(30)]
            public string FirstName {
                get;
                set;
            }

            [Required]
            [StringLength(30)]
            public string LastName {
                get;
                set;
            }

            [Required]
            public DateTime DateOfBirth {
                get;
                set;
            }

            [StringLength(50)]
            public string IndividualEmail {
                get;
                set;
            }

            [StringLength(50)]
            public string IndividualPhoneNumber {
                get;
                set;
            }

            [StringLength(50)]
            public string HouseholdEmail {
                get;
                set;
            }

            [StringLength(50)]
            public string HouseholdPhoneNumber {
                get;
                set;
            }
        }
        #endregion
    }
}
