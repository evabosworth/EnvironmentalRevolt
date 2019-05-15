using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldManager
{
	protected Battlefield battlefield;
	private GlobalVariables gv;

	public BattlefieldManager (){
 		string starttime = DateTime.Now.ToLongTimeString();
        gv = GlobalVariables.getInstance ();

		battlefield = new Battlefield ();

		/*
		Dictionary<Vector3, ITerrain> validPositions = battlefield.getAllValidPlacements ();
		battlefield.printBattlefield(validPositions);
		*/



		inputRun ();

        string endtime = DateTime.Now.ToLongTimeString();
        gv.printToConsole(starttime+"       "+endtime+"");
        //gv.printToConsole((endtime - starttime) + "");

	}

	//Get a value from user input
	//And do whatever needs to be done with it.
	public void acceptInput(int inputValue){
	
	}

	private void inputRun(){
		IUnit warrior0 = new Warrior ();
		IUnit archer0 = new Archer();
		IUnit warrior1 = new Warrior();

		Vector3 pos = new Vector3 (10, 10, 5);
		Vector3 pos2 = new Vector3(10, 4, 5);
		Vector3 pos3 = new Vector3(9, 5, 5);
	}

	//A list of actions to test things as if user input
	private void exampleRun(){
		//Dictionary<Vector3, ITerrain> validPositions = battlefield.getAllValidPlacements ();

		IUnit unit = new Warrior ();
        IUnit unit2 = new Warrior();
        IUnit unit3 = new Warrior();

		Vector3 pos = new Vector3 (10, 10, 5);
        Vector3 pos2 = new Vector3(10, 4, 5);
        Vector3 pos3 = new Vector3(9, 5, 5);


		bool isPlaced = tryPlaceUnitOntoBattlefield (pos, unit);
        isPlaced = tryPlaceUnitOntoBattlefield(pos2, unit2); 
        isPlaced = tryPlaceUnitOntoBattlefield(pos3, unit3);
       


        //battlefield.printBattlefield ();
        //battlefield.printBattlefield(validPositions);

        Dictionary<Vector3,ITerrain> possibleMovements = new Dictionary<Vector3, ITerrain>();
        possibleMovements = battlefield.listPossibleMovements(unit);
        // battlefield.printBattlefield(possibleMovements);
        // battlefield.printThingsOnBattlefield();
        battlefield.tryMoveUnit(unit, new Vector3(10.0f, 5.0f, 5.0f));        
		//List<Vector3> possibleTerrainTargets = unit.Attacks[0].Range.getAllValidTerrainTargets(unit.getCurrentPosition(), battlefield);
		List<Vector3> possibleTerrainTargets;// = unit.Attacks[0].Range.getAllMaxEffectiveValidTerrainTargets(unit.getCurrentPosition(), battlefield);
		//List<IWorldObject> possibleAttackUnits;// = battlefield.listPossibleTargets(possibleTerrainTargets);

		/*
        battlefield.printListOfIWorldObjects(possibleAttackUnits);
		battlefield.tryAttackWithUnit(unit, unit2, unit.Attacks[0], possibleAttackUnits);
		battlefield.printThingsOnBattlefield();
		battlefield.tryAttackWithUnit(unit, unit2, unit.Attacks[0], possibleAttackUnits);
		battlefield.printThingsOnBattlefield();
		battlefield.tryAttackWithUnit(unit, unit2, unit.Attacks[0], possibleAttackUnits);
		battlefield.printThingsOnBattlefield();
		battlefield.tryAttackWithUnit(unit, unit2, unit.Attacks[0], possibleAttackUnits);
		battlefield.printThingsOnBattlefield();
        battlefield.tryAttackWithUnit(unit, unit3, unit.Attacks[0], possibleAttackUnits);
        */


		//possibleTerrainTargets = unit.Attacks[1].Range.getAllValidTerrainTargets(unit.getCurrentPosition(), battlefield);
		possibleTerrainTargets = unit.Attacks[1].Range.getAllMaxEffectiveValidTerrainTargets(unit.getCurrentPosition(), battlefield);

		printList<Vector3> (possibleTerrainTargets);

        battlefield.printThingsOnBattlefield();



    }

	public bool tryPlaceUnitOntoBattlefield(Vector3 position, IUnit unit){

		//Try and Place object at location, returns bool
		bool isPlaced = battlefield.tryPlaceUnitAtPosition (position, unit);


		return isPlaced;

	}

    public void printList<T>(List<T> list)
    {
		int count = 0;
		foreach (T item in list)
        {
            gv.printToConsole("[" + count + "]" +item.ToString());
			count++;
        }
    }



}

