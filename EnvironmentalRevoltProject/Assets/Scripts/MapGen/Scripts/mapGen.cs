using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The first working version of map gen
 */
public class mapGen : ScriptableObject, IMap {

	//Starting with plains with light hills, so few particles, low max height

	public int[,] terrainGrid;

	public int xSize;
	public int zSize;
	public int maxHeight;
	//0 or 1 creates hills, 3,4 creates canyons
	public int redistributeThreshold; //height can only change on level at a time, not sure what this does anymore
	public int stepSize;
	public int passes;
	public int neighborPasses;
	public int numParticleStarts;
	public int numParticleSteps;

	public List<Vector3> runCreateTerrain(int xSize, int zSize, int maxHeight, int redistributeThreshold, int stepSize, int passes, int numParticleStarts, int numParticleSteps){
		this.xSize = xSize;
		this.zSize = zSize;
		this.maxHeight = maxHeight;
		this.redistributeThreshold = redistributeThreshold;
		this.stepSize = stepSize;
		this.passes = passes;
		this.numParticleStarts = numParticleStarts;
		this.numParticleSteps = numParticleSteps;


		//terrain = new GameObject ();
		//int seed = 12;
		//Random.InitState (seed);

		float startTime = Time.realtimeSinceStartup;

		terrainGrid = new int[xSize, zSize];
			
		int starts = numParticleStarts;
		for (int start = 0; start < starts; start++) {
			int particleChange = 1;
			int steps = numParticleSteps;
			//If all positive have been done, do negative

			particleDeposition (steps, particleChange);
		}

		flattenTerrain ();

		List<Vector3> returnGrid = createTerrainGrid (terrainGrid);

		return returnGrid;
		//TODO Terrain layout has to be easily accesible,
		//currently held in terrainGrid
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
		bool insideStepSize = false;
		while (!insideStepSize) { 
			insideStepSize = true;
			for (int x = 0; x < xSize; x++) {
				bool validTerrain = false;
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

					if (terrainGrid [x, z] > 0) { //maxHeight) {
						/* first try, using particle split instead of redistribute*/
						if (valx1 != int.MinValue && valx1 < maxHeight && valx1 + redistributeThreshold > curVal) {
							terrainGrid [x + 1, z] += 1;
							//terrainGrid [x, z]--;

							if(terrainGrid[x,z] <= terrainGrid [x + 1, z] + 2 && terrainGrid[x,z] >= terrainGrid [x + 1, z] - 2){
								validTerrain = true;
							}
						}
						if (valxNeg1 != int.MinValue && valxNeg1 < maxHeight && valxNeg1 + redistributeThreshold > curVal) {
							terrainGrid [x - 1, z] += 1;
							//terrainGrid [x, z]--;

							if(terrainGrid[x,z] <= terrainGrid [x - 1, z] + 2 && terrainGrid[x,z] >= terrainGrid [x - 1, z] - 2){
								validTerrain = true;
							}
						}
						if (valz1 != int.MinValue && valz1 < maxHeight && valz1 + redistributeThreshold > curVal) {
							terrainGrid [x, z + 1] += 1;
							//terrainGrid [x, z]--;

							if(terrainGrid[x,z] <= terrainGrid [x, z + 1] + 2 && terrainGrid[x,z] >= terrainGrid [x, z + 1] - 2){
								validTerrain = true;
							}
						}
						if (valzNeg1 != int.MinValue && valzNeg1 < maxHeight && valzNeg1 + redistributeThreshold > curVal) {
							terrainGrid [x, z - 1] += 1;
							//terrainGrid [x, z]--;

							if(terrainGrid[x,z] <= terrainGrid [x, z - 1] + 2 && terrainGrid[x,z] >= terrainGrid [x, z - 1] - 2){
								validTerrain = true;
							}
						}


						//First try step after leveling check
						if (terrainGrid [x, z] > maxHeight) {
							terrainGrid [x, z] -= 1;
						}
					} 

					if (terrainGrid [x, z] > maxHeight) {
						terrainGrid [x, z] = maxHeight;
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
	 * side effect visualizes the entire terrainGrid after particle depistion and smoothing
	**/
	public List<Vector3> createTerrainGrid(int[,] terrainGrid){
		float y = 0;

		List<Vector3> returnGrid = new List<Vector3> ();

		for (int x = 0; x < xSize; x++) {
			for (int z = 0; z < zSize; z++) {

				y = terrainGrid [x, z] * 0.5f;
				returnGrid.Add (new Vector3 (x, y, z));
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

					returnGrid.Add (new Vector3 (x, y, z));  
					  

				}
			}
		}
		return returnGrid;
	}
}
