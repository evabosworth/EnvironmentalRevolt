using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWorldObject
{
	protected Vector3 currentPosition;
    //Random start health value
    protected float maxHealth = 100.0f;
    protected float curHealth = 100.0f;

    public abstract string ToString ();
	public abstract void fromString(String str);

	public void setCurrentPosition(Vector3 pos){
		currentPosition = pos;
	}

    public Vector3 getCurrentPosition()
    {
        return currentPosition;
    }
    public void recieveAttack(IAttack attack)
    {
        this.curHealth -= attack.Damage;
    }
}

