using UnityEngine;
using System;
using System.Collections;

public class GlobalVariables
{
	//5 = all, 0 = none
	private int debugPriorityLevel = 5;
	private static GlobalVariables gv = null;
	private GlobalVariables(){
		gv = this;
	}

	public static GlobalVariables getInstance(){
		if (gv == null) {
			new GlobalVariables();
		}

		return gv; 
	}


	public void printToConsole(string str, int priority = 5){

		if (debugPriorityLevel >= priority) {
			Debug.Log (str);
		}


	}



}



