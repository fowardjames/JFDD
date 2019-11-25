using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandUIManager : MonoBehaviour
{
	[SerializeField] private GameObject _up;
	[SerializeField] private GameObject _down;
	[SerializeField] private GameObject _left;
	[SerializeField] private GameObject _right;
	[SerializeField] private GameObject _emp;

	[SerializeField] private Transform _spawnPoint;
	[SerializeField] private GameLoop _gameLoop;
	[System.Serializable]
	public enum Commands
	{
		UP,
		DOWN,
		LEFT,
		RIGHT,
		EMP
	}


	public void Up()
	{
		GameObject commandUp = Instantiate(_up, _spawnPoint);
		_gameLoop.Commands.Enqueue(commandUp.GetComponent<ICommand>());
	}
	public void Down()
	{
		GameObject commandDown = Instantiate(_down, _spawnPoint);
		_gameLoop.Commands.Enqueue(commandDown.GetComponent<ICommand>());
	}
	public void Left()
	{
		GameObject commandLeft = Instantiate(_left, _spawnPoint);
		_gameLoop.Commands.Enqueue(commandLeft.GetComponent<ICommand>());
	}
	public void Right()
	{
		GameObject commandRight = Instantiate(_right, _spawnPoint);
		_gameLoop.Commands.Enqueue(commandRight.GetComponent<ICommand>());
	}
	public void EMP()
	{
		GameObject commandEMP = Instantiate(_emp, _spawnPoint);
		_gameLoop.Commands.Enqueue(commandEMP.GetComponent<ICommand>());
	}
}
