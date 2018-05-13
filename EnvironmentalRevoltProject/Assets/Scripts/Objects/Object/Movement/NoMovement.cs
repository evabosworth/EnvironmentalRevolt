using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoMovement : ScriptableObject, IMovement
{
	public List<Vector3> findPossibleMovement (Vector3 startPos){

		GlobalVariables gv = FindObjectOfType<GlobalVariables>();

		gv.log ("Object has no movement");
		return new List<Vector3>();
	}

}