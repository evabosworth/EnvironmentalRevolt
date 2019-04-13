using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBowAttack : IAttack
{
	//IRange range;
	public LongBowAttack(){
		baseDamage = 50;
		range = new PointRange(8,4,8,0);
	}

	public override float modifyAttack(IUnit attacker = null)
	{
		if(attacker == null)
		{
			endDamage = baseDamage;
		}
		else
		{
			endDamage = baseDamage + attacker.dexterity*2;
		}
		return endDamage;
	}

}
