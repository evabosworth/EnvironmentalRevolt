using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PointRange : IRange
{
	/*
	 *All directions.
	 *Ranged takes that hit a point
	 *Other are half
	 */
	public PointRange (int maxEffectiveRange = 4, int minEffectiveRange = 0, int maxRange = 8, int minRange = 0)
	{
		this.maxRange = maxRange;
		this.minRange = minRange;
		this.maxEffectiveRange = maxEffectiveRange;
		this.minEffectiveRange = minEffectiveRange;


		heightDifferential = 5;


	}

		
}


