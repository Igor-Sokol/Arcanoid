using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Application.Scripts.Library.EnumerableSafeCollections
{
    public class SafeList<T> : IEnumerable<T>
        where T : class
    {
        private readonly List<T> _list = new List<T>();
        private readonly List<T> _listToAdd = new List<T>();

        private bool _needClean;
        private bool _executing;

        public int Count => _list.Count(i => i != null) + _listToAdd.Count;

        public void Add(T item)
        {
            if (_executing)
            {
                _listToAdd.Add(item);
                _needClean = true;
            }
            else
            {
                _list.Add(item);
            }
        }

        public void Remove(T item)
        {
            if (_executing)
            {
                var i = _list.IndexOf(item);
                if (i >= 0)
                {
                    _list[i] = null;
                    _needClean = true;
                }
            }
            else
            {
                _list.Remove(item);
            }
        }

        public void Clear()
        {
            if (_executing)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    _list[i] = null;
                }

                _needClean = true;
            }
            else
            {
                _list.Clear();
            }
            
            _listToAdd.Clear();
        }
        
        private IEnumerator<T> GetItems()
        {
            _executing = true;
            foreach (var item in _list)
            {
                if (item != null)
                {
                    yield return item;
                }
            }
            _executing = false;

            CleanCollection();
        }

        private void CleanCollection()
        {
            if (!_needClean) return;

            _list.RemoveAll(i => i is null);
            _list.AddRange(_listToAdd);
            _listToAdd.Clear();
            
            _needClean = false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetItems();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}