using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMove
{
	protected int distance;
    protected int jumpHeight;

    public int getJumpHeight()
    {
        return jumpHeight;
    }

    public int getMovement()
    {
        return distance;
    }

    public abstract List<Vector3> listPossibleMovementsRecursion(Vector3 unitPosition, int movementLeft, List<Vector3> visitedPositionsRecursive, int jumpHeight, Battlefield battlefield, Dictionary<Vector3, ITerrain> possiblePlacements);

}
