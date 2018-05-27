using UnityEngine;
using System.Collections;
using System.Collections.Generic;

	/**
	 * Class to manage Maps and map options.
	 * could also be considered a state?
	 * 		Really the primiative version of the state
	 * 		likely wont stay like this
	 */
public class MapGeneratorManager : MonoBehaviour
{

	GameObjectController objectController;

	public int xSize = 20;
	public int zSize = 20;
	GlobalVariables gv;
	DisplayObjects displayObjects;


	// Use this for initialization
	void Start ()
	{
		gv = GlobalVariables.getInstance ();
		displayObjects = DisplayObjects.getInstance ();
		gv.mapXSize = xSize;
		gv.mapZSize = zSize;

		objectController = FindObjectOfType<GameObjectController> ();

		IMap mapGen = new BattlefieldGen ();

		Battlefield battlefield = mapGen.getBattlefield (xSize, zSize);

		if(gv != null)
			gv.battlefield = battlefield;
	}
	
	// Update is called once per frame
	void Update ()
	{


	}
}

