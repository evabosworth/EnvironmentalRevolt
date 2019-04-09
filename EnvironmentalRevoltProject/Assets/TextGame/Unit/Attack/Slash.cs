using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : IAttack
{
	//IRange range;
	public Slash(){
		damage = 50;
		range = new MeleeRange();
	}

}
