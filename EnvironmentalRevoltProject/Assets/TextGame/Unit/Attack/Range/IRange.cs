using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IRange
{
	protected int maxEffectiveRange;
	protected int minEffectiveRange;

	protected int maxRange;
	protected int minRange;

	protected int heightDifferential;

	public abstract List<Vector3> getAllMaxEffectiveValidTerrainTargets(Vector3 position, Battlefield battlefield);
	public abstract List<Vector3> getAllValidTerrainTargets(Vector3 position, Battlefield battlefield);

	public int getHeightDifferential(){
		return heightDifferential;
	}
}

