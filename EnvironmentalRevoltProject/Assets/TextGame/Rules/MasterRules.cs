using UnityEngine;
using System.Collections;

public class MasterRules
{
	private static MasterRules masterRules = null;
	private IExperienceRules experienceRules = null;



	//default rules
	private MasterRules (){
		experienceRules = new ExperienceCurve ();
	
	}

	public static MasterRules getInstance(){
		if (masterRules == null) {
			masterRules = new MasterRules ();
		}
		return masterRules;
	}

	public IExperienceRules ExperienceRules {
		get {
			return experienceRules;
		}
	}


}

