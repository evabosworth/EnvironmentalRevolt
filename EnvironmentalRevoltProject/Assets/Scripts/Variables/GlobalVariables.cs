using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables: MonoBehaviour
{
	private static GlobalVariables gv = null;

	public bool DEBUG = true;

	public Battlefield battlefield;

	private GlobalVariables(){
		gv = this;
	}


	public float unitHeightModifier = 0.75f;
	public float basicTerrainHeight = 0.50f;
	public float mapXSize = 20;
	public float mapZSize = 20;

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
		if (gv == null) {
			gv = new GlobalVariables ();
		}
		return gv;
	}

}


