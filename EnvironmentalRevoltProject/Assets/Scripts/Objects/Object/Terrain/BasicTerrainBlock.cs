using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTerrainBlock:IObject
{

	public override void init(GameObject gameObject, string displayName, string uniqueName, Vector3 position) {
		base.init (gameObject, displayName, uniqueName, position);

		this.movement = NoMovement.CreateInstance<NoMovement> ();
	}
}

