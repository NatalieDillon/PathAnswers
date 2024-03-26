## Path Exercises

### Complete Graph Code
Complete the code in Graph.cs by implementing the following methods</br>
1. GetPath()
2. FindDistances()
3. FindShortestPath()
4. Dijkstra()
</br>
There are comments to guide you inside each method</br>
Once you have finished sucessfully the tests in GraphTests.cs should pass</br>
You may wish to write more tests of your own or write code to call from Program.cs</br>

### Pseudo code for Dijkstra using priority queue
```
1. Reset the vertices to their initial state
2. Set the distance of the start vertex to 0
3. Enqueue the start vertex into a priority queue
4. While there are still vertices in the priority queue
   	Dequeue the vertex
	If the vertex hasn't been visited then
		Mark it as visited
		For each unvisited neighbour
		If vertex distance + weight to neighbour < neighbour distance
			Update the neighbour's distance 
			Set the neighbour's previous vertex to the current vertex
			Enqueue the neighbour
```

### Extensions
1: Use real data related to roads/train times etc and load it from a text file (you can use previous code you may have written to do this)</br>
Then use the Graph class to find the shortest path</br>
You could make this interactive by asking the user where they wish to travel to</br>

2: Research the A* algorithm on Issac computer science. This is not on the syllabus but might be interesting to use in projects.
Add a method and tests to this to your code</br>

3: Write Dikjstras using a list instead of a priority queue that you order to find the node with the shortest distance on each pass</br>

4: Extension++ Write your own priority queue using a binary heap. Definitely not on the syllabus but again could be interesting in projects</br>

