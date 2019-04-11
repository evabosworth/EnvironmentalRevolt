using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAttack
{
	protected IRange range;
	protected float damage = 20.0f;

	public IRange Range {
		get {
			return range;
		}
	}
    public float Damage
    {
        get
        {
            return damage;
        }
    }
}
