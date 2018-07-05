using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacter: IObject
{
	public float initiative = 0;
	public float speed;

	public float strength;
	public float dexterity;
	public float meleeDefense;
	public float rangedDefense;
	public float magic;
	public float magicDefense;
	public List<ElementalAptitude> elementalAptitude;


	public float maxHealth;
	public float curHealth;


	public ICharacter(){
	}

	public ICharacter(GameObject gameObject, string displayName, string uniqueName, Vector3 position) : base(gameObject,displayName,uniqueName,position) {

		this.movement = new BasicMovement (5, 2);



	}

}

