using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MeleeRange : IRange
{
	/*
	 *All directions.
	 *Four Intial direction ones Max
	 *Other are half
	 */
	public MeleeRange ()
	{
		maxEffectiveRange = 1;
		minEffectiveRange = 0;

		maxRange = 2;
		minRange = 0;

		heightDifferential = 1;


	}


	public override List<Vector3> getAllMaxEffectiveValidTerrainTargets (Vector3 position, Battlefield battlefield)
	{
		
		Dictionary<Vector3,ITerrain> validTerrain = battlefield.getAllValidPlacements();
		List<Vector3> terrainTargets = new List<Vector3>();

		//four directions
		//x+1
		Vector3 posXP1 = position;
		posXP1.x += 1;
		//x-1
		Vector3 posXM1 = position;
		posXM1.x -= 1;
		//y+1
		Vector3 posYP1 = position;
		posYP1.y += 1;
		//y-1
		Vector3 posYM1 = position;
		posYM1.y -= 1;

		for (int depth = -heightDifferential; depth <= heightDifferential; depth++ ){
			
			Vector3 posXP1hd = posXP1;
			posXP1hd.z += depth;
			Vector3 posXM1hd = posXM1;
			posXM1hd.z += depth;
			Vector3 posYP1hd = posYP1;
			posYP1hd.z += depth;
			Vector3 posYM1hd = posYM1;
			posYM1hd.z += depth;

			ITerrain terrain;
			if(validTerrain.TryGetValue(posXP1hd, out terrain)){
				terrainTargets.Add (posXP1hd);
			}
			if(validTerrain.TryGetValue(posXM1hd, out terrain)){
				terrainTargets.Add (posXM1hd);
			}
			if(validTerrain.TryGetValue(posYP1hd, out terrain)){
				terrainTargets.Add (posYP1hd);
			}
			if(validTerrain.TryGetValue(posYM1hd, out terrain)){
				terrainTargets.Add (posYM1hd);
			}
			
		}


		return terrainTargets;
	} 

	public override List<Vector3> getAllValidTerrainTargets (Vector3 position, Battlefield battlefield)
	{

		Dictionary<Vector3,ITerrain> validTerrain = battlefield.getAllValidPlacements();
		return getAllValidTerrainTargetsRecursive (position, battlefield, new List<Vector3> (), maxRange, validTerrain);

	}

	private List<Vector3> getAllValidTerrainTargetsRecursive (Vector3 position, Battlefield battlefield, List<Vector3> terrainTargets, int rangeLeft, Dictionary<Vector3,ITerrain> validTerrain)
	{

		//four directions
		//x+1
		Vector3 posXP1 = position;
		posXP1.x += 1;
		//x-1
		Vector3 posXM1 = position;
		posXM1.x -= 1;
		//y+1
		Vector3 posYP1 = position;
		posYP1.y += 1;
		//y-1
		Vector3 posYM1 = position;
		posYM1.y -= 1;
		//List<Vector3> holderList = null;

		if (rangeLeft == 0) {
			return terrainTargets;
		}

		for (int depth = -heightDifferential; depth <= heightDifferential; depth++ ){
			//holderList = null;
			Vector3 posXP1hd = posXP1;
			posXP1hd.z += depth;
			Vector3 posXM1hd = posXM1;
			posXM1hd.z += depth;
			Vector3 posYP1hd = posYP1;
			posYP1hd.z += depth;
			Vector3 posYM1hd = posYM1;
			posYM1hd.z += depth;

			ITerrain terrain;
			if(validTerrain.TryGetValue(posXP1hd, out terrain)){
				if(!terrainTargets.Contains(posXP1hd))
					terrainTargets.Add (posXP1hd);
				getAllValidTerrainTargetsRecursive (posXP1hd, battlefield, terrainTargets, rangeLeft - 1, validTerrain);

			}
			if(validTerrain.TryGetValue(posXM1hd, out terrain)){
				if(!terrainTargets.Contains(posXM1hd))
					terrainTargets.Add (posXM1hd);
				getAllValidTerrainTargetsRecursive (posXM1hd, battlefield, terrainTargets, rangeLeft - 1, validTerrain);
			}
			if(validTerrain.TryGetValue(posYP1hd, out terrain)){
				if(!terrainTargets.Contains(posYP1hd))
					terrainTargets.Add (posYP1hd);
				getAllValidTerrainTargetsRecursive (posYP1hd, battlefield, terrainTargets, rangeLeft - 1, validTerrain);
			}
			if(validTerrain.TryGetValue(posYM1hd, out terrain)){
				if(!terrainTargets.Contains(posYM1hd))
					terrainTargets.Add (posYM1hd);
				getAllValidTerrainTargetsRecursive (posYM1hd, battlefield, terrainTargets, rangeLeft - 1, validTerrain);
			}

		}


		return terrainTargets;
	}

}


