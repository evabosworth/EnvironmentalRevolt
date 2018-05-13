using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnitState : IPlayerState
{
	GameObjectController objectController;
	private GlobalVariables gv;
	private DisplayObjects displayObjects;
	public static int counter = 0;
	public static GameObject highlightObject = null;


	public override IPlayerState mouseOver(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		displayObjects = DisplayObjects.getInstance ();

		GameObject hitObject = (GameObject)hit.collider.gameObject;
		gv.log ("CreateUnitState: mouseOver -> CreateUnitState");

		objectController = FindObjectOfType<GameObjectController>();

		if (hit.collider.tag == "Terrain") {
			if (!hitObject.Equals (highlightObject)) {//If the game object is new

				removeHighlights(highlightObject);

				if(gv.battlefield.isValidTerrainForUnitPlacement(hitObject.transform.position)){
					objectController.changeHighlighting (hitObject, "BasicHighlighting", true);
				}


				highlightObject = hitObject;
			} 
		} else if(hit.collider.tag == "Character") {
			if (!hitObject.Equals (highlightObject)) {//If the game object is new

				removeHighlights(highlightObject);
				objectController.changeHighlighting (hitObject, "InvalidHighlighting", true);

				highlightObject = hitObject;
			}
		} else {
			removeHighlights(highlightObject);
		}


		return this;
	}

	public override IPlayerState missedMouseOverAction(){
		removeHighlights (highlightObject);
		return this;
	}

	private void removeHighlights(GameObject gameObject){
		if (highlightObject == null) {
			return;
		}

		objectController.changeHighlighting (gameObject, "BasicHighlighting", false);
		objectController.changeHighlighting (gameObject, "InvalidHighlighting", false);
		highlightObject = null;

	}

	//TODO: what is displayed on click is changed by user action!!
	public override IPlayerState clickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		GameObject hitObject = hit.collider.gameObject;

		Dictionary<string, string> test = new Dictionary<string, string> ();
		test.Add ("test", "test");
		string testing = test ["test"];


		gv.log ("CreateUnitState: Clicked -> CreateUnitState");

		objectController = FindObjectOfType<GameObjectController>();
		if(!gv.battlefield.isValidTerrainForUnitPlacement(hitObject.transform.position)){
			return this;
		}


		if (hit.collider.tag == "Terrain")
		{
			Vector3 unitPosition = hitObject.transform.position;

			unitPosition.y += gv.unitHeightModifier;


			IObject unit = new Obstacle (displayObjects.basicUnitDisplayObject, "sphere" + counter, "SphereTest"  + counter, unitPosition);

			bool isUnitAdded= gv.battlefield.addUnitToBattlefield (unit);
			if (isUnitAdded) {
				objectController.createAndDisplayGameObject (unit);
				counter++;
			}

			removeHighlights(highlightObject);

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

