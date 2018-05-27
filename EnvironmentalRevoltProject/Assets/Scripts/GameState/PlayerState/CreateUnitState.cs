using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnitState : IPlayerState
{
	private GlobalVariables gv;
	private DisplayObjects displayObjects;
	public static int counter = 0;
	public static IObject highlightObject = null;


	public override IPlayerState mouseOver(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		displayObjects = DisplayObjects.getInstance ();

		GameObject hitObject = (GameObject)hit.collider.gameObject;
		gv.log ("CreateUnitState: mouseOver -> CreateUnitState");

		IObject codeHitObject = gv.battlefield.searchBattlefield (hitObject);


		if (hit.collider.tag == "Terrain") {
			if (!codeHitObject.Equals (highlightObject)) {//If the game object is new

				removeHighlights(highlightObject); //old highlight no longer needs to be highlighted

				highlightObject = codeHitObject;


				if(gv.battlefield.isValidTerrainForUnitPlacement(highlightObject.position)){ //if valid
					gv.battlefield.changeHighlight (highlightObject, "BasicHighlighting", true); //change highlight
				}


			} 
		} else if(hit.collider.tag == "Character") {
			if (!codeHitObject.Equals (highlightObject)) {//If the game object is new

				removeHighlights(highlightObject);
				gv.battlefield.changeHighlight (codeHitObject, "InvalidHighlighting", true);

				highlightObject = codeHitObject;
			}
		} else {
			removeHighlights(highlightObject);
		}


		return this;
	}

	public override IPlayerState missedMouseOverAction(){
		gv = GlobalVariables.getInstance ();

		removeHighlights (highlightObject);
		return this;
	}

	private void removeHighlights(IObject codeObj){
		gv = GlobalVariables.getInstance ();
		if (codeObj == null) {
			return;
		}

		gv.battlefield.changeHighlight (codeObj, "BasicHighlighting", false);
		gv.battlefield.changeHighlight (codeObj, "InvalidHighlighting", false);
		highlightObject = null;

	}

	//TODO: what is displayed on click is changed by user action!!
	public override IPlayerState clickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		GameObject hitObject = hit.collider.gameObject;
		IObject codeHitObject = gv.battlefield.searchBattlefield (hitObject);

		gv.log ("CreateUnitState: Clicked -> CreateUnitState");

		if(!gv.battlefield.isValidTerrainForUnitPlacement(codeHitObject.position)){
			return this;
		}


		if (hit.collider.tag == "Terrain")
		{
			codeHitObject.position.y += gv.unitHeightModifier;


			IObject unit = new Obstacle (displayObjects.basicUnitDisplayObject, "sphere" + counter, "SphereTest"  + counter, codeHitObject.position);

			bool isUnitAdded= gv.battlefield.addObjectToBattlefield (unit);
			if (isUnitAdded) {
				counter++;
			}

			removeHighlights(highlightObject);
			removeHighlights(unit);

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

		removeHighlights(highlightObject);
		return SelectUnitState.CreateInstance<SelectUnitState>();
	}
}

