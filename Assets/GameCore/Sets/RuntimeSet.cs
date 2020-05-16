using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Sets
{
    // TODO-GC: write an editor script for showing the values of objects within set.
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        public List<T> Items = new List<T>();

        public void Add(T item)
        {
            if (!Items.Contains(item))
                Items.Add(item);
        }

        public void Remove(T item)
        {
            if (Items.Contains(item))
                Items.Remove(item);
        }
    }
}
