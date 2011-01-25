// Many thanks to Mitsu Furuta and his excellent Many-to-many implementation for Linq-to-Sql - http://code.msdn.microsoft.com/linqtosqlmanytomany
//  I've made mostly cosmetic changes to his implementation
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace iServe.Models.dotNailsCommon {
	public class ManyToManyList<TManyToMany, TMapped> : IList<TMapped>, IList {
		public ManyToManyList(IList<TManyToMany> manyToMany, Func<TManyToMany, TMapped> mappingFunction) {
			this.manyToMany = manyToMany;
			this.mappingFunction = mappingFunction;
			mapped = manyToMany.Select(mappingFunction);
		}
		public ManyToManyList(IList<TManyToMany> manyToMany, Func<TManyToMany, TMapped> mappingFunction, Action<IList<TManyToMany>, TMapped> onAdd, Action<IList<TManyToMany>, TMapped> onRemove)
			: this(manyToMany, mappingFunction) {
			this.onRemove = onRemove;
			this.onAdd = onAdd;
			isReadOnly = false;
		}

		protected IList<TManyToMany> manyToMany;
		protected Func<TManyToMany, TMapped> mappingFunction;
		protected IEnumerable<TMapped> mapped;

		private bool isReadOnly = true;
		protected Action<IList<TManyToMany>, TMapped> onRemove;
		protected Action<IList<TManyToMany>, TMapped> onAdd;

		#region IList<T> Members

		public int IndexOf(TMapped item) {
			int i = 0;
			foreach (TMapped t in mapped) {
				if (t.Equals(item))
					return i;
				i++;
			}
			return -1;
		}

		public void Insert(int index, TMapped item) {
			throw new Exception("The method or operation is not implemented.");
		}

		public void RemoveAt(int index) {
			throw new Exception("The method or operation is not implemented.");
		}

		public TMapped this[int index] {
			get {
				return mappingFunction(manyToMany[index]);
			}
			set {
				throw new Exception("The method or operation is not implemented.");
			}
		}

		#endregion

		#region ICollection<T> Members

		public void Add(TMapped item) {
			if (onAdd != null)
				onAdd(manyToMany, item);
		}

		public void Clear() {
			throw new Exception("The method or operation is not implemented.");
		}

		public bool Contains(TMapped item) {
			return mapped.Contains(item);
		}

		public void CopyTo(TMapped[] array, int arrayIndex) {
			//source.CopyTo(projection.ToArray(), arrayIndex);
		}

		public int Count {
			get { return manyToMany.Count; }
		}

		public bool IsReadOnly {
			get { return isReadOnly; }
		}

		public bool Remove(TMapped item) {
			if (onRemove != null) {
				onRemove(manyToMany, item);
				return true;
			}
			else
				return false;
		}

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<TMapped> GetEnumerator() {
			return mapped.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return mapped.GetEnumerator();
		}

		#endregion

		#region IList Members

		int IList.Add(object value) {
			Add((TMapped)value);

			return 0;
		}

		void IList.Clear() {
			throw new Exception("The method or operation is not implemented.");
		}

		bool IList.Contains(object value) {
			return Contains((TMapped)value);
		}

		int IList.IndexOf(object value) {
			return IndexOf((TMapped)value);
		}

		void IList.Insert(int index, object value) {
			throw new Exception("The method or operation is not implemented.");
		}

		bool IList.IsFixedSize {
			get { return true; }
		}

		bool IList.IsReadOnly {
			get { return true; }
		}

		void IList.Remove(object value) {
			Remove((TMapped)value);
		}

		void IList.RemoveAt(int index) {
			Remove(this[index]);
		}

		object IList.this[int index] {
			get {
				return this[index];
			}
			set {
				throw new Exception("The method or operation is not implemented.");
			}
		}

		#endregion

		#region ICollection Members

		void ICollection.CopyTo(Array array, int index) {
			//CopyTo(array, index);
		}

		int ICollection.Count {
			get { return this.Count; }
		}

		bool ICollection.IsSynchronized {
			get { return false; }
		}

		object ICollection.SyncRoot {
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion
	}
}
