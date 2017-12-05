using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager: MonoBehaviour
{
	IPlayerState playerState;



	public void Start(){

		playerState = MoveUnitState.CreateInstance<MoveUnitState>();
	}

	public void Update(){
		playerState.clickAction ();
	}
}

