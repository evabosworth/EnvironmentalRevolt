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
}