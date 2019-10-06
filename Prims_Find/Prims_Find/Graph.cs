using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Prims_Find
{
    class Graph<T>
    {
        public List<Vertex<T>> Vertices { get; set; }
        public int EdgeCount { get; private set; }

        public Vertex<T> this[int index]
        {
            get
            {
                return null;
            }
        }

        public Graph()
        {
            Vertices = new List<Vertex<T>>();
            EdgeCount = Vertices.Count;
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
                    // from graph we need the last vertex's point


                    info.Add(graph[graph.EdgeCount - 1], new HashSet<Vertex<Point>>());
                }
            }

            for (int i = 0; i < graph.EdgeCount - 1; i++)
            {
                if((i + 1) % width == 0)
                {
                    i++;
                }


                info[graph[i]].Add(graph[i + 1]);
                info[graph[i + width]].Add(graph[i]);    
            }

            for (int i = 0; i < graph.EdgeCount - width; i++)
            {
                info[graph[i]].Add(graph[i + width]);
                info[graph[i + width]].Add(graph[i]);
            }

            var start = graph[gen.Next(graph.EdgeCount)];
            start.IsVisited = true;

            frontier.UnionWith(info[start]);

            while(frontier.Count > 0)
            {
                var curr = frontier.ElementAt(gen.Next(frontier.Count));
                frontier.Remove(curr);
                var Neib = info[curr].Where(x => x.IsVisited).ToList();
                graph.AddEdge(curr, Neib[gen.Next(Neib.Count)], 1);
                curr.IsVisited = true;
                frontier.UnionWith(info[curr].Where(x => !x.IsVisited));
            }

            return graph;
        }
    }
}
