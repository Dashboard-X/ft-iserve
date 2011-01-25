using System;

namespace iServe.Models.dotNailsCommon {
	public interface IDotNailsValidationAttribute {
		RunWhenEnum RunWhen { get; }
	}
}
