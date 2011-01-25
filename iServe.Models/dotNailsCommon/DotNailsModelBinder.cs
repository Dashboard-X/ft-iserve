using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.Web.Mvc.DataAnnotations;
using iServe.Models;

namespace iServe.Models.dotNailsCommon {
	public class DotNailsModelBinder : DataAnnotationsModelBinder {
		/// <summary>
		/// For any controller implementing IDotNailsController, this will ask that controller to use its default ModelFactory 
		/// to create the model entity to be passed into the controller action. This results in the proper initialization of
		/// the entity by the factory (most important of which is the setting of the DataContext). For any other controller, the
		/// default binder is used to create the entity.
		/// </summary>
		/// <param name="controllerContext"></param>
		/// <param name="bindingContext"></param>
		/// <param name="modelType"></param>
		/// <returns></returns>
		protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType) {
			object model = null;

			IDotNailsController dotNailsController = controllerContext.Controller as IDotNailsController;
			//Type modelFactoryType = typeof(IModelFactory<>);
			//Type 
			//IModelFactory<iServe.Models.iServeDBProcedures> modelFactory = new IModelFactory<iServe.Models.iServeDBProcedures>();

			if (dotNailsController != null) {
				model = dotNailsController.CreateModelEntityUsingDefaultFactory(modelType);
			}
			else {
				model = base.CreateModel(controllerContext, bindingContext, modelType);
			}

			return model;
		}

		// Overridden to provide RunWhen support to validation attributes. Properties can now be marked for validation only in Create or Update scenarios.
		protected override bool OnPropertyValidating(ControllerContext controllerContext,
													 ModelBindingContext bindingContext,
													 PropertyDescriptor propertyDescriptor,
													 object value) {
			string modelStateKey = CreateSubPropertyName(bindingContext.ModelName, propertyDescriptor.Name);
			var validationContext = new ValidationContext(bindingContext.Model, null, null);
			validationContext.DisplayName = GetDisplayName(propertyDescriptor);

			string action = controllerContext.RouteData.Values["action"].ToString();

			bool result = true;
			ValidationResult validationResult;

			foreach (ValidationAttribute attribute in GetValidationAttributes(propertyDescriptor)) {
				IDotNailsValidationAttribute dotNailsAttribute = attribute as IDotNailsValidationAttribute;
				if (dotNailsAttribute != null && dotNailsAttribute.RunWhen != RunWhenEnum.Always && action != dotNailsAttribute.RunWhen.ToString()) {
					// If the attribute isn't set to always be validated, and the current action doesn't line up with the attribute's RunWhen scenario (create or update),
					//  then don't bother validating this attribute.
					result = true;
				}
				else if (!attribute.TryValidate(value, validationContext, out validationResult)) {
					bindingContext.ModelState.AddModelError(modelStateKey, validationResult.ErrorMessage);
					result = false;
				}
			}

			return result;
		}

		// Copied from DataAnnotationsModelBinder because it was internal and I needed to override OnPropertyValidating above
		protected static string GetDisplayName(PropertyDescriptor descriptor) {
			var displayAttribute = descriptor.GetAttribute<DisplayAttribute>();
			if (displayAttribute != null && !String.IsNullOrEmpty(displayAttribute.Name)) {
				return displayAttribute.Name;
			}

			var displayNameAttribute = descriptor.GetAttribute<DisplayNameAttribute>();
			if (displayNameAttribute != null && !String.IsNullOrEmpty(displayNameAttribute.DisplayName)) {
				return displayNameAttribute.DisplayName;
			}

			return descriptor.Name;
		}

		// Copied from DataAnnotationsModelBinder because it was internal and I needed to override OnPropertyValidating above
		protected static IEnumerable<ValidationAttribute> GetValidationAttributes(PropertyDescriptor propertyDescriptor) {
			IEnumerable<ValidationAttribute> attributes = propertyDescriptor.GetAttributes<ValidationAttribute>();

			// Add an implied RequiredAttribute to non-nullable types if it's not already there
			if (!propertyDescriptor.PropertyType.IsNullable()) {
				if (!attributes.Any(attr => attr is iServe.Models.dotNailsCommon.RequiredAttribute)) {
					attributes = attributes.Concat(new[] { new iServe.Models.dotNailsCommon.RequiredAttribute() });
				}
			}

			return attributes;
		}
	}


}
