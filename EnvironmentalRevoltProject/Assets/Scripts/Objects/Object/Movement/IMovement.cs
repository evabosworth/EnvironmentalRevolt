using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMovement
{ 
	void init (int distance, int jumpHeightUnits);
	List<Vector3> findPossibleMovement (Vector3 startPos);


}

