using UnityEngine;
using System.Collections;

public class GameObjectGenerator : MonoBehaviour
{
	public GameObject sphere;
	GlobalVariables gv;

	public void Start(){
		gv = FindObjectOfType<GlobalVariables> ();

	}

	public GameObject createAndDisplayGameObject(IObject noneGameObject){
		GameObject objectClone = null;
		if (noneGameObject.prefabName == PrefabObject.PrefabIdentifier.SPHERE) {
			objectClone = (GameObject)Instantiate (sphere, noneGameObject.position, Quaternion.identity);
		}
		gameObject.transform.SetParent (this.transform);

		return objectClone;
	}


	/**
	 * TOOD: change terrain to a class that contians a gameobject terrain, maybe??? probably, yeah... maybe
	 */
	public GameObject createAndDisplayGameObject(GameObject gameObject, Vector3 position, string name){
		GameObject terrainClone = (GameObject)Instantiate (gameObject, position, transform.rotation);
		terrainClone.transform.SetParent(this.transform); 
		terrainClone.name = name;
	
		return terrainClone;
	}
}

