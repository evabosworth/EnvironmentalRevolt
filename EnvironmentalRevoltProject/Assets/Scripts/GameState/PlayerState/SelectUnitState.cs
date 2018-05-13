using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitState : IPlayerState{

	// Use this for initialization
	GameObjectController objectController = null;
	private GlobalVariables gv;
	public static GameObject highlightObject = null;


	public override IPlayerState clickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		// whatever tag you are looking for on your game object
		if (hit.collider.tag == "Character") {
			objectController = FindObjectOfType<GameObjectController>();
			gv.log ("SelectUnitState: Character Clicked -> MoveUniteState");
			GameObject hitObject = hit.collider.gameObject;

			if (!hitObject.Equals (highlightObject)) {
				removeHighlights(highlightObject);
			}


			objectController.changeHighlighting (hitObject, "BasicHighlighting", true);
			highlightObject = hitObject;

			MoveUnitState moveUnitState = MoveUnitState.CreateInstance<MoveUnitState>();

			return moveUnitState.clickAction(hit);
		} //Add else ifs as needed for each tag you are looking for

		return missedClickAction ();
	}

	private void removeHighlights(GameObject gameObject){
		if (highlightObject == null) {
			return;
		}

		objectController.changeHighlighting (gameObject, "BasicHighlighting", false);
		objectController.changeHighlighting (gameObject, "InvalidHighlighting", false);
		highlightObject = null;

	}
}

