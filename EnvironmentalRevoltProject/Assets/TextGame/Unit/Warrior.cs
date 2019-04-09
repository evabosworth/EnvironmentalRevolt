using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : IUnit
{	/*
	protected float health = 100.0f;
	protected int movement = 4;
	private List<IAttack> attacks;
	*/

	public Warrior(){
		attacks = new List<IAttack> ();
		maxHealth = 200.0f;
		movement = 6;
		IAttack basicSlash = new Slash ();
		attacks.Add (basicSlash);


	}


	public override string toString (){
		string warrior = "[warrior](";
		warrior += currentPosition.ToString ();
		warrior += "," + maxHealth.ToString ();
		warrior += "," + movement.ToString ();
		//warrior += "," + attacks.ToString ();
		warrior += ");";

		return warrior;

	}
	public override void fromString(String str){


	}


}

