using System;
using System.Collections.Generic;
using System.Text;

namespace Prims_Find
{
        public class Vertex<T>
        {

            public T Value;

            public bool IsVisited = false;
            public double TotalDistance = double.PositiveInfinity;
            public Vertex<T> Founder = null;


            public Dictionary<Vertex<T>, double> Edges = new Dictionary<Vertex<T>, double>(); //Dictionary<Vertex<T>, double>

            public int Count => Edges.Count;

            public Vertex(T value)
            {
                this.Value = value;
                Edges = new Dictionary<Vertex<T>, double>();
            }

            public int CompareTo(Vertex<T> other)
            {
                return TotalDistance.CompareTo(other.TotalDistance);
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }
}
