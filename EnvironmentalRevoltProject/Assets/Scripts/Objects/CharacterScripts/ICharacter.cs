using System;
using UnityEngine;

public abstract class ICharacter: IObject
{
	public float initiative = 0;

	public ICharacter(){
	}

	public ICharacter(GameObject gameObject, string displayName, string uniqueName, Vector3 position) : base(gameObject,displayName,uniqueName,position) {

		this.movement = new BasicMovement (5, 2);



	}

}

