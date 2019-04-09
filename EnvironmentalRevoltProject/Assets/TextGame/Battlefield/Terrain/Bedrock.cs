using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedrock : ITerrain
{
	public Bedrock(Vector3 position){
		currentPosition = position;
	}


	/*
	 *	Placement is ontop of the terrain. So if an object can be ontop of this terrain
	 */
	public override bool allowPlacement(IWorldObject toPlaceObject){
		return true;
	}
	/*
	 *	Occupation is in the terrain. So if an object can held within the tile.
	 */
	public override bool allowOccupation (IWorldObject toPlaceObject){
		return false;
	}




	public override string toString (){
		string bedrock = "[bedrock](";
		bedrock += currentPosition.ToString ();
		bedrock += ");";

		return bedrock;
		
	}
	public override void fromString(String str){
		
		
	}

}

