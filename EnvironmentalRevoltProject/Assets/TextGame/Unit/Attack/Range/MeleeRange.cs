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
	public MeleeRange (int maxEffectiveRange = 1, int minEffectiveRange = 0, int maxRange = 2, int minRange = 0)
	{
		this.maxRange = maxRange;
		this.minRange = minRange;
		this.maxEffectiveRange = maxEffectiveRange;
		this.minEffectiveRange = minEffectiveRange;

		heightDifferential = 1;


	}
		

}


