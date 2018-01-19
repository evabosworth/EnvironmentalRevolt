using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitState : ScriptableObject,IPlayerState{

	// Use this for initialization
	private GlobalVariables gv;


	public IPlayerState clickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		// whatever tag you are looking for on your game object
		if (hit.collider.tag == "Character") {
			gv.log ("SelectUnitState: Character Clicked");
			GameObject character = hit.collider.gameObject;
			//On select change color

			MeshRenderer meshRend = character.GetComponent<MeshRenderer> ();
			Material mat = meshRend.material;

			Color newColor = new Color (1.159f, 0.0f, 1.0f, 1.0f);
			gv.log(mat.color.ToString());
			mat.color = newColor;

			MoveUnitState moveUnitState = MoveUnitState.CreateInstance<MoveUnitState>();
			moveUnitState.setSelectedUnit (character);

			return moveUnitState;
		} //Add else ifs as needed for each tag you are looking for

		return missedClickAction ();
	}

	public IPlayerState missedClickAction(){
		gv.log ("SelectUnitState: Missed Click");

		return this;
	}
}

