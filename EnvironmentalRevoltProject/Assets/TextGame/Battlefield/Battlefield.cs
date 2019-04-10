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

		//createFlatland (length,width,depth);
		createStepland(length,width,depth);
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
						str += curTile.toString ();
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


    public void printThingsOnBattlefield(Dictionary<Vector3, IWorldObject> battlefield){
		String str;
		str = "[bfObjects]";

		for (int z = 0; z <= depth; z++) {
			for (int y = 0; y < width; y++) {
				for (int x = 0; x < length; x++) {
					IWorldObject curObject;
					if(battlefield.TryGetValue(new Vector3(x,y,z), out curObject)){
						str += curObject.toString ();
					}
				}
			}
			if(str != "")
				GlobalVariables.getInstance ().printToConsole (str);
			str = "";
		}

	}

    public Dictionary<Vector3, ITerrain> listPossibleMovements(IUnit unit)
    {
        Dictionary<Vector3, ITerrain> possibleMoves = new Dictionary<Vector3, ITerrain>();
        Vector3 unitPosInitial = unit.getCurrentPosition();
        List<Vector3> visitedPositionsRecursive = new List<Vector3>();
        int movementLeft = unit.getMovement();
        List<Vector3> visitedPositions = listPossibleMovementsRecursion(unitPosInitial, movementLeft, visitedPositionsRecursive);
        foreach (Vector3 item in visitedPositions)
        {
            ITerrain terrain;
            battlefield.TryGetValue(item, out terrain);
            possibleMoves.Add(item, terrain);
        }

        return possibleMoves;
    }

    private List<Vector3> listPossibleMovementsRecursion(Vector3 unitPosition, int movementLeft, List<Vector3> visitedPositionsRecursive)
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
        if (isValidForUnitPlacement(unitXP1))
        {
            if (!visitedPositionsRecursive.Contains(unitXP1) || movementLeft > 1)
            {
                //gv.printToConsole(unitXP1.ToString());
                if (!visitedPositionsRecursive.Contains(unitXP1))
                {
                    visitedPositionsRecursive.Add(unitXP1);
                }
                List<Vector3> holderList = new List<Vector3>();
                holderList = listPossibleMovementsRecursion(unitXP1, movementLeft - 1, visitedPositionsRecursive);


            }
        }
        //y+1
        if (isValidForUnitPlacement(unitYP1))
        {
            if (!visitedPositionsRecursive.Contains(unitYP1)||movementLeft > 1)
            {
                if (!visitedPositionsRecursive.Contains(unitYP1))
                {
                    visitedPositionsRecursive.Add(unitYP1);
                }
                List<Vector3> holderList = new List<Vector3>();
                holderList = listPossibleMovementsRecursion(unitYP1, movementLeft - 1, visitedPositionsRecursive);


            }
        }
        //z+1
        if (isValidForUnitPlacement(unitZP1))
        {
            if (!visitedPositionsRecursive.Contains(unitZP1) || movementLeft > 1)
            {
                if (!visitedPositionsRecursive.Contains(unitZP1))
                {
                    visitedPositionsRecursive.Add(unitZP1);
                }
                List<Vector3> holderList = new List<Vector3>();
                holderList = listPossibleMovementsRecursion(unitZP1, movementLeft - 1, visitedPositionsRecursive);


            }
        }
        //x-1      
        if (isValidForUnitPlacement(unitXM1))
        {
            if (!visitedPositionsRecursive.Contains(unitXM1)||movementLeft > 1)
            {
                if (!visitedPositionsRecursive.Contains(unitXM1))
                {
                    visitedPositionsRecursive.Add(unitXM1);

                }
                List<Vector3> holderList = new List<Vector3>();
                holderList = listPossibleMovementsRecursion(unitXM1, movementLeft - 1, visitedPositionsRecursive);


            }
        }
        //y-1
        if (isValidForUnitPlacement(unitYM1))
        {
            if (!visitedPositionsRecursive.Contains(unitYM1)||movementLeft > 1)
            {
                if (!visitedPositionsRecursive.Contains(unitYM1))
                {
                    visitedPositionsRecursive.Add(unitYM1);
                }
                List<Vector3> holderList = new List<Vector3>();
                holderList = listPossibleMovementsRecursion(unitYM1, movementLeft - 1, visitedPositionsRecursive);


            }
        }
        //z-1      
        if (isValidForUnitPlacement(unitZM1))
        {
            if (!visitedPositionsRecursive.Contains(unitZM1) || movementLeft > 1)
            {
                if (!visitedPositionsRecursive.Contains(unitZM1))
                {
                    visitedPositionsRecursive.Add(unitZM1);

                }
                List<Vector3> holderList = new List<Vector3>();
                holderList = listPossibleMovementsRecursion(unitZM1, movementLeft - 1, visitedPositionsRecursive);


            }
        }

        return visitedPositionsRecursive;
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