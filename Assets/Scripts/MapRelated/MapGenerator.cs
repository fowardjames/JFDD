using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public Level CurrentMap;
	public Level Map1;
	public Level Map2;

	public IDictionary<Vector3, bool> WalkablePositions = new Dictionary<Vector3, bool>();
	public IDictionary<Vector3, GameObject> NodeReference = new Dictionary<Vector3, GameObject>();
	public Dictionary<Vector3, string> Obstacles = new Dictionary<Vector3, string>();

	void Awake()
	{
		int rng = Random.Range(0, 2);
		//Randomly choose map because I don't want to code it 
		if (rng == 0)
			CurrentMap = Map1;
		else
			CurrentMap = Map2;

		InitializeNodeNetwork(CurrentMap.NumberOfObstacles);
	}

	// Update is called once per frame

	void InitializeNodeNetwork(int numBarriers)
	{
		var width = CurrentMap.BoardWidth;
		var height = CurrentMap.BoardHeight;

		Obstacles = GenerateObstacles(numBarriers);


		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				Vector3 newPosition = new Vector3(i, 0, j);
				GameObject copy;
				string obstacleType = null;

				if (Obstacles.TryGetValue(newPosition, out obstacleType))
				{
					copy = Instantiate(CurrentMap.BoardPiece);
					copy.transform.position = newPosition;
					switch (obstacleType)
					{
						case "barrier":
							WalkablePositions.Add(new KeyValuePair<Vector3, bool>(newPosition, false));
							break;
					}
				}
				else
				{
					copy = Instantiate(CurrentMap.BoardPiece);
					copy.transform.position = newPosition;
					WalkablePositions.Add(new KeyValuePair<Vector3, bool>(newPosition, true));
				}

				NodeReference.Add(newPosition, copy);
			}
		}
	}

	Dictionary<Vector3, string> GenerateObstacles(int numBarriers)
	{
		for (int i = 0; i < numBarriers; i++)
		{
			Vector3 nodePosition = new Vector3(Random.Range(0, CurrentMap.BoardWidth), 0, Random.Range(0, CurrentMap.BoardHeight));
			if (!Obstacles.ContainsKey(nodePosition))
			{
				Obstacles.Add(nodePosition, "barrier");
				Instantiate(CurrentMap.ObstaclePiece, nodePosition, Quaternion.identity);
			}
		}
		return Obstacles;
	}
}
