using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnitState : IPlayerState
{
	private GlobalVariables gv;
	private DisplayObjects displayObjects;
	public static int counter = 0;
	public static IObject highlightObject = null;

	public override IPlayerState passiveAction(){
		gv = GlobalVariables.getInstance ();


		return this;
	}


	public override IPlayerState mouseOver(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		displayObjects = DisplayObjects.getInstance ();

		GameObject hitObject = (GameObject)hit.collider.gameObject;
		gv.log ("CreateUnitState: mouseOver -> CreateUnitState");

		IObject codeHitObject = gv.battlefield.searchBattlefield (hitObject);
		if (codeHitObject == null) {
			return this;
		}


		if (hit.collider.tag == "Terrain") {
			if (!codeHitObject.Equals (highlightObject)) {//If the game object is new

				gv.battlefield.removeHighlights(highlightObject); //old highlight no longer needs to be highlighted
				highlightObject = codeHitObject;


				if (gv.battlefield.isValidTerrainForInitialUnitPlacement (highlightObject.position)) { //if valid
					gv.battlefield.changeHighlight (highlightObject, "BasicHighlighting", true); //change highlight
				} else {
					gv.log ("overall invalid Place");
				}


			} 
		} else if(hit.collider.tag == "Character") {
			if (!codeHitObject.Equals (highlightObject)) {//If the game object is new
				if (highlightObject != null) {
					gv.battlefield.removeHighlights (highlightObject);
				}
				gv.battlefield.changeHighlight (codeHitObject, "InvalidHighlighting", true); //change highlight
				highlightObject = codeHitObject;
			}
		} else {
			gv.battlefield.removeHighlights(highlightObject);
			highlightObject = null;
		}


		return this;
	}

	public override IPlayerState secondaryClickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		displayObjects = DisplayObjects.getInstance ();

		GameObject hitObject = (GameObject)hit.collider.gameObject;
		gv.log ("CreateUnitState: secondaryHit -> CreateUnitState");

		IObject codeHitObject = gv.battlefield.searchBattlefield (hitObject);


		if (hit.collider.tag == "Terrain") {
			return missedClickAction ();
		} else if (hit.collider.tag == "Character") {
			gv.battlefield.removeHighlights(highlightObject);
			highlightObject = null;
			gv.battlefield.removeUnit (codeHitObject);
			gv.log ("CreateUnitState: Unit removed");
		}

		return this;
	}

	public override IPlayerState missedMouseOverAction(){
		gv = GlobalVariables.getInstance ();

		gv.battlefield.removeHighlights(highlightObject); 
		highlightObject = null;
		return this;
	}

	//TODO: what is displayed on click is changed by user action!!
	public override IPlayerState clickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		GameObject hitObject = hit.collider.gameObject;
		IObject codeHitObject = gv.battlefield.searchBattlefield (hitObject);

		gv.log ("CreateUnitState: Clicked -> CreateUnitState");

		if(!gv.battlefield.isValidTerrainForInitialUnitPlacement(codeHitObject.position)){
			return this;
		}


		if (hit.collider.tag == "Terrain")
		{
			Vector3 unitPos = codeHitObject.position;
			unitPos.y += gv.unitHeightModifier;

			//TODO: add restrictions based on number of already placed and such
			//		also have a unit stored in the state that is the active unit right now

			IObject unit = new Obstacle (displayObjects.basicUnitDisplayObject, "sphere" + counter, "SphereTest"  + counter, unitPos);

			bool isUnitAdded= gv.battlefield.addObjectToBattlefield (unit);
			if (isUnitAdded) {
				counter++;
			}
			gv.battlefield.removeHighlights(highlightObject); 
			highlightObject = null;



		}
		if (hit.collider.tag == "Obstacle")
		{
			/*
			IObject unit = Obstacle.CreateInstance<Obstacle>();
			unit.updateInfo("sphere", "SphereTest", hitObject.transform.position);


			objectGenerator.createAndDisplayGameObject(unit);
			*/
		}

		return this;
	}


	public override IPlayerState manuallyAdvanceStage(){
		gv = GlobalVariables.getInstance ();
		gv.log ("CreateUnitState: Advance Stage -> SelectUnitState");

		gv.battlefield.removeHighlights(highlightObject);
		highlightObject = null;
		return SelectUnitState.CreateInstance<SelectUnitState>();
	}
}

