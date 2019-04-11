using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUnit : IWorldObject
{
	//Random start health vaue
	protected float maxHealth = 100.0f;
	//squares you can move in cardinal directions
	protected List<IAttack> attacks;
	protected IPlayer unitOwner;
    protected IMove move;

    public int getJumpHeight()
    {
        return this.move.getJumpHeight();
    }

    public IMove getMovementType()
    {
        return move;
    }

    public int getMovement()
    {
        return move.getMovement();
    }

    public IUnit(int movement, int jumpHeight)
    {
        move = new BasicMove(movement, jumpHeight);
    }
}

