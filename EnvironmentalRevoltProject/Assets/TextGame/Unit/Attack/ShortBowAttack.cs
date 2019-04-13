using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortBowAttack : IAttack
{
	//IRange range;
	public ShortBowAttack(){
		baseDamage = 50;
		range = new PointRange(4,0,8,0);
	}

	public override float modifyAttack(IUnit attacker = null)
	{
		if(attacker == null)
		{
			endDamage = baseDamage;
		}
		else
		{
			endDamage = baseDamage + attacker.dexterity;
		}
		return endDamage;
	}

}
