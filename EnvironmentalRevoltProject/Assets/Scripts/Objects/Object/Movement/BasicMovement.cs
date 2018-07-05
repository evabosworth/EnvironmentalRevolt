using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicMovement : ScriptableObject, IMovement
{
	int distance;
	GlobalVariables gv;
	List<Vector3> visitedPositions;

	float jumpHeight;


	//Allow movement (distance) squares in any direction, ignore height. 
	public BasicMovement(int distance, int jumpHeightUnits){
		gv = GlobalVariables.getInstance ();
		this.distance = distance;
		jumpHeight = gv.basicTerrainHeight * jumpHeightUnits;
	}

	public List<Vector3> findPossibleMovement (Vector3 startPos){
		visitedPositions = new List<Vector3> ();

		List<Vector3> validMovements = findAllValidNeighbors (startPos, 0);

		return validMovements;
	}

	private List<Vector3> findAllValidNeighbors (Vector3 pos, int curDistance){
		gv = GlobalVariables.getInstance ();

		Dictionary<Vector3, IObject> terrainDictionary = gv.battlefield.terrainDictionary;
		List<Vector3> validPositions = new List<Vector3>();
		float curHeight = gv.battlefield.maxHeight*2;

		validPositions.Add (pos);

		if (curDistance >= distance) {
			return validPositions;
		}
		visitedPositions.Add (pos);

		while(curHeight >= 0){
			/* Movement is valid when:
			 * height of new terrain is within jumping distance
			 * There are two empty tiles above the tile.
			 */

			IObject terrainXM1;
			IObject terrainXP1;
			IObject terrainZM1;
			IObject terrainZP1;

			//x-1,z
			if (terrainDictionary.TryGetValue (new Vector3 (pos.x - 1, curHeight, pos.z), out terrainXM1)) {
				if(isValidMove(terrainXM1, pos)){
					List<Vector3> allNeighbors = findAllValidNeighbors (terrainXM1.position, curDistance + 1);
					validPositions.AddRange (allNeighbors);
				}
			}

			//x+1,z
			if (terrainDictionary.TryGetValue (new Vector3 (pos.x + 1, curHeight, pos.z), out terrainXP1)) {
				if(isValidMove(terrainXP1, pos)){
					List<Vector3> allNeighbors = findAllValidNeighbors (terrainXP1.position, curDistance + 1);
					validPositions.AddRange (allNeighbors);
				}
				
			}

			//x,z-1
			if (terrainDictionary.TryGetValue (new Vector3 (pos.x, curHeight, pos.z - 1), out terrainZM1)) {
				if(isValidMove(terrainZM1, pos)){
					List<Vector3> allNeighbors = findAllValidNeighbors (terrainZM1.position, curDistance + 1);
					validPositions.AddRange (allNeighbors);
				}
				
			}

			//x,z+1
			if (terrainDictionary.TryGetValue (new Vector3 (pos.x, curHeight, pos.z + 1), out terrainZP1)) {
				if(isValidMove(terrainZP1, pos)){
					List<Vector3> allNeighbors = findAllValidNeighbors (terrainZP1.position, curDistance + 1);
					validPositions.AddRange (allNeighbors);
				}
				
			}
				
			curHeight -= gv.basicTerrainHeight;
		}


		return validPositions;

	}

	private bool isValidMove(IObject toTerrain, Vector3 fromPos){
		
		if (toTerrain == null) {
			return false;
		}

		//Terrain Already Visited
		if (visitedPositions.Contains (toTerrain.position)) {
			return false;
		}


		//To far to jump;
		if (toTerrain.position.y > fromPos.y + jumpHeight || toTerrain.position.y < fromPos.y - jumpHeight) {
			return false;
		}

		//Unit Already in the place.
		if (!gv.battlefield.isValidTerrainForUnitPlacement (toTerrain.position)) {


			//Arleady a unit there
			IObject movingUnit;
			fromPos.y += gv.unitHeightModifier;
			gv.battlefield.unitDictionary.TryGetValue(fromPos, out movingUnit);

			string strTest = gv.battlefield.unitDictionary.Keys.ToString ();

			//If there is no unit in the way, or i am the unit in the way
			if (movingUnit == null || movingUnit.origPosition.Equals(fromPos)) {
				return false;
			}
		}


		return true;
	}

}

