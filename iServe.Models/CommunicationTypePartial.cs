using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iServe.Models {
    public partial class CommunicationType {
        public static readonly KeyValuePair<string, int> HouseholdPhoneNumber = new KeyValuePair<string, int>("HouseholdPhoneNumber", 1);
        public static readonly KeyValuePair<string, int> IndividualPhoneNumber = new KeyValuePair<string, int>("IndividualPhoneNumber", 3);
        public static readonly KeyValuePair<string, int> IndividualEmail = new KeyValuePair<string, int>("IndividualEmail", 4);
        public static readonly KeyValuePair<string, int> HouseholdEmail = new KeyValuePair<string, int>("HouseholdEmail", 4);
    }
}
