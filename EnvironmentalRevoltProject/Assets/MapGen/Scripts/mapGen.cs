using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGen : MonoBehaviour {
	public GameObject terrain;
	private int xSize = 30;
	private int zSize = 30;

	// Use this for initialization
	void Start () {
		int y = 0;
		for (int x = 0; x < xSize; x++) {
			for (int z = 0; z < zSize; z++) {
				createTerrain (terrain, new Vector3 (x, y, z), x, z);
				
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void createTerrain(GameObject terrain, Vector3 position, int x, int z){
		GameObject terrainClone = (GameObject)Instantiate (terrain, position, transform.rotation);
		terrainClone.name = "terrain:x," + x + ":z," + z; 
	}
}
