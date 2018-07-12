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

	public override void init(GameObject gameObject, string displayName, string uniqueName, Vector3 position) {
		base.init (gameObject, displayName, uniqueName, position);

		this.movement = BasicMovement.CreateInstance<BasicMovement> ();
		this.movement.init (5, 2);



	}


}

