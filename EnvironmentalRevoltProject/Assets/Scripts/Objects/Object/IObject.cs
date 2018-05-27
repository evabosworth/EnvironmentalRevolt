using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IObject: ScriptableObject {
	public GameObject gameObject;
	public string displayName;
	public string uniqueName;
	public IMovement movement;
	public Vector3 position;
	public Vector3 origPosition; //For when moved, but not a new turn.

	public IObject(){
	}


	public IObject (GameObject gameObject, string displayName, string UniqueName, Vector3 position){
		this.gameObject = gameObject;
		this.displayName = displayName;
		this.uniqueName = UniqueName;
		this.position = position;
		this.origPosition = position;
	}
}

