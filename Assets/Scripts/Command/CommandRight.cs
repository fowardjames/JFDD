using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRight : MonoBehaviour, ICommand
{
	public void Command(GameObject go)
	{
		go.transform.position += new Vector3(1, 0, 0);
	}
}
