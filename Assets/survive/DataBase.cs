using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase {


	private static DataBase instance;
	public static DataBase Instance{

		get{
			if (instance==null) {
				instance = new DataBase ();
			}
			return instance;

		}



	}
	private DataBase(){}


	public float score = 0;




}
