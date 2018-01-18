using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitState : ScriptableObject, IPlayerState  {

	public GameObject lastChosen;
	public FindPossibleMovements lastMove;
	public FindPossibleMovements move;
	public List<Node> possibleMovements;
	// Use this for initialization
	private GlobalVariables gv;

	public IPlayerState clickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		// whatever tag you are looking for on your game object
		if (hit.collider.tag == "Character") {
			gv.log ("Character Clicked");

			/*
			possibleMovements = new List<Node>();
			lastMove = lastChosen.GetComponent<FindPossibleMovements>();
			lastMove.ableToMove = false; 
			move = hit.collider.gameObject.GetComponent<FindPossibleMovements>();
			move.ableToMove = true;
			lastChosen = hit.rigidbody.gameObject;
			possibleMovements = move.FindMovements(new Vector3(hit.transform.position.x, hit.transform.position.y-.75f, hit.transform.position.z));
			*/
		} //Add else ifs as needed for each tag you are looking for

		return missedClickAction ();
	}

	public IPlayerState missedClickAction(){
		return SelectUnitState.CreateInstance<SelectUnitState>();
	}
}
