using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iServe.Models.dotNailsCommon {
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class RequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute, IDotNailsValidationAttribute {
		public RunWhenEnum RunWhen { get; set; }

		public RequiredAttribute() : this(RunWhenEnum.Always) { }

		public RequiredAttribute(RunWhenEnum runWhen)
			: base() {
			RunWhen = runWhen;
		}

		public override bool IsValid(object value) {
			if (value == null) {
				return false;
			}
			string str = value as string;
			if (str != null) {
				return (str.Trim().Length != 0);
			}
			if (value.GetType() == typeof(int)) {
				// Treat Int32.MinValue as invalid (not supplied) for required integers
				int integer = (int)value;
				if (integer == Int32.MinValue) {
					return false;
				}
			}
			return true;
		}
	}

	public class StringLength : System.ComponentModel.DataAnnotations.StringLengthAttribute {
		public StringLength(int maxLength) : base(maxLength) { }
	}
}
