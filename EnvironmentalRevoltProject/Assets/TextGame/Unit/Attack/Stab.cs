using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : IAttack
{
	//IRange range;
	public Stab(){
		baseDamage = 40;
		range = new DirectionalRange (2,0,4,0);
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
