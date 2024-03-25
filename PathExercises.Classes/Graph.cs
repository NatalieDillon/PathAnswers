using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathExercises.Classes
{
    public class Graph
    {
        // Fields
        private readonly Dictionary<Vertex, Dictionary<Vertex, double>> _graph = new();
        private readonly bool _isDirected = true;

        // Constructors
        public Graph() { }
        protected Graph(bool isDirected)
        {
            _isDirected = isDirected;
        }
        
        // Public Methods
        public List<string> FindShortestPath(string startVertex, string endVertex)
        {
            // Find start vertex if it doesn't exist throw an error
            // Find end vertex if it doesn't exist throw an error
            
            if (IsGraphWeighted())
            {
                // If the graph is weighted call Dijkstra()
            }
            else
            {
                // Otherwise call BFS() (Dijkstra also works on unweighted graphs but this is just to illustrate)
            }

            // Call GetPath to extract the path information saved into the vertices
            throw new NotImplementedException();
        }

        public List<(string name, double distance)> FindDistances(string startVertex)
        {
            // Find start vertex if it doesn't exist throw an error

            if (IsGraphWeighted())
            {
                // If the graph is weighted call Dijkstra()
            }
            else
            {
                // Otherwise call BFS() (Dijkstra also works on unweighted graphs but this is just to illustrate)
            }

            // Write some code here to extract the node names and distances and return it as a list of tuples
            // Order the list by ascending distance
            // You can use a linq expression with select and order by to do this
            throw new NotImplementedException();
        }

        public void AddEdge(string startName, string endName, double weight=1)
        {
            Vertex startVertex = AddOrGetVertex(startName);
            Vertex endVertex = AddOrGetVertex(endName);

            AddNeighbour(startVertex, endVertex, weight);

            if (!_isDirected)
            {
                AddNeighbour(endVertex, startVertex, weight);
            }
        }

        public string Display()
        {
            bool weighted = IsGraphWeighted();
            StringBuilder sb = new();
            foreach (var vertex in _graph)
            {
                string entry = $"{vertex.Key.Name}: ";
                entry += "[";
                foreach (var adjacencyList in vertex.Value)
                {
                    if (weighted)
                    {
                        entry += $"{adjacencyList.Key.Name}:{adjacencyList.Value}, ";
                    }
                    else
                    {
                        entry += $"{adjacencyList.Key.Name}, ";
                    }
                }
                entry = entry.TrimEnd().TrimEnd(',');
                entry += "]\n";
                sb.Append(entry);
            }
            return sb.ToString();
        }

        // Private methods
        private void Dijkstra(Vertex startVertex)
        {
            // Implement Dijkstra here
            // You can use the PriorityQueue<T> class but you will have to enqueue the vertex again if the distance changes
            // This is because the priority is not updated after insertion
            // There is some pseudocode in the readme if needed
            throw new NotImplementedException() ;
        }

        private void BFS(Vertex startVertex)
        {
            Reset(); // set vertices to starting values
            Queue<Vertex> queue = new();
            startVertex.Visited = true;
            startVertex.Distance = 0;
            queue.Enqueue(startVertex);
            while (queue.Count > 0)
            {
                Vertex currentVertex = queue.Dequeue();
                foreach (Vertex neighbour in _graph[currentVertex].Keys)
                {
                    if (!neighbour.Visited)
                    {
                        neighbour.PreviousVertex = currentVertex;
                        neighbour.Visited = true;
                        neighbour.Distance = currentVertex.Distance + 1;
                        queue.Enqueue(neighbour);
                    }
                }
            }
        }

        private List<string> GetPath(Vertex vertex)
        {
            // Return the path from the vertex passed in by adding the previous vertex to a list
            // Continue until the previous vertex is null
            // At the end you will want to reverse the list to get the order correct
            throw new NotImplementedException();
        }
        private Vertex AddOrGetVertex(string vertexName)
        {
            Vertex vertex;
            if (!_graph.Any(kvp => kvp.Key.Name == vertexName))
            {
                vertex = new(vertexName);
                _graph.Add(vertex, new Dictionary<Vertex, double>());
            }
            vertex = _graph.First(kvp => kvp.Key.Name == vertexName).Key;
            return vertex;
        }
        private void AddNeighbour(Vertex startVertex, Vertex endVertex, double weight)
        {
            var neighbours = _graph[startVertex];
            if (neighbours.ContainsKey(endVertex))
            {
                neighbours[endVertex] = weight;
            }
            else
            {
                neighbours.Add(endVertex, weight);
            }
        }
        private bool IsGraphWeighted()
        {
            var edges = _graph.SelectMany(v => v.Value);
            bool isWeighted = edges.Any(e => e.Value > 1);
            return isWeighted;
        }           
        private void Reset()
        {
            foreach (Vertex v in _graph.Keys)
            {
                v.Reset();
            }
        }
    }
}
