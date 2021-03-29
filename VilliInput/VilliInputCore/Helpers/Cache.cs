using System;
using System.Collections;
using System.Collections.Generic;

namespace VilliInput.Helpers
{

    public class Cache<K, V> : IEnumerable<(K, V, int)>, IEnumerable
    {

        public List<K> Order;

        public Dictionary<K, V> Values;

        public ulong version;

        public Cache(int capacity = 16)
        {
            Order = new List<K>(capacity);
            Values = new Dictionary<K, V>(capacity);
        }

        public int Count => Order.Count;

        IEnumerator<(K, V, int)> IEnumerable<(K, V, int)>.GetEnumerator()
        {
            return InternalGetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InternalGetEnumerator();
        }

        public void Clear()
        {
            Order.Clear();
            Values.Clear();

            version = 0;
        }

        public int? AddItem(K key, V value, int index = int.MaxValue, bool forceAdd = false)
        {
            if (Values.ContainsKey(key))
            {
                if (!forceAdd)
                {
                    return null;
                }

                Values[key] = value;
                version++;
                return GetIndexForKey(key);
            }

            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (index >= Count)
            {
                Order.Add(key);
                Values.Add(key, value);
                version++;
                return Order.Count - 1;
            }

            Order.Insert(index, key);
            Values.Add(key, value);
            version++;
            return index;
        }

        public (K, V, int) GetItem(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var key = Order[index];
            return (key, Values[key], index);
        }

        public (K, V, int) GetItem(K key)
        {
            V value;
            if (!Values.TryGetValue(key, out value))
            {
                throw new KeyNotFoundException();
            }

            var index = GetIndexForKey(key);
            return (key, Values[key], index);
        }

        public int GetIndexForKey(K key)
        {
            return Order.FindIndex(k => k.Equals(key));
        }

        public (K, V, int) RemoveItem(K key)
        {
            var item = GetItem(key);
            Values.Remove(key);
            Order.RemoveAt(item.Item3);
            version++;

            return item;
        }

        public (K, V, int) RemoveItem(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var key = Order[index];
            var value = Values[key];

            Values.Remove(key);
            Order.RemoveAt(index);
            version++;

            return (key, value, index);
        }

        private IEnumerator<(K, V, int)> GetEnumerator()
        {
            return InternalGetEnumerator();
        }


        private IEnumerator<(K, V, int)> InternalGetEnumerator()
        {
            var enumeratorVersion = version;
            for (var i = 0; i < Count; i++)
            {
                if (enumeratorVersion != version)
                {
                    throw new InvalidOperationException("Collection modified");
                }

                var key = Order[i];
                yield return (key, Values[key], i);
            }
        }

        /*
        public class Enumerator : IEnumerator<(K, V, int)>
        {
            private Cache<K, V> _stack;

            private int _index;
            private ulong _version;

            internal Enumerator(Cache<K, V> stack)
            {
                _index = 0;
                _stack = stack;
                _version = stack.version;
            }

            private bool disposedValue;

            public (K, V, int) Current
            {
                get
                {
                    if (_index < 0) throw new InvalidOperationException("Enumerator Ended");

                    return _stack.GetItem(_index);
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if (_index < 0) throw new InvalidOperationException("Enumerator Ended");

                    return _stack.GetItem(_index);
                }
            }

            public bool MoveNext()
            {
                if (_version != _stack.version) throw new InvalidOperationException("Collection modified");
                _index++;

                // End of enumeration.
                if (_index < 0)
                {
                    return false;
                }
                return true;
            }

            public void Reset()
            {
                if (_version != _stack.version) new InvalidOperationException("Collection modified");
                _index = 0;
            }

            protected virtual void Dispose(bool disposing)
            {
                _index = -1;
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects)
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                    // TODO: set large fields to null
                    disposedValue = true;
                }
            }

            // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
            // ~Enumerator()
            // {
            //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            //     Dispose(disposing: false);
            // }

            public void Dispose()
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
        */

    }

}
