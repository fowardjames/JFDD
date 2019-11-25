using Priority_Queue;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
	IDictionary<Vector3, Vector3> nodeParents = new Dictionary<Vector3, Vector3>();

	IDictionary<Vector3, GameObject> prevTile = new Dictionary<Vector3, GameObject>();

	[SerializeField] private MapGenerator _mapGenerator;
	IList<Vector3> path;

	bool solutionVisible;
	string prevAlgo;

	public GameObject PathTile; 
	public Vector3 goal;
	// Use this for initialization
	void Start()
	{
		if (_mapGenerator == null)
		{
			_mapGenerator = GameObject.FindObjectOfType<MapGenerator>(); 
		}
	}


	int EuclideanEstimate(Vector3 node, Vector3 goal)
	{
		return (int)Mathf.Sqrt(Mathf.Pow(node.x - goal.x, 2) +
			Mathf.Pow(node.y - goal.y, 2) +
			Mathf.Pow(node.z - goal.z, 2));
	}

	int ManhattanEstimate(Vector3 node, Vector3 goal)
	{
		return (int)(Mathf.Abs(node.x - goal.x) +
			Mathf.Abs(node.y - goal.y) +
			Mathf.Abs(node.z - goal.z));
	}

	int HeuristicCostEstimate(Vector3 node, Vector3 goal, string heuristic)
	{
		switch (heuristic)
		{
			case "euclidean":
				return EuclideanEstimate(node, goal);
			case "manhattan":
				return ManhattanEstimate(node, goal);
		}

		return -1;
	}

	public Vector3 FindShortestPathAStar(Vector3 startPosition, Vector3 goalPosition, string heuristic)
	{

		uint nodeVisitCount = 0;
		float timeNow = Time.realtimeSinceStartup;

		// A* tries to minimize f(x) = g(x) + h(x), where g(x) is the distance from the start to node "x" and
		//    h(x) is some heuristic that must be admissible, meaning it never overestimates the cost to the next node.
		//    There are formal logical proofs you can look up that determine how heuristics are and are not admissible.

		IEnumerable<Vector3> validNodes = _mapGenerator.WalkablePositions
			.Where(x => x.Value == true)
			.Select(x => x.Key);

		// Represents h(x) or the score from whatever heuristic we're using
		IDictionary<Vector3, int> heuristicScore = new Dictionary<Vector3, int>();

		// Represents g(x) or the distance from start to node "x" (Same meaning as in Dijkstra's "distances")
		IDictionary<Vector3, int> distanceFromStart = new Dictionary<Vector3, int>();

		foreach (Vector3 vertex in validNodes)
		{
			heuristicScore.Add(new KeyValuePair<Vector3, int>(vertex, int.MaxValue));
			distanceFromStart.Add(new KeyValuePair<Vector3, int>(vertex, int.MaxValue));
		}

		heuristicScore[startPosition] = HeuristicCostEstimate(startPosition, goalPosition, heuristic);
		distanceFromStart[startPosition] = 0;

		// The item dequeued from a priority queue will always be the one with the lowest int value
		//    In this case we will input nodes with their calculated distances from the start g(x),
		//    so we will always take the node with the lowest distance from the queue.
		SimplePriorityQueue<Vector3, int> priorityQueue = new SimplePriorityQueue<Vector3, int>();
		priorityQueue.Enqueue(startPosition, heuristicScore[startPosition]);

		while (priorityQueue.Count > 0)
		{
			// Get the node with the least distance from the start
			Vector3 curr = priorityQueue.Dequeue();
			nodeVisitCount++;

			// If our current node is the goal then stop
			if (curr == goalPosition)
			{
				return goalPosition;
			}

			IList<Vector3> neighbors = GetWalkableNodes(curr);

			foreach (Vector3 node in neighbors)
			{
				// Get the distance so far, add it to the distance to the neighbor
				int currScore = distanceFromStart[curr] + Weight(node);

				// If our distance to this neighbor is LESS than another calculated shortest path
				//    to this neighbor, set a new node parent and update the scores as our current
				//    best for the path so far.
				if (currScore < distanceFromStart[node])
				{
					nodeParents[node] = curr;
					distanceFromStart[node] = currScore;

					int hScore = distanceFromStart[node] + HeuristicCostEstimate(node, goalPosition, heuristic);
					heuristicScore[node] = hScore;

					// If this node isn't already in the queue, make sure to add it. Since the
					//    algorithm is always looking for the smallest distance, any existing entry
					//    would have a higher priority anyway.
					if (!priorityQueue.Contains(node))
					{
						priorityQueue.Enqueue(node, hScore);
					}
				}
			}
		}

		return startPosition;
	}

	int Weight(Vector3 node)
	{
		//Add tags for weighted values
		return 1;
	}


	bool CanMove(Vector3 nextPosition)
	{
		return (_mapGenerator.WalkablePositions.ContainsKey(nextPosition) ? _mapGenerator.WalkablePositions[nextPosition] : false);
	}

	public bool DisplayShortestPath(string algorithm, Vector3 pos1, Vector3 pos2)
	{

		if (solutionVisible && algorithm == prevAlgo)
		{
			foreach (Vector3 node in path)
			{
				_mapGenerator.NodeReference[node] = prevTile[node];
			}

			solutionVisible = false;
			return false;
		}

		nodeParents = new Dictionary<Vector3, Vector3>();
		path = FindShortestPath(algorithm, pos1, pos2);

		if (path == null)
		{
			Debug.LogError("No Path");
			return false;
		}

		foreach (Vector3 node in path)
		{
			prevTile[node] = _mapGenerator.NodeReference[node];
			_mapGenerator.NodeReference[node] = PathTile;
			GameObject go = Instantiate(PathTile, node,	Quaternion.identity);

		}
		if (path.Count < _mapGenerator.CurrentMap.MinimunNumberOfMoves)
		{
			return false;
		}
		_mapGenerator.NodeReference[path[0]] = PathTile;


		solutionVisible = true;
		prevAlgo = algorithm;
		return true; 
	}


	IList<Vector3> FindShortestPath(string algorithm, Vector3 pos1, Vector3 pos2)
	{

		IList<Vector3> path = new List<Vector3>();
		
		goal = FindShortestPathAStar(pos1, pos2, "manhattan");


		if (goal == pos1 || !nodeParents.ContainsKey(nodeParents[goal]))
		{
			//No solution was found.
			Debug.LogError("No solution");
			return null;
		}

		Vector3 curr = goal;
		while (curr != pos1)
		{
			path.Add(curr);
			curr = nodeParents[curr];
		}

		return path;
	}

	IList<Vector3> GetWalkableNodes(Vector3 curr)
	{

		IList<Vector3> walkableNodes = new List<Vector3>();

		IList<Vector3> possibleNodes = new List<Vector3>() {
			new Vector3 (curr.x + 1, curr.y, curr.z),
			new Vector3 (curr.x - 1, curr.y, curr.z),
			new Vector3 (curr.x, curr.y, curr.z + 1),
			new Vector3 (curr.x, curr.y, curr.z - 1),
			//new Vector3 (curr.x + 1, curr.y, curr.z + 1),
			//new Vector3 (curr.x + 1, curr.y, curr.z - 1),
			//new Vector3 (curr.x - 1, curr.y, curr.z + 1),
			//new Vector3 (curr.x - 1, curr.y, curr.z - 1)
		};

		foreach (Vector3 node in possibleNodes)
		{
			if (CanMove(node))
			{
				walkableNodes.Add(node);
			}
		}

		return walkableNodes;
	}
}
