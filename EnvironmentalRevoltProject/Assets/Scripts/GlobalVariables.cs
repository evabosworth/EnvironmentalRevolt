using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables: MonoBehaviour
{
	private static GlobalVariables gv = null;

	public bool DEBUG = true;
	public Dictionary<Vector3, GameObject> terrainDictionary;

	public void Start(){
		gv = this;
	}

	/* 
	 * Global helper function to convert a vector 3 into a Node;
	 */
	public static Node convertVector3ToNode(Vector3 vector){
		Node node = new Node((int)vector.x, (int)vector.y,(int)vector.z);

		return node;
	}

	public void log(string message){
		if (DEBUG) {
			Debug.Log (message);
		}
	}

	public static GlobalVariables getInstance(){
		return gv;
	}
}


