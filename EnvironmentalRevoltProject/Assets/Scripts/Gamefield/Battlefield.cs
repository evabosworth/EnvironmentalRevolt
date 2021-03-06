﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Battlefield holds all information on the combat
 * Should be the only point of access between other code that interacts 
 * 	with GameObjectController on battlefield Objects
 * 
 * Also maintains all troops and determines all their valid moevements.
 */
public class Battlefield : ScriptableObject
{
	public Dictionary<Vector3, IObject> terrainDictionary;
	public Dictionary<Vector3, IObject> unitDictionary;
	public List<IObject> turnOrder = new List<IObject> ();
	public int curTurn;
	//public Dictionary<Vector3, IObject> obstacaleDictionary;
	public List<IObject> hightlightedObjects;
	public List<IPlayer> playerList; //player 0 always human?

	//This should be controlled by player, not battlefield
	/*
	public Dictionary<Vector3, IObject> friendlyTerrainDictionary;
	public Dictionary<Vector3, IObject> enemyTerrainDictionary;
	*/

	public Dictionary<Vector2, float> terrainHeightDictionary = new Dictionary<Vector2, float>();
	GameObjectController objectController;

	public int maxHeight = 0;

	GlobalVariables gv;

	public void readyForPlayer(){
		IDictionaryEnumerator enumerable = playerList [0].startingPositions.GetEnumerator ();

		while (enumerable.MoveNext()) {
			
			changeHighlight ((IObject)(enumerable.Value), "ValidHighlighting", true);

		}
	}

	public IObject GetNextTurnUnit(){

		if (turnOrder.Count <= 0) {

			foreach(IObject obj in unitDictionary.Values){
				if (obj != null) {
					turnOrder.Add (obj);
				}
			}
			curTurn = 0;
		}

		gv.log ("CurTurn"+ curTurn);


		if (curTurn >= turnOrder.Count) {
			curTurn = 0;
		}

		IObject result = turnOrder [curTurn];
		curTurn++;
		return result;
	}


	//pick a direciton PosX, NegX, PosY, NegY
	//neutral zone where no one can place?
	//int direction = 0;



	//the initalization from MapGen
	//Need to add teams and such
	public void setTerrainDictionary (Dictionary<Vector3, IObject> terrainDictionary)
	{
		objectController = FindObjectOfType<GameObjectController>();
		gv = GlobalVariables.getInstance ();
		gv.log ("Terrain generated and Set");

		this.terrainDictionary = terrainDictionary;

		foreach (KeyValuePair<Vector3, IObject> entry in terrainDictionary) {
			objectController.createAndDisplayGameObject (entry.Value);
		}

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

	/**
	 * isValidTerrainForInitialUnitPlacement:
	 * allows for players to have set positions in which to place within the battlefield
	 */
	public bool isValidTerrainForInitialUnitPlacement(Vector3 position){
		
		IPlayer player = playerList[0];
		bool result = isValidTerrainForUnitPlacement (position);

		Dictionary<Vector3, IObject> startingPositions = player.startingPositions;
		IObject terrainResult;

		result = result && (startingPositions.TryGetValue (position, out terrainResult));

		return result;
	}

	//Add parameter for which player?
	public bool isValidTerrainForUnitPlacement(Vector3 position){
		bool canPlaceUnitHuh = true;

		IObject obj = null;


		if (terrainDictionary.TryGetValue (position, out obj)) {
			//Actually this will check if the terrain is inpassable, make sure its a valid terrain to stand on.
			Vector3 posY1 = position;
			posY1.y += gv.basicTerrainHeight;
			IObject objY1;

			Vector3 posY2 = position;
			posY2.y += gv.basicTerrainHeight*2;
			IObject objY2;

			terrainDictionary.TryGetValue (posY1, out objY1);
			terrainDictionary.TryGetValue (posY2, out objY2);

			if (objY1 != null || objY2 != null) {
				//if both blocks above are full, dont place unit
				canPlaceUnitHuh= false;

			}

		}


		if (unitDictionary.TryGetValue (position, out obj)) {
			if (obj != null) {
				canPlaceUnitHuh = false;
				gv.log ("can't place cuz unit in way");
				gv.log (obj.displayName);
			} 
		} 
		
		return canPlaceUnitHuh;
	}

	public bool isValidTerrainForUnitMovement(IObject movingUnit, Vector3 position){
		if (movingUnit == null) {
			return isValidTerrainForUnitPlacement (position);
		}
		
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
			IObject unitAtPos;
			unitDictionary.TryGetValue (unitPosition, out unitAtPos);

			if (unitAtPos.origPosition.Equals(movingUnit.origPosition)) {
				returnValue = true;
			} else {
				returnValue = false;
			}

		}

		return returnValue;
	}
		

	public bool addObjectToBattlefield(IObject obj){
		bool canPlace = isValidTerrainForUnitPlacement(obj.position);

		if(canPlace){
			unitDictionary.Add (obj.position, obj);
			objectController.createAndDisplayGameObject (obj);
		}
		return canPlace;
	}
		

	public void moveUnit(IObject codeObject, Vector3 toPos){

		objectController.moveObject (codeObject, toPos);

		toPos.y += gv.unitHeightModifier;


		unitDictionary.Remove (codeObject.origPosition);
		codeObject.position = toPos;
		unitDictionary.Add (toPos, codeObject);
	}

	public void addHighlights(List<IObject> objs, string highlightName){
		objectController.addHighlights(objs, highlightName);

	}

	public void changeHighlight(IObject obj, string highlightName, bool active){
		objectController.changeHighlight (obj, highlightName, active);
	}

	public void removeListHighlights(List<IObject> objs, string highlightName){
		objectController.removeHighlights(objs, highlightName);
	}

	public void removeDictionaryHighlights(Dictionary<Vector3, IObject> objs, string highlightName){
		
		IDictionaryEnumerator enumerable = playerList [0].startingPositions.GetEnumerator ();

		while (enumerable.MoveNext()) {

			changeHighlight ((IObject)(enumerable.Value), "ValidHighlighting", false);

		}
	}


	public void removeHighlights(IObject codeObj){
		gv = GlobalVariables.getInstance ();
		if (codeObj == null) {
			return;
		}

		changeHighlight (codeObj, "BasicHighlighting", false);
		changeHighlight (codeObj, "InvalidHighlighting", false);

	}

	public IObject searchBattlefield(GameObject hitObject){
		IObject codeHitObject;

		terrainDictionary.TryGetValue (hitObject.transform.position, out codeHitObject);
		if (codeHitObject != null) {
			return codeHitObject;
		}

		unitDictionary.TryGetValue (hitObject.transform.position, out codeHitObject);
		if (codeHitObject != null) {
			return codeHitObject;
		}


		return null;
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

	public void highlightPlaceableTerrain(){
		objectController = FindObjectOfType<GameObjectController>();
		if (hightlightedObjects == null) {
			hightlightedObjects = GlobalVariables.convertDictionaryToList(playerList [0].startingPositions);
			objectController.addHighlights (hightlightedObjects, "MovementHightlight");

		}
	}

	public void removeUnit(IObject codeObject){
		bool canRemove = !isValidTerrainForUnitPlacement(codeObject.position);

		if (canRemove) {
			bool removed = unitDictionary.Remove (codeObject.position);

			objectController = FindObjectOfType<GameObjectController> ();
			objectController.removeObject (codeObject);
		}
	}


	public void removeTerrainHighlights(string highlighName){
		removeDictionaryHighlights (terrainDictionary, highlighName);
	}

	public void displaySelectedObject(IObject codeObject){
		objectController = FindObjectOfType<GameObjectController> ();
		objectController.displaySelectedObject (codeObject);
		

	}

}

