using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IObject: ScriptableObject {
	public GameObject gameObject;
	public string displayName;
	public string uniqueName;
	public IMovement movement;
	public Vector3 position;
	public Vector3 origPosition; //For when moved, but not a new turn.
	public List<string> currentHighlights = new List<string>();
	public GlobalVariables gv;

	public IObject(){
		gv = GlobalVariables.getInstance ();
	}



	public IObject (GameObject gameObject, string displayName, string UniqueName, Vector3 position){
		gv = GlobalVariables.getInstance ();

		this.gameObject = gameObject;
		this.displayName = displayName;
		this.uniqueName = UniqueName;
		this.position = position;
		this.origPosition = position;
	}

	public void addHightlight(string highlightName){
		currentHighlights.Add (highlightName);
	}

	public void removeHighlight(string highlightName){
		currentHighlights.Remove (highlightName);
	}

	public void removeAllHightlights(){

	}
}

