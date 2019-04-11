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

	public Warrior(int movement = 6, int jumpHeight = 2):base(movement, jumpHeight){
        
		attacks = new List<IAttack> ();
		maxHealth = 200.0f;
		IAttack basicSlash = new Slash ();
		attacks.Add (basicSlash);
        move = new BasicMove(movement, jumpHeight);


	}


	public override string toString (){
		string warrior = "[warrior](";
		warrior += currentPosition.ToString ();
		warrior += "," + maxHealth.ToString ();
		warrior += "," + move.getMovement().ToString ();
		//warrior += "," + attacks.ToString ();
		warrior += ");";

		return warrior;

	}
	public override void fromString(String str){


	}


}

