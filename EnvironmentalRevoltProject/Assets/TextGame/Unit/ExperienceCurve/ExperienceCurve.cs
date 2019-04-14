using UnityEngine;
using System.Collections;

public static class ExperienceCurve
{

	public static int getLevelFromStandardExp(float experienceTotal){

		float level = 0;
		level = Mathf.Pow((14f/300f*experienceTotal), (3f/4f));


		return Mathf.FloorToInt (level);

	}

}

