using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTerrainBlock:IObject
{
	public BasicTerrainBlock (GameObject gameObject, string displayName, string uniqueName, Vector3 position): base(gameObject, displayName, uniqueName, position){
		

		this.movement = NoMovement.CreateInstance<NoMovement> ();
	}
}

