using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
	public Queue<ICommand> Commands;

	public GameObject Player;

	public MapGenerator MapGenerator;
	public Path Path; 
	void Start()
    {
		Commands = new Queue<ICommand>();
	}

	public void Go()
	{
		StartCoroutine(IssueCommands());
	}
	private IEnumerator IssueCommands()
	{
		foreach (var command in Commands)
		{
			yield return new WaitForSeconds(0.5f);
			command.Command(Player);
			IsValidLocation(Player.transform.position); 
		}
		IsAtFinalLocation(Player.transform.position); 
		Commands.Clear();
	}
	private void IsValidLocation(Vector3 playerPos)
	{
		if (MapGenerator.Obstacles.ContainsKey(playerPos))
		{
			Debug.LogError("Restart Scene You Lose");
		}
	}
	private void IsAtFinalLocation(Vector3 playerPos)
	{
		if (Path.goal == playerPos)
		{
			Debug.Log("YAY YOU WON!!!!!!"); 
		}
	}
}
