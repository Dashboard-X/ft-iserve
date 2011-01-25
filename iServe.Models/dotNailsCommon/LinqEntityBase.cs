using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
//using xVal.ServerSide;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iServe.Models.dotNailsCommon {
	[DataContract(IsReference=true)]
	public class LinqEntityBase<EntityType, DataContextType> : IEntity
																where EntityType : LinqEntityBase<EntityType, DataContextType>, new()
																where DataContextType : System.Data.Linq.DataContext {
		private EntityStateEnum _entityState = EntityStateEnum.DefaultLinqToSql;
		public EntityStateEnum EntityState {
			get { return _entityState; }
			set { _entityState = value; } 
		}
		public int? CurrentUserID { get; set; }
		internal bool IsSaveInProgress { get; set; }
		internal bool IsMappingForeignKeys { get; set; }
		//protected int? UserID {
		//    get { 
		//        int? userID = null;
		//        System.Security.Principal.IPrincipal principal = System.Threading.Thread.CurrentPrincipal;
		//        if (principal != null) {
		//            IdotNailsIdentity identity = principal.Identity as IdotNailsIdentity;
		//            if (identity != null) {
		//                userID = identity.ID;
		//            }
		//        }
		//        return userID; 
		//    }
		//}
		
		protected DataContextType _db;

		#region Extensibility Method Definitions
		protected virtual void ValidateBusinessRules() { }
		#endregion
		
		public virtual void SetDataContext(System.Data.Linq.DataContext dataContext) {
			DataContextType db = dataContext as DataContextType;

			if (dataContext != null) {
				_db = db;
			}
			else {
				throw new Exception("SetDataContext(dataContext) for " + typeof(EntityType) + " did not receive parameter of type " + typeof(DataContextType));
			}
		}

		public virtual IEntity Save() {
			return Save(null, null);
		}
		
		public virtual IEntity Save(System.Data.Linq.DataContext dataContext) {
			return Save(null, dataContext);
		}

		public virtual IEntity Save(IEntity parent, System.Data.Linq.DataContext dataContext) {
			//var errors = DataAnnotationsValidationRunner.GetErrors(this);
			//if (errors.Any())
			//    throw new RulesException(errors);

			// Extensibility hook for derived classes
			//ValidateBusinessRules();
			
			IEntity topMostParent = parent ?? this;
			if (dataContext != null) {
				_db = dataContext as DataContextType;
			}
			
			if (EntityState == EntityStateEnum.DefaultLinqToSql && topMostParent == this) {
				// Nothing fancy, just let Linq to Sql do its thing
				_db.SubmitChanges();
				return null;
			}
			else {
				// Do our dotNails thing
				EntityType clone = null;
				IsSaveInProgress = true;

				DetermineEntityState();
				IAuditable auditableEntity = this as IAuditable;
						
				// Handle this current entity
				switch (EntityState) {
					case EntityStateEnum.Added:
					case EntityStateEnum.DefaultLinqToSql: // this can occur for ManyToMany tables (always treat them as Added)
						if (auditableEntity != null) {
							auditableEntity.AuditForCreate(CurrentUserID);
						}
						clone = InsertEntity(topMostParent) as EntityType;
						break;
					case EntityStateEnum.Modified:
						if (auditableEntity != null) {
							auditableEntity.AuditForUpdate(CurrentUserID);
						}
						clone = UpdateEntity(topMostParent) as EntityType;
						break;
					case EntityStateEnum.Deleted:
						clone = DeleteEntity(topMostParent) as EntityType;
						break;
					case EntityStateEnum.Destroyed:
						clone = DestroyEntity(topMostParent) as EntityType;
						break;
					case EntityStateEnum.UnModified:
						// While we don't have anything to perform on this entity, we may need to mess with his children
						clone = UnModifiedEntity(topMostParent) as EntityType;
						break;
				}

				if (parent == null) {
					// We're the entity with the originating request to save, so submit all the changes on the datacontext
					try {
						_db.SubmitChanges();
						MapChildForeignKeys();
					}
					catch (Exception ex) {
						throw (ex);
					}
				}

				IsSaveInProgress = false;
				
				return clone;
			}
		}
		
		public virtual EntityType GetSerializableObject() {
			return Clone(EntityStateEnum.NotSet);
		}

		// HACK: This exists because of a troubling mystery with LinqToSql.  When a hierarchy of objects gets inserted, the children's foreign key propertyID that points
		//  to their parent EntityRef somehow magically gets set by LinqToSql without calling the property setter for that property!!!!  Honest to God, I have no idea how 
		//  this is even possible in C#.  The underlying variables are private and there's no constructor that I know of that can set them.  The result is that our facing 
		//  objects never get notified that their clones have new IDs on those properties.  The clones have the foreign key set, but the facing objects never hear about it.  
		//  This method is a hack for all children to make sure they have the newly inserted IDs of their parents.
		public virtual void MapChildForeignKeys() { }

		protected virtual IEntity InsertEntity(IEntity parent) {
			throw new NotImplementedException();
		}

		protected virtual IEntity UpdateEntity(IEntity parent) {
			throw new NotImplementedException();
		}

		protected virtual IEntity DeleteEntity(IEntity parent) {
			throw new NotImplementedException();
		}

		protected virtual IEntity DestroyEntity(IEntity parent) {
			throw new NotImplementedException();
		}
		
		protected virtual IEntity UnModifiedEntity(IEntity parent) {
			throw new NotImplementedException();
		}

		public EntityType Clone() {
			return Clone(EntityStateEnum.Added);
		}

		public virtual EntityType Clone(EntityStateEnum entityState) {
			throw new NotImplementedException();
		}

		public EntityType ClonePrimaryKeyOnly() {
			return ClonePrimaryKeyOnly(EntityStateEnum.Modified);
		}
		
		public virtual EntityType ClonePrimaryKeyOnly(EntityStateEnum entityState) {
			throw new NotImplementedException();
		}

		protected virtual void DetermineEntityState() {
			throw new NotImplementedException();
		}
	}

	//internal static class DataAnnotationsValidationRunner {
	//    public static IEnumerable<ErrorInfo> GetErrors(object instance) {
	//        List<ErrorInfo> errors = new List<ErrorInfo>();

	//        IEnumerable<PropertyDescriptor> properties = TypeDescriptor.GetProperties(instance).Cast<PropertyDescriptor>();
	//        foreach (PropertyDescriptor prop in properties) {
	//            IEnumerable<ValidationAttribute> attributes = prop.Attributes.OfType<ValidationAttribute>();
	//            foreach (ValidationAttribute attribute in attributes) {
	//                if (!attribute.IsValid(prop.GetValue(instance))) {
	//                    errors.Add(new ErrorInfo(prop.Name, attribute.FormatErrorMessage(string.Empty), instance));
	//                }
	//            }
	//        }
	//        return errors;

	//        //return from prop in TypeDescriptor.GetProperties(instance).Cast<PropertyDescriptor>()
	//        //       from attribute in prop.Attributes.OfType<ValidationAttribute>()
	//        //       where !attribute.IsValid(prop.GetValue(instance))
	//        //       select new ErrorInfo(prop.Name, attribute.FormatErrorMessage(string.Empty), instance);
	//    }
	//}
}
