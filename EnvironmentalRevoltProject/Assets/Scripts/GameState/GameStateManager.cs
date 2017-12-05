using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager: MonoBehaviour
{
	IPlayerState playerState;
    int playerStateNum;
    //0 for createUnitState
    //1 for MoveUnitStae
    public GameObject unit;

	public void Start(){

        playerState = CreateUnitState.CreateInstance<CreateUnitState>();
        playerStateNum = 0;
	}

	public void Update(){
        if (playerStateNum == 0)
        {
            playerState.setUnit(unit);
        }
		playerState.clickAction ();
	}
}

