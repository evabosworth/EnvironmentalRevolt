using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : IUnit
{	/*
	protected float health = 100.0f;
	protected int movement = 4;
	private List<IAttack> attacks;
	*/

	public Archer(int movement = 4, int jumpHeight = 3):base(movement, jumpHeight){

		attacks = new List<IAttack> ();
		maxHealth = 125.0f;
		curHealth = maxHealth;
		IAttack longbowAttack = new LongBowAttack ();
		IAttack shortbowAttack = new ShortBowAttack ();
		attacks.Add (longbowAttack);
		attacks.Add (shortbowAttack);
		move = new BasicMove(movement, jumpHeight);
		experienceReward = 90;

		strength = 1;
		physicalDefense = 3;
		intelligence = 1;
		magicDefense = 2;
		dexterity = 5;


	}


	public override string ToString (){
		string unit = "[Archer](";
		unit += currentPosition.ToString ();
		unit += "," + curHealth.ToString();
		unit += "/" + maxHealth.ToString ();
		unit += "," + move.getMovement().ToString ();
		unit += "," + curLevel.ToString();
		//warrior += "," + attacks.ToString ();
		unit += ");";

		return unit;

	}
	public override void fromString(String str){


	}




}

