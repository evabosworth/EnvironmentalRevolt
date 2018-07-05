using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class IPlayer{
	public Dictionary<Vector3, IObject> startingPositions = new Dictionary<Vector3, IObject>();
	public Dictionary<Vector3, IObject> playerUnits = new Dictionary<Vector3, IObject>();



}