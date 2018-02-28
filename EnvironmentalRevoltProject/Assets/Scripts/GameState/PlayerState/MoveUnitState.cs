using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitState : IPlayerState  {

	public GameObject lastChosen;
	public FindPossibleMovements lastMove;
	public FindPossibleMovements move;
	public List<Node> possibleMovements;
	// Use this for initialization
	private GlobalVariables gv;
	private GameObject selectedObject = null;


	public override IPlayerState clickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();

		Vector3 unitPos = selectedObject.transform.position;

		// whatever tag you are looking for on your game object
		if (hit.collider.tag == "Character") {
			gv.log ("MoveUnitState: Clicked character -> MoveUnitState");

			GameObject character = hit.collider.gameObject;
			//On select change color

			MeshRenderer meshRend = character.GetComponent<MeshRenderer> ();
			Material mat = meshRend.material;

			Color newColor = new Color (1.159f, 0.0f, 1.0f, 1.0f);
			gv.log(mat.color.ToString());
			mat.color = newColor;

			MoveUnitState moveUnitState = MoveUnitState.CreateInstance<MoveUnitState>();

			if (selectedObject == null || selectedObject.transform.name != character.transform.name) {
				deselectUnit ();
				moveUnitState.setSelectedUnit (character);
				gv.log ("MoveUnitState: Clicked new character -> MoveUnitState");
			}

			return this;
		} else if(hit.collider.tag == "Terrain"){
			gv.log ("MoveUnitState: Terrain -> MoveUnitState");

			Vector3 playPos = selectedObject.transform.position;

			GameObject terrain = hit.collider.gameObject;

			gv.log (terrain.transform.position.ToString ());



			return this;
		}//Add else ifs as needed for each tag you are looking for

		return missedClickAction ();
	}

	public void setSelectedUnit(GameObject selectedUnit){
		this.selectedObject = selectedUnit;
	}

	private void deselectUnit(){
		if (selectedObject != null) {
			MeshRenderer meshRend = selectedObject.GetComponent<MeshRenderer> ();
			Material mat = meshRend.material;
			Color oldColor = new Color (0.159f, 0.0f, 1.0f, 1.0f);
			mat.color = oldColor;

		}


		selectedObject = null;
	}

	public override IPlayerState missedClickAction(){
		gv = GlobalVariables.getInstance ();
		gv.log ("MoveUnitState: Missed Click -> SelectUnitState");

		deselectUnit ();

		return SelectUnitState.CreateInstance<SelectUnitState>();
	}
}
