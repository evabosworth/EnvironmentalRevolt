using System;


public static class PrefabObject{
	public enum PrefabIdentifier
	{
		SPHERE
	}

	public static PrefabIdentifier breed(PrefabIdentifier mother, PrefabIdentifier father){
		return PrefabIdentifier.SPHERE;
	}
}
