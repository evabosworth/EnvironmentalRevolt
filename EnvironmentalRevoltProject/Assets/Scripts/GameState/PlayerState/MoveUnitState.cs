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
	private GameObject selectedUnit;


	public IPlayerState clickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();

		// whatever tag you are looking for on your game object
		if (hit.collider.tag == "Character") {
			gv.log ("MoveUnitState: Clicked character");
			
		} //Add else ifs as needed for each tag you are looking for

		return missedClickAction ();
	}

	public void setSelectedUnit(GameObject selectedUnit){
		this.selectedUnit = selectedUnit;
	}

	public IPlayerState missedClickAction(){
		gv = GlobalVariables.getInstance ();
		gv.log ("MoveUnitState: Missed Click");

		MeshRenderer meshRend = selectedUnit.GetComponent<MeshRenderer> ();
		Material mat = meshRend.material;
		Color oldColor = new Color (0.159f, 0.0f, 1.0f, 1.0f);
		mat.color = oldColor;
		return SelectUnitState.CreateInstance<SelectUnitState>();
	}
}
