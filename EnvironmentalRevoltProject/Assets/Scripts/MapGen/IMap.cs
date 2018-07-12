using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMap : ScriptableObject
{
	GameObjectController objectController;
	GlobalVariables gv;
	DisplayObjects disObjs;

	public virtual Battlefield getBattlefield (int xSize, int zSize){
		Battlefield battlefield = Battlefield.CreateInstance<Battlefield> ();
		battlefield.setTerrainDictionary (createFlatLand (xSize, zSize));

		return battlefield;
	}

	private Dictionary<Vector3, IObject> createFlatLand(int xSize, int zSize){
		objectController = FindObjectOfType<GameObjectController>();
		disObjs = FindObjectOfType<DisplayObjects>();
		gv = GlobalVariables.getInstance ();


		Dictionary<Vector3, IObject> flatland = new Dictionary<Vector3, IObject> ();
		int y = 0;
		for (int x = 0; x < xSize; x++) {
			for (int z = 0; z < zSize; z++) {
				Vector3 position = new Vector3 (x, y, z);
				string name = "Terrain; x:" + position.x + ", y:" + position.y + " z:" + position.z;

				//IObject terrainBlock = new BasicTerrainBlock (disObjs.basicTerrainDisplayObject, name, name, position);
				IObject terrainBlock = BasicTerrainBlock.CreateInstance<BasicTerrainBlock>();
				terrainBlock.init (disObjs.basicTerrainDisplayObject, name, name, position);

				flatland.Add (position, terrainBlock);
			}
		}

		return flatland;


	}

}

