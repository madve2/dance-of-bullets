using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoB.Xaml
{
	public class CustomDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		public TValue this[TKey key]
		{
			get
			{
				return store[key];
			}
			set
			{
				store[key] = value;
				OnKeyAssigned( key );
			}
		}

		protected virtual void OnKeyAssigned(TKey key)
		{
		}

		protected virtual void OnKeyRemoved( TKey key )
		{
		}

		protected virtual void OnCleared( )
		{
		}

		private Dictionary<TKey, TValue> store = new Dictionary<TKey, TValue>();

		#region IDictionary<TKey,TValue> Members

		void IDictionary<TKey,TValue>.Add( TKey key, TValue value )
		{
			store.Add( key, value );
			OnKeyAssigned( key );
		}

		bool IDictionary<TKey,TValue>.ContainsKey( TKey key )
		{
			return store.ContainsKey( key );
		}

		ICollection<TKey> IDictionary<TKey,TValue>.Keys
		{
			get { return store.Keys; }
		}

		bool IDictionary<TKey,TValue>.Remove( TKey key )
		{
			var result = store.Remove( key );
			if (result)
				OnKeyRemoved( key );
			return result;
		}

		bool IDictionary<TKey,TValue>.TryGetValue( TKey key, out TValue value )
		{
			return store.TryGetValue( key, out value );
		}

		ICollection<TValue> IDictionary<TKey,TValue>.Values
		{
			get { return store.Values; }
		}

		#endregion

		#region ICollection<KeyValuePair<TKey,TValue>> Members

		void ICollection<KeyValuePair<TKey,TValue>>.Add( KeyValuePair<TKey, TValue> item )
		{
			((ICollection<KeyValuePair<TKey, TValue>>)store).Add( item );
			OnKeyAssigned( item.Key );
		}

		void ICollection<KeyValuePair<TKey,TValue>>.Clear()
		{
			store.Clear();
			OnCleared();
		}

		bool ICollection<KeyValuePair<TKey,TValue>>.Contains( KeyValuePair<TKey, TValue> item )
		{
			return store.Contains( item );
		}

		void ICollection<KeyValuePair<TKey,TValue>>.CopyTo( KeyValuePair<TKey, TValue>[] array, int arrayIndex )
		{
			( (ICollection<KeyValuePair<TKey, TValue>>)store ).CopyTo( array, arrayIndex );
		}

		int ICollection<KeyValuePair<TKey,TValue>>.Count
		{
			get { return store.Count; }
		}

		bool ICollection<KeyValuePair<TKey,TValue>>.IsReadOnly
		{
			get { return ((ICollection<KeyValuePair<TKey, TValue>>)store ).IsReadOnly; }
		}

		bool ICollection<KeyValuePair<TKey,TValue>>.Remove( KeyValuePair<TKey, TValue> item )
		{
			var result = ( (ICollection<KeyValuePair<TKey, TValue>>)store ).Remove( item );
			if (result)
				OnKeyRemoved( item.Key );
			return result;
		}

		#endregion

		#region IEnumerable<KeyValuePair<TKey,TValue>> Members

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey,TValue>>.GetEnumerator()
		{
			return store.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return store.GetEnumerator();
		}

		#endregion
	}
}
