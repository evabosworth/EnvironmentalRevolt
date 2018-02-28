using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnitState : IPlayerState
{
    GameObjectGenerator objectGenerator;
	private GlobalVariables gv;


	public override IPlayerState clickAction(RaycastHit hit){
		gv = GlobalVariables.getInstance ();
		GameObject hitObject = hit.collider.gameObject;

		gv.log ("CreateUnitState: Clicked -> CreateUnitState");

		objectGenerator = FindObjectOfType<GameObjectGenerator>();

		if (hit.collider.tag == "Terrain")
		{

			IObject obstacle = Obstacle.CreateInstance<Obstacle>();
			obstacle.updateInfo("sphere", "SphereTest", hitObject.transform.position);


			objectGenerator.createAndDisplayGameObject(obstacle);
		}

		return this;
	}

	public override IPlayerState missedClickAction(){
		gv = GlobalVariables.getInstance ();
		gv.log ("CreateUnitState: Missed Click -> CreateUnitState");

		return this;
	}
}
