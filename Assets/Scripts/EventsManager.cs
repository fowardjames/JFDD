using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventsManager
{
	public static Action<GameObject> OnEndDrag;
	public static Action<GameObject> OnBeginDrag;
	//public static Action<GameObject> OnDrag;
	//SendMessage Events
	public static Action<int> OnMessageSent;
	public static Action<GameObject> OnCollide;

	public static Action<string> OnCharacterSelected;
	public static Action<Vector3> OnCreateNewCharacter;
	public static Action<Vector3> TriggerEMP;
}
