using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUnit : IWorldObject
{
	//Random start health vaue
	protected float maxHealth = 100.0f;
	//squares you can move in cardinal directions
	protected int movement = 4;
	protected List<IAttack> attacks;
	protected IPlayer unitOwner;

}

