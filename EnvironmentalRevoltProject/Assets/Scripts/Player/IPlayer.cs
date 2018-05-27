using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class IPlayer{
	public Dictionary<Vector3, IObject> validStartingPositions;
	public Dictionary<Vector3, IObject> playerUnits;

	public virtual bool isValidStartingPosition(Vector3 startingPos, IObject codeUnit){
		IObject codeTerrain;
		validStartingPositions.TryGetValue (startingPos, out codeTerrain);

		if (codeTerrain == null) {
			return false;
		}

		return true;
	}


}