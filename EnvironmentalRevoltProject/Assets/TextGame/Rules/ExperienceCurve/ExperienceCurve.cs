using UnityEngine;
using System.Collections;

public class ExperienceCurve : IExperienceRules
{

	public override int getLevelFromStandardExp(float experienceTotal){

		float level = 0;
		//50% experience = level 60
		//level = Mathf.Pow((14f/300f*experienceTotal), (3f/4f));

		//50% experience = level 65
		level = Mathf.Pow((22.5f*experienceTotal), (3f/5f));


		return Mathf.FloorToInt (level);

	}

}

