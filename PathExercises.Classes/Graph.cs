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
        private readonly Dictionary<Vertex, Dictionary<Vertex, double>> _graph = [];
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
			// Check start vertex exists
			if (!_graph.Any(kvp => kvp.Key.Name == startVertex))
			{
				throw new ArgumentException("Start vertex not found");
			}
			var start = _graph.First(kvp => kvp.Key.Name == startVertex).Key;

			// Check end vertex exists
			if (!_graph.Any(kvp => kvp.Key.Name == endVertex))
			{
				throw new ArgumentException("End vertex not found");
			}
			var end = _graph.First(kvp => kvp.Key.Name == endVertex).Key;

			if (IsGraphWeighted())
			{
				Dijkstra(start);
			}
			else
			{
				BFS(start);
			}
			return GetPath(end);
		}

		public List<(string name, double distance)> FindDistances(string startVertex)
		{
			// Check start vertex exists
			if (!_graph.Any(kvp => kvp.Key.Name == startVertex))
			{
				throw new ArgumentException("Start vertex not found");
			}
			var start = _graph.First(kvp => kvp.Key.Name == startVertex).Key;

			if (IsGraphWeighted())
			{
				Dijkstra(start);
			}
			else
			{
				BFS(start);
			}
			return _graph.Keys.Select(vertex => (vertex.Name, vertex.Distance)).OrderBy(v => v.Distance).ToList();
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
			Reset(); // set vertices to starting values
			startVertex.Distance = 0;
			PriorityQueue<Vertex, double> queue = new();
			queue.Enqueue(startVertex, startVertex.Distance);
			while (queue.Count > 0)
			{
				Vertex currentVertex = queue.Dequeue();
				if (!currentVertex.Visited) // The same Vertex might get enqueued multiple times with shorter distances to we need to check if it has been visited already
				{
					currentVertex.Visited = true;
					foreach (var neighbour in _graph[currentVertex])
					{
						if (!neighbour.Key.Visited)
						{
							var weight = neighbour.Value;
							if (currentVertex.Distance + weight < neighbour.Key.Distance)
							{
								neighbour.Key.Distance = currentVertex.Distance + weight;
								neighbour.Key.PreviousVertex = currentVertex;
								queue.Enqueue(neighbour.Key, neighbour.Key.Distance);
							}
						}
					}
				}
			}
		}

		private void DijkstraList(Vertex startVertex)
		{
			Reset(); // set vertices to starting values
			startVertex.Visited = true;
			startVertex.Distance = 0;
			List<Vertex> toVisit = [];
			foreach (var vertex in _graph.Keys)
			{
				toVisit.Add(vertex); // add every node
			}
			while (toVisit.Count > 0)
			{
				var currentVertex = toVisit.OrderBy(v => v.Distance).First();
				currentVertex.Visited = true;
				toVisit.Remove(currentVertex);
				foreach (var neighbour in _graph[currentVertex])
				{
					if (!neighbour.Key.Visited)
					{
						var weight = neighbour.Value;
						if (currentVertex.Distance + weight < neighbour.Key.Distance)
						{
							neighbour.Key.Distance = currentVertex.Distance + weight;
							neighbour.Key.PreviousVertex = currentVertex;
						}
					}
				}
			}
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

        private static List<string> GetPath(Vertex vertex)
		{
			List<string> route = [];
			Vertex current = vertex;
			route.Add(current.Name);
			while (current.PreviousVertex != null)
			{
				current = current.PreviousVertex;
				route.Add(current.Name);
			}
			route.Reverse();
			return route;
		}
        private Vertex AddOrGetVertex(string vertexName)
        {
            Vertex vertex;
            if (!_graph.Any(kvp => kvp.Key.Name == vertexName))
            {
                vertex = new(vertexName);
                _graph.Add(vertex, []);
            }
            vertex = _graph.First(kvp => kvp.Key.Name == vertexName).Key;
            return vertex;
        }
        private void AddNeighbour(Vertex startVertex, Vertex endVertex, double weight)
        {
            var neighbours = _graph[startVertex];
            if (!neighbours.TryAdd(endVertex, weight))
            {
                neighbours[endVertex] = weight;
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
