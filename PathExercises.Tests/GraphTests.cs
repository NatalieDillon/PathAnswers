using PathExercises.Classes;

namespace PathExercises.Tests
{
    [TestClass]
    public class GraphTests
    {                   
        private static UndirectedGraph PopulateUnweightedUndirectedGraph()
        {
            UndirectedGraph graph = new();
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
            return graph;
        }

        private static Graph PopulateWeightedDirectedGraph()
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
            return graph;
        }

        private static UndirectedGraph PopulateWeightedUnDirectedGraph()
        {
            UndirectedGraph graph = new();
            graph.AddEdge("A", "B", 7);
            graph.AddEdge("A", "D", 3);
            graph.AddEdge("B", "D", 2);
            graph.AddEdge("B", "C", 3);
            graph.AddEdge("B", "E", 6);
            graph.AddEdge("D", "C", 4);
            graph.AddEdge("D", "E", 7);
            graph.AddEdge("C", "E", 2);
            return graph;
        }

        [TestMethod]
        public void TestDistancesWeightedUnDirectedGraphFromA()
        {
            Graph graph = PopulateWeightedUnDirectedGraph();
            var distances = graph.FindDistances("A");
            List<(string name, double distance)> expected = [ ("A", 0), ("D", 3), ("B", 5), ("C", 7), ("E", 9) ];
            CollectionAssert.AreEqual(expected, distances);
        }

        [TestMethod]
        public void TestPathWeightedDirectedGraphAToC()
        {
            Graph graph = PopulateWeightedDirectedGraph();
            List<string> route = graph.FindShortestPath("A", "C");
            List<string> expectedRoute = [ "A", "D", "C" ];
            CollectionAssert.AreEqual(expectedRoute, route);
        }

        [TestMethod]
        public void TestPathWeightedDirectedGraphAToI()
        {
            Graph graph = PopulateWeightedDirectedGraph();
            List<string> route = graph.FindShortestPath("A", "E");
            List<string> expectedRoute = [ "A", "D", "C", "E" ];
            CollectionAssert.AreEqual(expectedRoute, route);
        }


        [TestMethod]
        public void TestPathUnweightedUndirectedGraphAToD()
        {
            Graph graph = PopulateUnweightedUndirectedGraph();
            List<string> route = graph.FindShortestPath("A", "D");
            List<string> expectedRoute = [ "A", "C", "D" ];
            CollectionAssert.AreEqual(expectedRoute, route);
        }

        [TestMethod]
        public void TestDistancesWeightedDirectedGraphFromA()
        {
            Graph graph = PopulateWeightedDirectedGraph();
            var distances = graph.FindDistances("A");
            List<(string name, double distance)> expected = [ ("A", 0), ("D", 3), ("B", 7), ("C", 7), ("E", 9) ];
            CollectionAssert.AreEqual(expected, distances);
        }

        [TestMethod]
        public void TestPathUnweightedUndirectedGraphAToI()
        {
            Graph graph = PopulateUnweightedUndirectedGraph();
            List<string> route = graph.FindShortestPath("A", "I");
            List<string> expectedRoute = [ "A", "E", "H", "I" ];
            CollectionAssert.AreEqual(expectedRoute, route);
        }

        [TestMethod]
        public void TestDistancesUnweightedUndirectedGraphFromA()
        {
            Graph graph = PopulateUnweightedUndirectedGraph();
            var distances = graph.FindDistances("A");
            List<(string name, double distance)> expected =[ ("A",0), ("C",1), ("E",1), ("D",2), ("G",2), ("H",2), ("F",3), ("I",3), ("B",4)];
            CollectionAssert.AreEqual(expected, distances);
        }
    }
}