using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldGen : IMap
{
	
	GameObjectController objectController;
	GlobalVariables gv;
	DisplayObjects disObjs;

	int basicHeight = 5;

	public override Battlefield getBattlefield(int xSize, int zSize){
		objectController = FindObjectOfType<GameObjectController>();
		disObjs = FindObjectOfType<DisplayObjects>();
		gv = GlobalVariables.getInstance ();

		//So this will generate just a basic mostly? flat terrain?
		Battlefield battlefield = Battlefield.CreateInstance<Battlefield>();

		battlefield.setTerrainDictionary (createPlainsTerrain (xSize, zSize));

		List<IPlayer> players = new List<IPlayer> ();
		IPlayer human = new HumanPlayer (battlefield.terrainDictionary, xSize, zSize, basicHeight);

		players.Add (human);
		battlefield.playerList = players;


		return battlefield;
	}


	private Dictionary<Vector3, IObject> createFlatLand(int xSize, int zSize){

		Dictionary<Vector3, IObject> flatland = new Dictionary<Vector3, IObject> ();
		int y = 0;
		for (int x = 0; x < xSize; x++) {
			for (int z = 0; z < zSize; z++) {
				Vector3 position = new Vector3 (x, y, z);
				string name = "Terrain; x:" + position.x + ", y:" + position.y + ", z:" + position.z;

				IObject terrainBlock = new BasicTerrainBlock (disObjs.basicTerrainDisplayObject, name, name, position);
				flatland.Add (position, terrainBlock);
			}
		}

		return flatland;


	}
		
	private Dictionary<Vector3, IObject> createPlainsTerrain(int xSize, int zSize){

		Dictionary<Vector3, IObject> terrainDict = new Dictionary<Vector3, IObject> ();

		for (int x = 0; x < xSize; x++) {
			for(float y = 0; y < basicHeight; y += gv.basicTerrainHeight){
				for (int z = 0; z < zSize; z++) {
					Vector3 position = new Vector3 (x, y, z);
					string name = "Terrain; x:" + position.x + ", y:" + position.y + ", z:" + position.z;

					GameObject basicTerrainDefault = disObjs.basicTerrainDisplayObject;

					IObject terrainBlock = new BasicTerrainBlock (basicTerrainDefault, name, name, position);
					terrainDict.Add (position, terrainBlock);
				}
			}
		}

		return terrainDict;
	}

}

