using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWorldObject
{
	protected Vector3 currentPosition;

	public abstract string toString ();
	public abstract void fromString(String str);

	public void setCurrentPosition(Vector3 pos){
		currentPosition = pos;
	}
}

