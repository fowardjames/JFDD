using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSetup : MonoBehaviour
{
	public MapGenerator MapGenerator; 
	public Path Path;

	private Vector3 StartPosition;
	private Vector3 EndPosition;

	[SerializeField] private GameObject StartTile;
	[SerializeField] private GameObject EndTile;

	[SerializeField] private GameLoop GameLoop;

	[SerializeField] private GameObject _playerPrefab; 

	void Start()
    {
		EventsManager.OnCreateNewCharacter = null;
		EventsManager.OnCreateNewCharacter += SpawnPlayer;
		SpawnStartNode();
		SpawEndNode();
		if (!Path.DisplayShortestPath("Heuristic", StartPosition, EndPosition))
		{
			Debug.LogError("Restart");
			RestartScene();
		}
	}

	public void SpawnStartNode()
	{
		StartPosition = new Vector3(Random.Range(0, MapGenerator.CurrentMap.BoardWidth), 0, Random.Range(0, MapGenerator.CurrentMap.BoardHeight));
		Instantiate(StartTile, StartPosition, Quaternion.identity);
		EventsManager.OnCreateNewCharacter.Invoke(StartPosition);

	}
	public void SpawEndNode()
	{
		EndPosition = new Vector3(Random.Range(0, MapGenerator.CurrentMap.BoardWidth),0, Random.Range(0, MapGenerator.CurrentMap.BoardHeight));
		Instantiate(EndTile, EndPosition, Quaternion.identity);
		
	}
	public void RestartScene()
	{
		SceneManager.LoadScene(0);
	}
	private void SpawnPlayer(Vector3 spawnPoint)
	{
	    GameObject player = Instantiate(_playerPrefab, StartPosition, Quaternion.identity);
		GameLoop.Player = player;
	}

}
