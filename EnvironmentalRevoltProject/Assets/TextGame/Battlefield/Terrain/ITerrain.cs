using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ITerrain : IWorldObject
{
	/*
	 *	Placement is ontop of the terrain. So if an object can be ontop of this terrain
	 */
	public abstract bool allowPlacement(IWorldObject toPlaceObject);
	/*
	 *	Occupation is in the terrain. So if an object can held within the tile.
	 */
	public abstract bool allowOccupation (IWorldObject toPlaceObject);
}

