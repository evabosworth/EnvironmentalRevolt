using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IObject: ScriptableObject {
	public string DisplayName;
	public string UniqueName;
	public IMovement movement;
	public PrefabObject.PrefabIdentifier prefabName;
	public Vector3 position;


	public abstract void updateInfo (string DisplayName, string UniqueName, Vector3 pos);
}

