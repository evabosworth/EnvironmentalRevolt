using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPlayerState: ScriptableObject {

	/**
	 * clickAction should hold code for what the player's clicks can do 
	 */
	public virtual IPlayerState clickAction(RaycastHit hit){
		return this;
	}

	/**
	 * missedClickAction is called when a click occurs and doesnt hit anything.
	 */
	public virtual IPlayerState missedClickAction(){
		return this;
	}
}
