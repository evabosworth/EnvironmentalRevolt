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
    protected int experienceReward;

    public abstract string ToString ();
	public abstract void fromString(String str);

	public void setCurrentPosition(Vector3 pos){
		currentPosition = pos;
	}

    public Vector3 getCurrentPosition()
    {
        return currentPosition;
    }
    public bool recieveAttack(IAttack attack, IUnit attacker)
    {
        this.curHealth -= attack.getDamage(attacker);
        if(this.curHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool makeAttack(IAttack attack, IWorldObject target, IUnit attacker)
    {

        return target.recieveAttack(attack, attacker);
    }

    public int getExperienceReward()
    {
        return experienceReward;
    }
}

