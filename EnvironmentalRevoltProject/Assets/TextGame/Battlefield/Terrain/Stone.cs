using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : ITerrain
{

	public Stone(Vector3 position){
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

	public override string ToString (){
		string stone = "[stone](";
		stone += currentPosition.ToString ();
		stone += ");";

		return stone;

	}
	public override void fromString(String str){


	}
}

