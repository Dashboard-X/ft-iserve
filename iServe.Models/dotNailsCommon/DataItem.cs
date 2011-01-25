using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iServe.Models.dotNailsCommon {
	/// <summary>
	/// Summary description for DataItem.
	/// </summary>
	[Serializable]
	public struct DataItem {
		private string _text;
		private string _value;

		public string Text {
			get { return _text; }
			set { _text = value; }
		}

		public string Value {
			get { return _value; }
			set { _value = value; }
		}

		public DataItem(string text) {
			_text = text;
			_value = text;
		}

		public DataItem(string text, string value) {
			_text = text;
			_value = value;
		}

	}
}
