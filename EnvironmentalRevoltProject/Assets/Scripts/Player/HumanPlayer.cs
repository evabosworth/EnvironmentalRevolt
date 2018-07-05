using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : IPlayer{
	

	public HumanPlayer (Dictionary<Vector3, IObject> terrainDictionary, int xSize, int zSize, float height){

		for (int x = 0; x < xSize; x++) {
			for (float y = 0; y < height; y += 0.5f) {
				for (int z = 0; z < (zSize / 2 - 2); z++) {
					IObject terrainBlock;
					Vector3 pos = new Vector3 (x, y, z);
					if (terrainDictionary.TryGetValue (pos, out terrainBlock)) {
						startingPositions.Add (pos, terrainBlock);
					}
				}
			}
		}


	}
}


