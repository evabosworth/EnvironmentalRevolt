using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState {

	/**
	 * clickAction should hold code for what the player's clicks can do 
	 */
	IPlayerState clickAction(RaycastHit hit);

	/**
	 * missedClickAction is called when a click occurs and doesnt hit anything.
	 */
	IPlayerState missedClickAction();
}
