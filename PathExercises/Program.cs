using PathExercises.Classes;

namespace PathExercises
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, Graph!");
			// Add code here to test 

			TestWeightedPathFinding();
			TestUnweightedPathFinding();
		}

		private static void TestUnweightedPathFinding()
		{
			Graph graph = new UndirectedGraph();
			graph.AddEdge("A", "C");
			graph.AddEdge("A", "E");
			graph.AddEdge("C", "D");
			graph.AddEdge("C", "G");
			graph.AddEdge("D", "E");
			graph.AddEdge("E", "H");
			graph.AddEdge("F", "G");
			graph.AddEdge("F", "B");
			graph.AddEdge("F", "I");
			graph.AddEdge("H", "I");
			Console.WriteLine(graph.Display());
			var distances = graph.FindDistances("A");
			Console.Write("BFS A (Distances): ");
			distances.ForEach(d => Console.Write($"{d.name}:{d.distance} "));
			Console.WriteLine();
			Console.WriteLine($"BFS A:D (shortest path): {string.Join(',', graph.FindShortestPath("A", "D"))}");
			Console.WriteLine($"BFS A:I (shortest path): {string.Join(',', graph.FindShortestPath("A", "I"))}");
			Console.WriteLine();
		}

		private static void TestWeightedPathFinding()
		{
			Graph graph = new();
			graph.AddEdge("A", "B", 7);
			graph.AddEdge("A", "D", 3);
			graph.AddEdge("B", "D", 2);
			graph.AddEdge("B", "C", 3);
			graph.AddEdge("B", "E", 6);
			graph.AddEdge("D", "C", 4);
			graph.AddEdge("D", "E", 7);
			graph.AddEdge("C", "E", 2);

			Console.WriteLine(graph.Display());
			var distances = graph.FindDistances("A");
			Console.Write("Dijkstra A (Distances): ");
			distances.ForEach(d => Console.Write($"{d.name}:{d.distance} "));
			Console.WriteLine();
			Console.WriteLine($"Dijkstra A:C (shortest path): {string.Join(',', graph.FindShortestPath("A", "C"))}");
			Console.WriteLine($"Dijkstra A:E (shortest path): {string.Join(',', graph.FindShortestPath("A", "E"))}");
			Console.WriteLine();
		}

	}
}