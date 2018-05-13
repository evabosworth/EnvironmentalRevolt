 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Battlefield : ScriptableObject
{
	public Dictionary<Vector3, IObject> terrainDictionary;
	public Dictionary<Vector3, IObject> unitDictionary;
	public Dictionary<Vector2, float> terrainHeightDictionary = new Dictionary<Vector2, float>();
	GameObjectController objectController;

	public int maxHeight = 0;

	GlobalVariables gv;

	//pick a direciton PosX, NegX, PosY, NegY
	//neutral zone where no one can place? only a multiplayer concern, CPU can do what they want basically
		//TODO: address later when multiplayer matters
	//int direction = 0;



	public void setDictionary (Dictionary<Vector3, IObject> terrainDictionary)
	{

		objectController = FindObjectOfType<GameObjectController>();
		gv = GlobalVariables.getInstance ();
		gv.log ("Terrain generated and Set");

		this.terrainDictionary = terrainDictionary;
		create2DMap ();
		this.unitDictionary = new Dictionary<Vector3, IObject> ();
	}

	public List<IObject> convertListPosToListObject(List<Vector3> positions){
		List<IObject> codeObjects = new List<IObject> ();


		foreach (Vector3 pos in positions) {
			IObject codeObject;
			if (terrainDictionary.TryGetValue (pos, out codeObject)) {
				codeObjects.Add (codeObject);
			}
		}

		return codeObjects;
		
	}

	private void create2DMap(){



		foreach (Vector3 position in terrainDictionary.Keys) {
			float x = position.x;
			float z = position.z;

			Vector2 coord = new Vector2 (x, z);
			float y = position.y;

			if (y > maxHeight) {
				maxHeight = (int)y;
			}

			if (terrainHeightDictionary.ContainsKey (coord)) {
				if (terrainHeightDictionary [coord] < y) {
					terrainHeightDictionary [coord] = y;
				}


			} else {
				terrainHeightDictionary.Add (coord, y);
			}

		}
		
	}

	public bool canPlaceUnit(Vector3 position){
		bool canPlaceUnitHuh = true;
		Vector3 posUp = position;

		if (terrainDictionary.ContainsKey (position)) {
			//Actually this will check if the terrain is inpassable, make sure its a valid terrain to stand on.
		} else if (unitDictionary.ContainsKey (posUp)) {
			//Check place above for unit, eventually also 
			canPlaceUnitHuh = false;
		}
		return canPlaceUnitHuh;
	}

	public bool isValidTerrainForUnitPlacement(Vector3 position){
		Vector3 terrainPosition = position;
		position.y = position.y + gv.unitHeightModifier;
		Vector3 unitPosition =  position;
		Vector3 aboveTerrainPosition = terrainPosition;
		aboveTerrainPosition.y += gv.basicTerrainHeight;
		Vector3 aboveDoubleTerrainPosition = aboveTerrainPosition;
		aboveDoubleTerrainPosition.y += gv.basicTerrainHeight;

		bool returnValue = true;

		if (terrainDictionary.ContainsKey(aboveTerrainPosition) || terrainDictionary.ContainsKey(aboveDoubleTerrainPosition)) { 
			//if its not a top level terrain, ignore
			returnValue = false;
		} else if (unitDictionary.ContainsKey (unitPosition)) { 
			//if already have a unit, ignore
			returnValue = false;
		}

		return returnValue;
	}

	public void displayPossibleMovement(IObject obj){
		Vector3 unitPosition = obj.origPosition;
		Vector3 terrainUnderfoot = unitPosition;
		terrainUnderfoot.y -= gv.unitHeightModifier;

		//objectController.changeHighlighting
	}

	public bool addUnitToBattlefield(IObject obj){
		bool canPlace = canPlaceUnit(obj.position);

		if(canPlace){
			unitDictionary.Add (obj.position, obj);
		}
		return canPlace;

	}
		

	public void moveUnit(IObject codeObject, Vector3 toPos){

		objectController.moveObject (codeObject, toPos);

		toPos.y += gv.unitHeightModifier;

		unitDictionary.Remove (codeObject.position);
		codeObject.position = toPos;
		unitDictionary.Add (toPos, codeObject);
	}
}

