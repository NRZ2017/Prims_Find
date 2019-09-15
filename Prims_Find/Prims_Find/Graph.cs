using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Prims_Find
{
    class Graph<T>
    {
        public List<Vertex<T>> Vertices = new List<Vertex<T>>();
        public int EdgeCount { get; private set; }

        public Graph<Point> this[int index]
        {
            get
            {
                return null;
            }
        }

        public Graph()
        {
        }

        public void AddVertex(Vertex<T> vert)
        {
            Vertices.Add(vert);
        }

        public bool RemoveVertex(Vertex<T> vertex)
        {
            foreach (var vert in Vertices)
            {
                vert.Edges.Remove(vertex);
            }
            return Vertices.Remove(vertex);
        }

        public void AddEdge(T a, T b, double weight)
        {
            //find the vertex with value a and b;
            var x = GetVertex(a);
            var y = GetVertex(b);

            if (x == null || y == null)
            {
                throw new Exception("verticies not found");
            }

            AddEdge(x, y, weight);
        }

        public void AddEdge(Vertex<T> a, Vertex<T> b, double weight)
        {
            a.Edges.Add(b, weight);
            b.Edges.Add(a, weight);
        }
        public void AddDirectedEdge(Vertex<T> start, Vertex<T> end, double weight)
        {
            start.Edges.Add(end, weight);
        }
        public Vertex<T> GetVertex(T value)
        {
            foreach (var vert in Vertices)
            {
                if (vert.Value.Equals(value))
                {
                    return vert;
                }
            }
            return null;
        }
        public void RemoveEdge(Vertex<T> A, Vertex<T> B)
        {
            A.Edges.Remove(B);
            B.Edges.Remove(A);
        }

        public static Graph<Point> Maze(int height, int width, int seed)
        {

            var graph = new Graph<Point>();

            var frontier = new HashSet<Vertex<Point>>();

            var info = new Dictionary<Vertex<Point>, HashSet<Vertex<Point>>>();

            Random gen = new Random(seed);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    graph.AddVertex(new Vertex<Point>(new Point(x, y)));
                    info.Add(graph[graph.Vertices.Count - 1], new HashSet<Vertex<Point>>());
                }
            }

            var begin = graph[gen.Next(graph.EdgeCount)];
            begin.Vertices[0].IsVisited = true;

            

            return graph;
        }
    }
}
