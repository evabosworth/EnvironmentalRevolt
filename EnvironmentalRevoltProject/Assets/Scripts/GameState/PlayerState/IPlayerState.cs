using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState {

	/**
	 * clickAction should hold code for what the player's clicks can do 
	 */
	void clickAction();
    void setUnit(GameObject unit);

}
