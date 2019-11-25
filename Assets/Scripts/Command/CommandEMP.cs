using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandEMP : MonoBehaviour, ICommand
{
	public void Command(GameObject go)
	{
		Debug.Log("Trigger EMP");
		if (EventsManager.TriggerEMP != null)
		{
			EventsManager.TriggerEMP.Invoke(go.transform.position);
		}
	}
}
