using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables: MonoBehaviour
{
	public bool DEBUG = false;

	/* 
	 * Global helper function to convert a vector 3 into a Node;
	 */
	public static Node convertVector3ToNode(Vector3 vector){
		Node node = new Node((int)vector.x, (int)vector.y,(int)vector.z);

		return node;
	}

}


