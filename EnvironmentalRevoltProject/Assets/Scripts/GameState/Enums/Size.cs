using System;


public static class Size{
	public enum Sizes
	{
		TINY,
		SMALL,
		MEDIUM,
		LARGE,
		HUGR,
		GIGANTIC,
		COLOSSOL
	}

	public static Sizes breed(Sizes mother, Sizes father){
		return (Sizes)(int)(((int)mother + (int)father) / 2);
	}
}

