using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : ITerrain
{
	public Air(Vector3 position){
		currentPosition = position;
	}
	/*
	 *	Placement is ontop of the terrain. So if an object can be ontop of this terrain
	 */
	public override bool allowPlacement(IWorldObject toPlaceObject){
		return false;
	}
	/*
	 *	Occupation is in the terrain. So if an object can held within the tile.
	 */
	public override bool allowOccupation (IWorldObject toPlaceObject){
		return true;
	}

	public override string toString (){
		string air = "[air](";
		air += currentPosition.ToString ();
		air += ");";

		return air;

	}
	public override void fromString(String str){


	}

}

