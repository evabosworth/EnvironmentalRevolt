using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ITerrain : IWorldObject
{
	/*
	 *	Placement is ontop of the terrain. So if an object can be ontop of this terrain
	 */
	public virtual bool allowPlacement(IWorldObject toPlaceObject){
		return true;
	}
	/*
	 *	Occupation is in the terrain. So if an object can be held within the tile.
	 */
	public virtual bool allowOccupation (IWorldObject toPlaceObject){
		return true;
	}
}

