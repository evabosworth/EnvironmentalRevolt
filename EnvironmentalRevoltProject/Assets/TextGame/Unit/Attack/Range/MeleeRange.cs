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
		

}


