using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : IObject
{

	public override void init(GameObject gameObject, string displayName, string uniqueName, Vector3 position){
		base.init (gameObject, displayName, uniqueName, position);

		this.movement = BasicMovement.CreateInstance<BasicMovement> ();
		this.movement.init(5, 2);


	}
}

