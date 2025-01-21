using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNE03
{
    public class Set
    {
        private HashSet<int> elements;

        public Set()
        {
            elements = new HashSet<int>();
        }

        public void Add(int i)
        {
            elements.Add(i);
        }

        public bool Contains(int i)
        {
            return elements.Contains(i);
        }

        public void Remove(int i)
        {
            elements.Remove(i);
        }

        public int Size()
        {
            return elements.Count;
        }

        public Set Union(Set other)
        {
            Set resultSet = new Set();
            resultSet.elements.UnionWith(this.elements);
            resultSet.elements.UnionWith(other.elements);
            return resultSet;
        }

        public Set Intersection(Set other)
        {
            Set resultSet = new Set();
            resultSet.elements.UnionWith(this.elements);
            resultSet.elements.IntersectWith(other.elements);
            return resultSet;
        }

        public Set Subtract(Set other)
        {
            Set resultSet = new Set();
            resultSet.elements.UnionWith(this.elements);
            resultSet.elements.ExceptWith(other.elements);
            return resultSet;
        }

        public bool IsSubsetOf(Set other)
        {
            return this.elements.IsSubsetOf(other.elements);
        }
    }

}
