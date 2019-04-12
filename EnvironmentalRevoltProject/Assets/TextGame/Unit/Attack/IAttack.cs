using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAttack
{
	protected IRange range;
	protected float baseDamage;
    protected float endDamage;

	public IRange Range {
		get {
			return range;
		}
	}
    public float getDamage(IUnit attacker = null)
    {
        return modifyAttack(attacker);
    }
    public abstract float modifyAttack(IUnit attacker = null);
}
