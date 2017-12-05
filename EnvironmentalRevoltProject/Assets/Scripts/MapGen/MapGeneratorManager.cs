using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGeneratorManager : MonoBehaviour
{

	IMap map;
	GameObjectGenerator objectGenerator;
	public GameObject terrain;

	public int xSize = 20;
	public int zSize = 20;
	public int maxHeight = 5;
	//0 or 1 creates hills, 3,4 creates canyons
	public int redistributeThreshold = 1; //height can only change on level at a time, not sure what this does anymore
	public int stepSize = 1;
	public int passes = 5;
	public int neighborPasses = 3;
	public int numParticleStarts = 2;
	public int numParticleSteps = 25;


	// Use this for initialization
	void Start ()
	{
		objectGenerator = FindObjectOfType<GameObjectGenerator> ();
		map = mapGen.CreateInstance<mapGen>();
		List<Vector3> mapList = map.runCreateTerrain (xSize, zSize, maxHeight, redistributeThreshold, stepSize, passes, numParticleStarts, numParticleSteps);

		foreach (Vector3 position in mapList) {
			string name = "Terrain; x:" + position.x + ", y:" + position.y + " z:" + position.z;

			objectGenerator.createAndDisplayGameObject (terrain, position, name);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{


	}
}

