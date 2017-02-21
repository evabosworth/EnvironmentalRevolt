using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGen : MonoBehaviour {

	//Starting with plains with light hills, so few particles, low max height
	public GameObject terrain;
	public int[,] terrainGrid;


	public int xSize = 50;
	public int zSize = 50;
	public int maxHeight = 5;
	//0 or 1 creates hills, 3,4 creates canyons
	public int redistributeThreshold = 1; //height can only change on level at a time, not sure what this does anymore
	public int stepSize = 1;
	public int passes = 5;
	public int neighborPasses = 3;
	public int numParticleStarts = 20;
	public int numParticleSteps = 250;

	// Use this for initialization
	void Start () {
		//int seed = 12;
		//Random.InitState (seed);

		float startTime = Time.realtimeSinceStartup;
		print ("start: " + startTime);
		print ("start: 0");

		terrainGrid = new int[xSize, zSize];

		int starts = numParticleStarts;
		for (int start = 0; start < starts; start++) {
			int particleChange = 1;
			int steps = numParticleSteps;
			//If all positive have been done, do negative

			particleDeposition (steps, particleChange);
		}

		float preAlgorithm = Time.realtimeSinceStartup;
		print ("pre-algorithm: " + (preAlgorithm - startTime));
		flattenTerrain ();

		float postAlgorithm = Time.realtimeSinceStartup;
		print ("post-algorithm, pre-visual: " + (postAlgorithm- preAlgorithm));
		StartCoroutine ("createTerrainGrid");
		float postTerrain = Time.realtimeSinceStartup;
		print ("post-Terrain" + (postTerrain - postAlgorithm));

		float total = Time.realtimeSinceStartup - startTime;
		print ("Total: " + total);  
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void flattenTerrain(){
		redistributeTerrain ();
		for (int pass = 0; pass < neighborPasses; pass++) {
			for (int x = 0; x < xSize; x++) {
				for (int z = 0; z < zSize; z++) {
					/*
					bool inboundx1 = false;
					bool inboundxNeg1 = false;
					bool inboundz1 = false;
					bool inboundzNeg1 = false;
					*/
					int numer = 0;
					int denom = 0;
					int valx1 = 0;
					int valxNeg1 = 0;
					int valz1 = 0;
					int valzNeg1 = 0;
					if (x < xSize - 1) {
						valx1 = terrainGrid [x + 1, z];
						denom++;
						numer += valx1;
						//inboundx1 = true;
					}
					if (x > 0) {
						valxNeg1 = terrainGrid [x - 1, z];
						denom++;
						numer += valxNeg1;
						//inboundxNeg1 = true;
					}
					if (z < zSize - 1) {
						valz1 = terrainGrid [x, z + 1];
						denom++;
						numer += valz1;
						//inboundz1 = true;
					}
					if (z > 0) {
						valzNeg1 = terrainGrid [x, z - 1];
						denom++;
						numer += valzNeg1;
						//inboundzNeg1 = true;
					}

					int height =(int) (numer / denom + Random.value);
					terrainGrid [x, z] = height;

				}
			}
		}

	}

	/*
	 * Using max height, flatten the terrain a number of passes
	 * the more passes the flatter the terrain
	 * uses, maxHeight, Passes
	**/
	private void redistributeTerrain(){
		for (int pass = 0; pass < passes; pass++) {
			for (int x = 0; x < xSize; x++) {
				for (int z = 0; z < zSize; z++) {
					int valx1 = int.MinValue;
					int valxNeg1 = int.MinValue;
					int valz1 = int.MinValue;
					int valzNeg1 = int.MinValue;
					int curVal = terrainGrid [x, z];
					if (x < xSize - 1) {
						valx1 = terrainGrid [x + 1, z];
					}
					if (x > 0) {
						valxNeg1 = terrainGrid [x - 1, z];
					}
					if (z < zSize - 1) {
						valz1 = terrainGrid [x, z + 1];
					}
					if (z > 0) {
						valzNeg1 = terrainGrid [x, z - 1];
					}

					if (terrainGrid [x, z] > 0){ //maxHeight) {
						/* first try, using particle split instead of redistribute*/
						if (valx1 != int.MinValue && valx1 < maxHeight && valx1 + redistributeThreshold > curVal) {
							terrainGrid [x + 1, z] += 1;
						}if (valxNeg1 != int.MinValue && valxNeg1 < maxHeight && valxNeg1 + redistributeThreshold > curVal) {
							terrainGrid [x - 1, z] += 1;
						}if (valz1 != int.MinValue && valz1 < maxHeight && valz1 + redistributeThreshold > curVal) {
							terrainGrid [x, z + 1] += 1;
						}if (valzNeg1 != int.MinValue && valzNeg1 < maxHeight && valzNeg1 + redistributeThreshold > curVal) {
							terrainGrid [x, z - 1] += 1;
						}


						//First try step after leveling check
						if (terrainGrid [x, z] > maxHeight) {
							terrainGrid [x, z] -= 1;
						}
					} 

					if (pass == passes - 1) {
						if (terrainGrid [x, z] > maxHeight) {
							terrainGrid [x, z] = maxHeight;
						}
					}

				}
			}
		}
	}

	/*
	 * takes in the curstep, and the charge of the particle
	 * returns void
	 * side effect, alters TerrainGrid with different sizes
	 */
	private void particleDeposition(int steps,  int particleChange){
		int xPos = (int)(Random.value * xSize);
		int zPos = (int)(Random.value * zSize);

		for (int step = 0; step < steps; step++) {
			//int curValue = terrainGrid [xPos, zPos];
			if (particleChange > 0) {
				terrainGrid [xPos, zPos] += particleChange*stepSize;
			} else {
				terrainGrid [xPos, zPos] += particleChange*stepSize;
			}

			int rng = (int)(Random.value *4);
			switch(rng) {
			case 0: xPos++;break; // move right
			case 1: xPos--;break; // move left
			case 2: zPos++;break; // move closer
			case 3: zPos--;break; // move away
			}
			if (xPos < 0) {
				xPos = 0;
			}else if (xPos >= xSize) {
				xPos = xSize-1;
			}
			if (zPos < 0) {
				zPos = 0;
			}else if (zPos >= zSize) {
				zPos = zSize-1;
			}
		}
	}

	/*
	 * Takes in a terrain block (lava, grass, normal, ...) a position and location (for naming) [probably don't need]
	 * returns void, 
	 * side effect visually creates a terrain block at position
	**/
	private void createTerrain(GameObject terrain, Vector3 position){
		int x = (int)position.x;
		int y = (int)position.y;
		int z = (int)position.z;
		GameObject terrainClone = (GameObject)Instantiate (terrain, position, transform.rotation);
		terrainClone.name = "terrain:x" + x + ":y" + y + ":z" + z;
	}

	/*
	 * side effect visualizes the entire terrainGrid after particle depistion and smoothing
	**/
	public IEnumerator createTerrainGrid(){
		int curNum = 0;
		float y = 0;
		for (int x = 0; x < xSize; x++) {
			for (int z = 0; z < zSize; z++) {
				y = terrainGrid [x, z]*0.5f;
				createTerrain (terrain, new Vector3 (x, y, z));
				//Check surround for heights, fill in below

				int curVal = terrainGrid [x, z];
				int lowest = curVal;
				if (x < xSize - 1) {
					int temp = terrainGrid [x + 1, z];
					if (temp < lowest) {lowest = temp;}
				} else {lowest = 0;}

				if (x > 0) {
					int temp = terrainGrid [x - 1, z];
					if (temp < lowest) {lowest = temp;}
				} else {lowest = 0;}

				if (z < zSize - 1) {
					int temp = terrainGrid [x, z + 1];
					if (temp < lowest) {lowest = temp;}
				} else {lowest = 0;}

				if (z > 0) {
					int temp = terrainGrid [x, z - 1];
					if (temp < lowest) {lowest = temp;}
				} else {lowest = 0;}


				for (int fillPos = lowest; fillPos < curVal; fillPos++) {
					y = fillPos*0.5f;
					createTerrain (terrain, new Vector3 (x, y, z));
				}

				if(curNum >= xSize){
					yield return new WaitForSeconds(.01f);
					curNum = 0;
				}
				curNum++;
			}
		}
	}
}
