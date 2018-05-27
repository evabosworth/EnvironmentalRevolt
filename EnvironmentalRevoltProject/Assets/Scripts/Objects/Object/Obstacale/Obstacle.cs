using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : IObject
{

	public Obstacle(GameObject gameObject, string displayName, string uniqueName, Vector3 position) : base(gameObject, displayName, uniqueName, position)
	{

		this.movement = new BasicMovement (5, 2);


	}
}

