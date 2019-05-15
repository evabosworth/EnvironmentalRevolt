using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Battlefield
{
	private Dictionary<Vector3, ITerrain> battlefield = new Dictionary<Vector3, ITerrain> ();
	private Dictionary<Vector3, IWorldObject> objectsOnfield = new Dictionary<Vector3, IWorldObject>();
	//private Dictionary<Vector3, ???> magicOverlay;
	private int length = 25;
	private int width = 25;
	private int depth = 10;

	public Battlefield(){

		createFlatland (length,width,depth);
		//createStepland(length,width,depth);
	}

	public Dictionary<Vector3, ITerrain> getBattlefield(){
		return battlefield;
	}

	public Dictionary<Vector3, ITerrain> getAllValidPlacements(){
		Dictionary<Vector3, ITerrain> result = new Dictionary<Vector3, ITerrain> ();
		//Get all non-air terrain with an air terrain above it
		for (int x = 0; x < length; x++) {
			for (int y = 0; y < width; y++) {
				for (int z = 0; z <= depth; z++) {
					Vector3 pos = new Vector3 (x, y, z);
					ITerrain posTerrain;
					if (battlefield.TryGetValue (pos, out posTerrain)) {
						if (isValidForUnitPlacement (pos)) {
							result.Add (pos, posTerrain);
						}
					}
				}
			}
		}

		return result;
		
	}

	public bool tryPlaceUnitAtPosition(Vector3 unitPlacePosition, IWorldObject objectToPlace){
		bool isValidForPlacement = isValidForUnitPlacement (unitPlacePosition, objectToPlace);
		if (isValidForPlacement) {
			objectsOnfield.Add (unitPlacePosition, objectToPlace);
			objectToPlace.setCurrentPosition (unitPlacePosition);
		}

		return isValidForPlacement;
	}

	public bool isValidForUnitPlacement(Vector3 unitPlacePosition, IWorldObject objectToPlace = null){
		ITerrain desiredSpot;
		bool spotCanBePlaced = false;
		bool spotAboveCanBeOccupied = false;

		if (!battlefield.TryGetValue (unitPlacePosition, out desiredSpot)) {
			//That position doesn't exist in the battlefield
			return false;
		}
		//If null, just assume whatever is default of the class
		spotCanBePlaced = desiredSpot.allowPlacement(objectToPlace);
			
		Vector3 abovePlacePosition = unitPlacePosition;
		abovePlacePosition.z += 1;

		ITerrain aboveSpot;
		if (!battlefield.TryGetValue (abovePlacePosition, out aboveSpot)) {
			//above spot doesn't exist
			spotAboveCanBeOccupied = false;

		} else {

			spotAboveCanBeOccupied = aboveSpot.allowOccupation (objectToPlace);
		}

		return  spotCanBePlaced && spotAboveCanBeOccupied;
	}

	public void printBattlefield(){
		printBattlefield (battlefield);
		printThingsOnBattlefield (objectsOnfield);
	}

	public void printBattlefield(Dictionary<Vector3, ITerrain> battlefield){
		String str;
		str = "[battlefield]";

		for (int z = 0; z <= depth; z++) {
			for (int y = 0; y < width; y++) {
				for (int x = 0; x < length; x++) {
					ITerrain curTile;
					if(battlefield.TryGetValue(new Vector3(x,y,z), out curTile)){
						str += curTile.ToString ();
					}
				}
			}
			if(str != "")
				GlobalVariables.getInstance ().printToConsole (str);
			str = "";
		}
	
	}

    public void printThingsOnBattlefield()
    {
        printThingsOnBattlefield(objectsOnfield);
    }

    public void printListOfIWorldObjects(List<IWorldObject> input)
    {
        string str = null;
        foreach (IWorldObject item in input)
        {
            str += item.ToString();
        }
        GlobalVariables.getInstance().printToConsole(str);
    }


    public void printThingsOnBattlefield(Dictionary<Vector3, IWorldObject> battlefield){
		String str;
		str = "[bfObjects]";

		for (int z = 0; z <= depth; z++) {
			for (int y = 0; y < width; y++) {
				for (int x = 0; x < length; x++) {
					IWorldObject curObject;
					if(battlefield.TryGetValue(new Vector3(x,y,z), out curObject)){
						str += curObject.ToString ();
					}
				}
			}
			if(str != "")
				GlobalVariables.getInstance ().printToConsole (str);
			str = "";
		}

	}

    public void printUnitsOnBattlefield(Dictionary<Vector3, IWorldObject> battlefield)
    {
        String str;
        str = "[bfObjects]";
        IWorldObject curObject;

        for (int z = 0; z <= depth; z++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {

                    if (battlefield.TryGetValue(new Vector3(x, y, z), out curObject))
                    {
                        curObject = null;
                        if (curObject.GetType().IsInstanceOfType(typeof(IUnit)))
                        {
                            str += curObject.ToString();
                        }

                    }
                }
            }
            if (str != "")
                GlobalVariables.getInstance().printToConsole(str);
            str = "";
        }
    }


    public Dictionary<Vector3, ITerrain> listPossibleMovements(IUnit unit)
    {
        Dictionary<Vector3, ITerrain> possibleMoves = new Dictionary<Vector3, ITerrain>();
        Vector3 unitPosInitial = unit.getCurrentPosition();
        List<Vector3> visitedPositionsRecursive = new List<Vector3>();
        int movementLeft = unit.getMovement();
        Dictionary<Vector3, ITerrain> possiblePlacements;
        possiblePlacements = getAllValidPlacements();
        List<Vector3> visitedPositions = unit.getMovementType().listPossibleMovementsRecursion(unitPosInitial, movementLeft, visitedPositionsRecursive, unit.getJumpHeight(), this, possiblePlacements);
        foreach (Vector3 item in visitedPositions)
        {
            ITerrain terrain;
            battlefield.TryGetValue(item, out terrain);
            possibleMoves.Add(item, terrain);
        }

        return possibleMoves;
    }



    public bool tryMoveUnit(IUnit unit, Vector3 toPos, Dictionary<Vector3,ITerrain> possibleMoves = null)
    {
        if (possibleMoves == null)
        {
            possibleMoves = listPossibleMovements(unit);
        }
        ITerrain terrain;
        if(possibleMoves.TryGetValue(toPos, out terrain))
        {
            objectsOnfield.Remove(unit.getCurrentPosition());
            unit.setCurrentPosition(toPos);
            objectsOnfield.Add(toPos, unit);
            return true;
        }
        else { return false; }
    }

    public List<IWorldObject> listPossibleTargets(List<Vector3> positions)
    {
        List<IWorldObject> output = new List<IWorldObject>();
        IWorldObject curObject;
        foreach (Vector3 position in positions)
        {
            curObject = null;
            if (objectsOnfield.TryGetValue(position, out curObject))
            {
                output.Add(curObject);
            }
        }
        return output;
    }

    public bool tryAttackWithUnit(IUnit attacker, IWorldObject target, IAttack attack, List<IWorldObject> possibleUnitsToAttack)
    {
        if (possibleUnitsToAttack.Contains(target))
        {
            bool isDead = false;
            isDead = attacker.makeAttack(attack, target, attacker);
            if (isDead)
            {
                attacker.addExperienceToUnit(target.getExperienceReward());
                //TODO add a body when a unit dies
                objectsOnfield.Remove(target.getCurrentPosition());
            }
            return true;
        }
        else return false;
    }




	private void createFlatland(int length, int width, int depth){

		for (int x = 0; x < length; x++) {
			for (int y = 0; y < width; y++) {
				for (int z = 0; z <= depth; z++) {
					if (z == 0) {
						//BedrockLayer
						ITerrain bedrock = new Bedrock (new Vector3 (x, y, z));
						battlefield.Add (new Vector3 (x, y, z), bedrock);
					} else if (z<=2){
						ITerrain stone = new Stone (new Vector3 (x, y, z));
						battlefield.Add (new Vector3 (x, y, z), stone);

					} else if (z<=depth/2){
						ITerrain dirt = new Dirt (new Vector3 (x, y, z));
						battlefield.Add (new Vector3 (x, y, z), dirt);
					} else{
						//} else if (z<=depth-1){
						ITerrain air = new Air (new Vector3 (x, y, z));
						battlefield.Add (new Vector3 (x, y, z), air);

						//ITerrain layline = new Layline (new Vector3 (x, y, z));
						//battlefield.Add (new Vector3 (x, y, z), layline);
					}
				}
			}
		}
	}


	private void createStepland(int length, int width, int depth){

		int height = 1;

		for (int x = 0; x < length; x++) {
			for (int y = 0; y < width; y++) {
				for (int z = 0; z <= depth; z++) {
					if (z == 0) {
						//BedrockLayer
						ITerrain bedrock = new Bedrock (new Vector3 (x, y, z));
						battlefield.Add (new Vector3 (x, y, z), bedrock);
					} else if (z>=height){
						ITerrain air = new Air (new Vector3 (x, y, z));
						battlefield.Add (new Vector3 (x, y, z), air);
					} else{
						ITerrain dirt = new Dirt (new Vector3 (x, y, z));
						battlefield.Add (new Vector3 (x, y, z), dirt);

					}

				}
			}
			height++;
		}
	}


}