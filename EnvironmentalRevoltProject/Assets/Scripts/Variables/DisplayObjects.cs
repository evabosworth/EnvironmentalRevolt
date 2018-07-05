using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayObjects: MonoBehaviour
{

	//Probably best to keep things alphabetical here...
	public GameObject basicTerrainDisplayObject;
	public List<GameObject> displayUnitList;









	private static DisplayObjects displayObjects = null;
	private DisplayObjects(){
		displayObjects = this;
	}

	public static DisplayObjects getInstance(){
		if (displayObjects == null) {
			displayObjects = new DisplayObjects ();
		}
		return displayObjects;
	}

}


