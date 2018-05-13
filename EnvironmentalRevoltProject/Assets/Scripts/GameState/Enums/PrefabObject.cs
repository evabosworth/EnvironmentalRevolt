using System;


public static class PrefabObject{
	public enum PrefabIdentifier
	{
		SPHERE,
		BASICTERRAIN
	}

	public static PrefabIdentifier breed(IObject mother, IObject father){
		return PrefabIdentifier.SPHERE;
	}
}
