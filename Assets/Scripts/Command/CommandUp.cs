using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandUp : MonoBehaviour, ICommand
{
	public void Command(GameObject go)
	{
		go.transform.position += new Vector3(0, 0, 1);
	}
}
