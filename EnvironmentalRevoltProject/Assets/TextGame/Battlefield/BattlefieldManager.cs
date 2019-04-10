using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldManager
{
	protected Battlefield battlefield;
	private GlobalVariables gv;

	public BattlefieldManager (){
		gv = GlobalVariables.getInstance ();
		battlefield = new Battlefield ();

		Dictionary<Vector3, ITerrain> validPositions = battlefield.getAllValidPlacements ();


		//exampleRun ();
		battlefield.printBattlefield(validPositions);
	}

	//A list of actions to test things as if user input
	public void exampleRun(){
		Dictionary<Vector3, ITerrain> validPositions = battlefield.getAllValidPlacements ();

		IUnit unit = new Warrior ();
		Vector3 pos = new Vector3 (0, 0, 0);

		bool isPladced = tryPlaceUnitOntoBattlefield (pos, unit);
		if (isPladced) {
			unit.setCurrentPosition (pos);
		}

		//battlefield.printBattlefield ();
       	//battlefield.printBattlefield(validPositions);

        Dictionary<Vector3,ITerrain> possibleMovements = new Dictionary<Vector3, ITerrain>();
        possibleMovements = battlefield.listPossibleMovements(unit);
        battlefield.printBattlefield(possibleMovements);
        //battlefield.tryMoveUnit(unit, new Vector3(0.0f, 11.0f, 5.0f));
        battlefield.printThingsOnBattlefield();


    }

	public bool tryPlaceUnitOntoBattlefield(Vector3 position, IUnit unit){

		//Try and Place object at location, returns bool
		bool isPlaced = battlefield.tryPlaceUnitAtPosition (position, unit);


		return isPlaced;

	}

    public void printListOfMovement(List<Vector3> possibleMovements)
    {
        foreach (Vector3 item in possibleMovements)
        {
            gv.printToConsole(item.ToString());
        }
    }

}

