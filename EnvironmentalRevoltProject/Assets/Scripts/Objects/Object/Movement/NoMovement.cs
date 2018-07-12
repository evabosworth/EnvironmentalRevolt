using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoMovement : ScriptableObject, IMovement
{

	int distance;
	GlobalVariables gv;
	List<Vector3> visitedPositions;

	float jumpHeightUnits;
	
	public void init(int distance, int jumpHeightUnits){
		this.distance = 0;
		this.jumpHeightUnits = 0;
	}

	public List<Vector3> findPossibleMovement (Vector3 startPos){

		GlobalVariables gv = FindObjectOfType<GlobalVariables>();

		gv.log ("Object has no movement");
		return new List<Vector3>();
	}

}