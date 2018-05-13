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

	IMap map;
	GameObjectController objectController;

	public int xSize = 20;
	public int zSize = 20;
	public int maxHeight = 5;
	//0 or 1 creates hills, 3,4 creates canyons??
	public int redistributeThreshold = 1; //height can only change on level at a time, not sure what this does anymore
	public int stepSize = 1;
	public int passes = 5;
	public int neighborPasses = 3;
	public int numParticleStarts = 2;
	public int numParticleSteps = 25;
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
		map = mapGen.CreateInstance<mapGen>();
		List<Vector3> battlefieldGrid = map.runCreateTerrain (xSize, zSize, maxHeight, redistributeThreshold, stepSize, passes, numParticleStarts, numParticleSteps);

		//Dictionary containing location of a block and info on the block
		Dictionary<Vector3, IObject> terrainDictionary = new Dictionary<Vector3, IObject>();

		foreach (Vector3 position in battlefieldGrid) {
			string name = "Terrain; x:" + position.x + ", y:" + position.y + " z:" + position.z;
			IObject terrain = new BasicTerrainBlock (displayObjects.basicTerrainDisplayObject, "Terrain", name, position);

			GameObject createdTerrain = objectController.createAndDisplayGameObject (terrain);
			terrain.gameObject = createdTerrain;

			terrainDictionary.Add (position, terrain);

		}
		Battlefield battlefield = Battlefield.CreateInstance<Battlefield> ();
		battlefield.setDictionary(terrainDictionary);


		if(gv != null)
			gv.battlefield = battlefield;
	}
	
	// Update is called once per frame
	void Update ()
	{


	}
}

