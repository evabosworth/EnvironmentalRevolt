using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : IAttack
{
	//IRange range;
	public Slash(){
		baseDamage = 50;
		range = new MeleeRange();
	}

    public override float modifyAttack(IUnit attacker = null)
    {
        if(attacker == null)
        {
            endDamage = baseDamage;
        }
        else
        {
            endDamage = baseDamage + attacker.strength;
        }
        return endDamage;
    }

}
