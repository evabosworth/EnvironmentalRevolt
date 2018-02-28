using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoMovement : IMovement
{
	public List<Node> findPossibleMovement (Node startNode){

		GlobalVariables gv = FindObjectOfType<GlobalVariables>();

		gv.log ("Object has no movement");
		return null;
	}

}