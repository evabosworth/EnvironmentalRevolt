using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : IMove
{
	public BasicMove(int distance, int jumpHeight){
        this.distance = distance;
        this.jumpHeight = jumpHeight;
    }

    public override List<Vector3> listPossibleMovementsRecursion(Vector3 unitPosition, int movementLeft, List<Vector3> visitedPositionsRecursive, int jumpHeight, Battlefield battlefield, Dictionary<Vector3, ITerrain> possiblePlacements)
    {
        GlobalVariables gv = GlobalVariables.getInstance();

        //gv.printToConsole(movementLeft.ToString());

        Vector3 unitXP1 = unitPosition;
        unitXP1.x += 1;
        Vector3 unitXM1 = unitPosition;
        unitXM1.x -= 1;
        Vector3 unitYP1 = unitPosition;
        unitYP1.y += 1;
        Vector3 unitYM1 = unitPosition;
        unitYM1.y -= 1;
        Vector3 unitZP1 = unitPosition;
        unitZP1.z += 1;
        Vector3 unitZM1 = unitPosition;
        unitZM1.z -= 1;


        //x+1,x-1,y+1,y-1

        if (movementLeft == 0)
        {
            return visitedPositionsRecursive;
        }
        //x+1
        if (battlefield.isValidForUnitPlacement(unitXP1))
        {
            if (!visitedPositionsRecursive.Contains(unitXP1) || movementLeft > 1)
            {
                if (!visitedPositionsRecursive.Contains(unitXP1))
                {
                    visitedPositionsRecursive.Add(unitXP1);
                }

                listPossibleMovementsRecursion(unitXP1, movementLeft - 1, visitedPositionsRecursive, jumpHeight, battlefield, possiblePlacements);


            }
        }
        else
        {

            for (int i = -jumpHeight; i < jumpHeight; i++)
            {
                Vector3 tempPos = new Vector3();
                tempPos = unitXP1;
                tempPos.z += i;
                if (possiblePlacements.ContainsKey(tempPos))
                {
                    if (!visitedPositionsRecursive.Contains(tempPos) || movementLeft > 1)
                    {
                        if (!visitedPositionsRecursive.Contains(tempPos))
                        {
                            visitedPositionsRecursive.Add(tempPos);
                        }
                        listPossibleMovementsRecursion(tempPos, movementLeft - 1, visitedPositionsRecursive, jumpHeight, battlefield, possiblePlacements);

                    }
                }
            }
        }

        //y+1
        if (battlefield.isValidForUnitPlacement(unitYP1))
        {
            if (!visitedPositionsRecursive.Contains(unitYP1) || movementLeft > 1)
            {
                if (!visitedPositionsRecursive.Contains(unitYP1))
                {
                    visitedPositionsRecursive.Add(unitYP1);
                }
                listPossibleMovementsRecursion(unitYP1, movementLeft - 1, visitedPositionsRecursive, jumpHeight, battlefield, possiblePlacements);


            }
        }
        else
        {

            for (int i = -jumpHeight; i < jumpHeight; i++)
            {
                Vector3 tempPos = new Vector3();
                tempPos = unitYP1;
                tempPos.z += i;
                if (possiblePlacements.ContainsKey(tempPos))
                {
                    if (!visitedPositionsRecursive.Contains(tempPos) || movementLeft > 1)
                    {
                        if (!visitedPositionsRecursive.Contains(tempPos))
                        {
                            visitedPositionsRecursive.Add(tempPos);
                        }
                        listPossibleMovementsRecursion(tempPos, movementLeft - 1, visitedPositionsRecursive, jumpHeight, battlefield, possiblePlacements);

                    }
                }
            }
        }

        //x-1      
        if (battlefield.isValidForUnitPlacement(unitXM1))
        {
            if (!visitedPositionsRecursive.Contains(unitXM1) || movementLeft > 1)
            {
                if (!visitedPositionsRecursive.Contains(unitXM1))
                {
                    visitedPositionsRecursive.Add(unitXM1);

                }
                listPossibleMovementsRecursion(unitXM1, movementLeft - 1, visitedPositionsRecursive, jumpHeight, battlefield, possiblePlacements);


            }
        }
        else
        {

            for (int i = -jumpHeight; i < jumpHeight; i++)
            {
                Vector3 tempPos = new Vector3();
                tempPos = unitXM1;
                tempPos.z += i;
                if (possiblePlacements.ContainsKey(tempPos))
                {
                    if (!visitedPositionsRecursive.Contains(tempPos) || movementLeft > 1)
                    {
                        if (!visitedPositionsRecursive.Contains(tempPos))
                        {
                            visitedPositionsRecursive.Add(tempPos);
                        }
                        listPossibleMovementsRecursion(tempPos, movementLeft - 1, visitedPositionsRecursive, jumpHeight, battlefield, possiblePlacements);

                    }
                }
            }
        }

        //y-1
        if (battlefield.isValidForUnitPlacement(unitYM1))
        {
            if (!visitedPositionsRecursive.Contains(unitYM1) || movementLeft > 1)
            {
                if (!visitedPositionsRecursive.Contains(unitYM1))
                {
                    visitedPositionsRecursive.Add(unitYM1);
                }
                listPossibleMovementsRecursion(unitYM1, movementLeft - 1, visitedPositionsRecursive, jumpHeight, battlefield, possiblePlacements);


            }
        }
        else
        {

            for (int i = -jumpHeight; i < jumpHeight; i++)
            {
                Vector3 tempPos = new Vector3();
                tempPos = unitYM1;
                tempPos.z += i;
                if (possiblePlacements.ContainsKey(tempPos))
                {
                    if (!visitedPositionsRecursive.Contains(tempPos) || movementLeft > 1)
                    {
                        if (!visitedPositionsRecursive.Contains(tempPos))
                        {
                            visitedPositionsRecursive.Add(tempPos);
                        }
                        listPossibleMovementsRecursion(tempPos, movementLeft - 1, visitedPositionsRecursive, jumpHeight, battlefield, possiblePlacements);

                    }
                }
            }
        }


        return visitedPositionsRecursive;
    }
}
