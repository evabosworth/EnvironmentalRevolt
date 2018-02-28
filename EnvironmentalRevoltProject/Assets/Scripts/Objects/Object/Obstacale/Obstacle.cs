using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : IObject
{

	public override void updateInfo(string DisplayName, string UniqueName, Vector3 position)
	{
		this.DisplayName = DisplayName;
		this.UniqueName = UniqueName;
		this.movement = NoMovement.CreateInstance<NoMovement> ();
		this.prefabName = PrefabObject.PrefabIdentifier.SPHERE;
		this.position = position;
	}
}

