using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : ITerrain
{
	public Dirt(Vector3 position){
		currentPosition = position;
	}

	public override bool allowPlacement(IWorldObject toPlaceObject){
		return true;
	}

	public override bool allowOccupation(IWorldObject toPlaceObject){
		return false;
	}
	
	public override string ToString (){
		string dirt = "[dirt](";
		dirt += currentPosition.ToString ();
		dirt += ");";

		return dirt;

	}
	public override void fromString(String str){


	}
}

