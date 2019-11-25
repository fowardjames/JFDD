using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
	public int BoardWidth = 10;
	public int BoardHeight = 10;
	public int NumberOfObstacles = 10;
	public int MinimunNumberOfMoves = 5;
	public GameObject BoardPiece;
	public GameObject ObstaclePiece;
}
