using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitState : IPlayerState{

	// Use this for initialization
	private GlobalVariables gv;
	public static IObject highlightObject = null;


	public override IPlayerState clickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		// whatever tag you are looking for on your game object
		if (hit.collider.tag == "Character") {
			gv.log ("SelectUnitState: Character Clicked -> MoveUniteState");
			GameObject hitObject = hit.collider.gameObject;
			IObject codeHitObject = gv.battlefield.searchBattlefield (hitObject);

			if (!hitObject.Equals (highlightObject)) {
				removeHighlights(highlightObject);
			}


			gv.battlefield.changeHighlight (codeHitObject, "BasicHighlighting", true);
			highlightObject = codeHitObject;

			MoveUnitState moveUnitState = MoveUnitState.CreateInstance<MoveUnitState>();

			return moveUnitState.clickAction(hit);
		} //Add else ifs as needed for each tag you are looking for

		return missedClickAction ();
	}

	private void removeHighlights(IObject highlightObject){
		if (highlightObject == null) {
			return;
		}

		gv.battlefield.changeHighlight (highlightObject, "BasicHighlighting", false);
		gv.battlefield.changeHighlight (highlightObject, "InvalidHighlighting", false);
		highlightObject = null;

	}
}

