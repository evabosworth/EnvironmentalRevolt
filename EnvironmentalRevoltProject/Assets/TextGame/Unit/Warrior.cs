﻿using System;
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
		curHealth = maxHealth;
		IAttack basicSlash = new Slash ();
		IAttack basicStab = new Stab ();
		attacks.Add (basicSlash);
		attacks.Add (basicStab);
        move = new BasicMove(movement, jumpHeight);
        experienceReward = 100;

        strength = 5;
        physicalDefense = 5;
        intelligence = 1;
        magicDefense = 1;
        dexterity = 3;


}


	public override string ToString (){
		string warrior = "[warrior](";
		warrior += currentPosition.ToString ();
        warrior += "," + curHealth.ToString();
        warrior += "/" + maxHealth.ToString ();
		warrior += "," + move.getMovement().ToString ();
        warrior += "," + curLevel.ToString();
		//warrior += "," + attacks.ToString ();
		warrior += ");";

		return warrior;

	}
	public override void fromString(String str){


	}




}

