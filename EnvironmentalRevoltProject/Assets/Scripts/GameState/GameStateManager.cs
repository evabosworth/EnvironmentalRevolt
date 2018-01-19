using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *	GameStateManager is the basic brains behind the players actions.  
 */
public class GameStateManager: MonoBehaviour
{
	IPlayerState playerState;
	public GameObject lastChosen;
	public FindPossibleMovements lastMove;
	public FindPossibleMovements move;
	public List<Node> possibleMovements;
	// Use this for initialization
	public GlobalVariables gv;



	public void Start(){

		playerState = MoveUnitState.CreateInstance<SelectUnitState>();
		gv = FindObjectOfType<GlobalVariables> ();
	}

	public void Update(){
		//Always listen for a click, and respond according to current state.


		//If there was a left mouse click
		if (Input.GetMouseButtonDown(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				playerState = playerState.clickAction (hit);
			} else { //clicked in a place where nothing exists. What do?
				playerState = playerState.missedClickAction();
			}
		}
		//close left mouse click
	}
}

