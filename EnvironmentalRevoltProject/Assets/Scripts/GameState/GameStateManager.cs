using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *	GameStateManager is the basic brains behind the players actions.  
 */
public class GameStateManager: MonoBehaviour
{
	IPlayerState playerState;
	GameObject lastChosen;
	FindPossibleMovements lastMove;
	FindPossibleMovements move;
	List<Node> possibleMovements;
	// Use this for initialization
	GlobalVariables gv;



	public void Start(){
		gv = FindObjectOfType<GlobalVariables> ();

		playerState = CreateUnitState.CreateInstance<CreateUnitState>();

	}

	public void Update(){
		playerState.passiveAction ();

		//Always listen for a click, and respond according to current state.
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100)) {
			playerState = playerState.mouseOver (hit);
		} else {
			playerState = playerState.missedMouseOverAction ();
		}

		//TODO: make all buttons user custimizable.
		//If there was a left mouse click
		if (Input.GetMouseButtonDown(0))
		{

			if (Physics.Raycast (ray, out hit, 100)) {
				playerState = playerState.clickAction (hit);
			} else { //clicked in a place where nothing exists. What do?
				playerState = playerState.missedClickAction();
			}
		} 
		//close left mouse click
		//default, enter button start
		else if(Input.GetAxis("Submit") > 0){
			playerState = playerState.manuallyAdvanceStage ();
		
		}

	}

	public void ManuallyAdvance(){
		gv.log ("Advance Stage");
		playerState = playerState.manuallyAdvanceStage ();
	}
}

