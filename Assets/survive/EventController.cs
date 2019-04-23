using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController {


	private static EventController instance;
	public static EventController Instance{

		get{
			if (instance==null) {
				instance = new EventController ();
			}
			return instance;

		}





	}
	private EventController(){}



	public bool isPlayerLose=false;




}
