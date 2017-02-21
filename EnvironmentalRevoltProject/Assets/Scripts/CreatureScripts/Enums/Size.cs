using System;


public static class Size{
	public enum Sizes
	{
		tiny,
		small,
		medium,
		large,
		huge,
		giagantic,
		colossal
	}

	public static Sizes breed(Sizes mother, Sizes father){
		return (Sizes)(int)(((int)mother + (int)father) / 2);
	}
}

