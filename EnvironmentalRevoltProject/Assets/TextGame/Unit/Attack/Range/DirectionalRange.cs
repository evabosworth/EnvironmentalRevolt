using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DirectionalRange : IRange
{
	/*
	 *directional.
	 *Spear, or lance or something
	 *
	 */
	public DirectionalRange (int maxEffectiveRange = 2, int minEffectiveRange = 0, int maxRange = 4, int minRange = 0)
	{
		this.maxRange = maxRange;
		this.minRange = minRange;
		this.maxEffectiveRange = maxEffectiveRange;
		this.minEffectiveRange = minEffectiveRange;

		heightDifferential = 1;



	}

	public override List<Vector3> getAllMaxEffectiveValidTerrainTargets (Vector3 position, Battlefield battlefield)
	{
		List<Vector3> terrainTargets;
		terrainTargets = getAllValidTerrainTargetsRecursive (position, battlefield, new List<Vector3> (), maxEffectiveRange, battlefield.getAllValidPlacements ());


		return terrainTargets;
	}

	public override List<Vector3> getAllValidTerrainTargets (Vector3 position, Battlefield battlefield)
	{

		Dictionary<Vector3,ITerrain> validTerrain = battlefield.getAllValidPlacements();
		return getAllValidTerrainTargetsDirectional (position, battlefield, validTerrain);

	}

	protected List<Vector3> getAllValidTerrainTargetsDirectional(Vector3 position, Battlefield battlefield, Dictionary<Vector3,ITerrain> validTerrain){
		List<Vector3> terrainTargets = new List<Vector3> ();

		//four directions
		//x+1
		Vector3 posXP1 = position;
		//x-1
		Vector3 posXM1 = position;
		//y+1
		Vector3 posYP1 = position;
		//y-1
		Vector3 posYM1 = position;
		Vector3 posXP1hd;
		Vector3 posXM1hd;
		Vector3 posYP1hd;
		Vector3 posYM1hd;
		ITerrain terrain;

		int rangeLeft = maxRange;


		for (int x = 1; x <= rangeLeft; x++) {
			for (int depth = -heightDifferential; depth <= heightDifferential; depth++) {
				posXP1.x = position.x + x;
				posXM1.x = position.x - x;
				posYP1.y = position.y + x;
				posYM1.y = position.y - x;
				posXP1hd = posXP1;
				posXP1hd.z = posXP1hd.z + depth;
				posXM1hd = posXM1;
				posXM1hd.z = posXM1hd.z + depth;
				posYP1hd = posYP1;
				posYP1hd.z = posYP1hd.z +depth;
				posYM1hd = posYM1;
				posYM1hd.z = posYM1hd.z +depth;

				if(validTerrain.TryGetValue(posXP1hd, out terrain)){
					if(!terrainTargets.Contains(posXP1hd))
						terrainTargets.Add (posXP1hd);

				}
				if(validTerrain.TryGetValue(posXM1hd, out terrain)){
					if(!terrainTargets.Contains(posXM1hd))
						terrainTargets.Add (posXM1hd);
				}
				if(validTerrain.TryGetValue(posYP1hd, out terrain)){
					if(!terrainTargets.Contains(posYP1hd))
						terrainTargets.Add (posYP1hd);
				}
				if(validTerrain.TryGetValue(posYM1hd, out terrain)){
					if(!terrainTargets.Contains(posYM1hd))
						terrainTargets.Add (posYM1hd);
				}

			}
		}


		return terrainTargets;
	}



}


