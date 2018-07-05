using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitState : IPlayerState  {

	public FindPossibleMovements lastMove;
	public FindPossibleMovements move;
	public List<Node> possibleMovements;

	private GlobalVariables gv;
	public static IObject highlightedUnit = null;
	public static IObject unit = null;
	public static List<IObject> highlightedTerrain = new List<IObject> ();


	public override IPlayerState clickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		// whatever tag you are looking for on your game object
		if (hit.collider.tag == "Character") {
			gv.log ("SelectUnitState: Character Clicked -> MoveUniteState");
			GameObject hitObject = hit.collider.gameObject;
			IObject codeHitObject = gv.battlefield.searchBattlefield (hitObject);

			if (!hitObject.Equals (highlightedUnit)) {
				//Remove old unit highlighting
				removeHighlights(highlightedUnit);

				gv.battlefield.changeHighlight (codeHitObject, "BasicHighlighting", true);
				//update what unit is higlighted
				highlightedUnit = codeHitObject;

				gv.battlefield.unitDictionary.TryGetValue(hitObject.transform.position, out unit);

				Vector3 unitTerrainPos = unit.origPosition;
				unitTerrainPos.y -= gv.unitHeightModifier;

				List<Vector3> possibleMovement = unit.movement.findPossibleMovement (unitTerrainPos);
				List<IObject> movementObjects = gv.battlefield.convertListPosToListObject (possibleMovement);

				//remove Old movement highlighting
				gv.battlefield.removeListHighlights (highlightedTerrain, "MovementHighlighting");
				gv.battlefield.addHighlights (movementObjects, "MovementHighlighting");
				//update what terrain is highlighted
				highlightedTerrain = movementObjects;


			}
			return this;
		} else if(hit.collider.tag == "Terrain") {
			gv.log ("SelectUnitState: Terrain Clicked -> MoveUniteState");
			GameObject hitTerrain = hit.collider.gameObject;

			IObject terrainCode; 
			gv.battlefield.terrainDictionary.TryGetValue (hitTerrain.transform.position, out terrainCode);


			if (terrainCode != null && highlightedTerrain.Contains (terrainCode)) {
				if (unit != null) {
					gv.battlefield.moveUnit (unit, terrainCode.position);
				}
			}
			
		}//Add else ifs as needed for each tag you are looking for

		return missedClickAction ();
	}

	private void removeHighlights(IObject codeObject){
		if (highlightedUnit == null) {
			return;
		}

		gv.battlefield.changeHighlight (codeObject, "BasicHighlighting", false);
		gv.battlefield.changeHighlight (codeObject, "InvalidHighlighting", false);
		highlightedUnit = null;

	}

	public override IPlayerState missedClickAction(){
		gv = GlobalVariables.getInstance ();
		gv.log ("MoveUnitState: Missed Click -> SelectUnitState");

		removeHighlights (highlightedUnit);
		gv.battlefield.removeListHighlights (highlightedTerrain, "MovementHighlighting");

		return SelectUnitState.CreateInstance<SelectUnitState>();
	}
}
