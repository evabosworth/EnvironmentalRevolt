using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPlayerState: ScriptableObject {
	public IPlayerState previousState = null;

	public virtual IPlayerState passiveAction(){
		return this;
	}

	public virtual IPlayerState passAction(IObject obj){
		return this;
	}

	/**
	 * clickAction should hold code for what the player's clicks can do 
	 */
	public virtual IPlayerState clickAction(RaycastHit hit){
		return this;
	}

	/**
	 * 
	 */
	public virtual IPlayerState secondaryClickAction(RaycastHit hit){
		return this;
	}

	/**
	 * missedClickAction is called when a click occurs and doesnt hit anything.
	 */
	public virtual IPlayerState missedClickAction(){
		return this;
	}

	/**
	 * manuallyAdvanceStage function that advances to a set next stage on default enter key;
	 */
	public virtual IPlayerState manuallyAdvanceStage(){
		return this;
	}

	/**
	 * MouseOver function that takes in any hits of raycasts 
	 */
	public virtual IPlayerState mouseOver(RaycastHit hit){
		return this;
	}

	/**
	 * missedMouseOverAction for when the moiuse over hits nothing
	 */
	public virtual IPlayerState missedMouseOverAction(){
		return this;
	}

	public virtual IPlayerState horizontalAction(int direction){
		return this;
	}
}
