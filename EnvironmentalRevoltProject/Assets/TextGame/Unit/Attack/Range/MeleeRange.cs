using System;


public class MeleeRange : IRange
{
	public MeleeRange ()
	{
		maxEffectiveRange = 1;
		minEffectiveRange = 0;

		maxRange = 2;
		minRange = 0;
	}
}


