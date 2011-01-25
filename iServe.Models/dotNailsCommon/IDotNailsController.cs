using System;

namespace iServe.Models.dotNailsCommon {
	public interface IDotNailsController {
		IEntity CreateModelEntityUsingDefaultFactory(Type type);
	}
}
