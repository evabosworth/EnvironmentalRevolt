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
			gv.log ("Character Clicked");
			GameObject character = hit.collider.gameObject;
			//On select change color

			MeshRenderer meshRend = character.GetComponent<MeshRenderer> ();

			Material mat = meshRend.material;


			gv.log(mat.color.ToString());

			return MoveUnitState.CreateInstance<MoveUnitState>();
		} //Add else ifs as needed for each tag you are looking for

		return missedClickAction ();
	}

	public IPlayerState missedClickAction(){
		return this;
	}
}

