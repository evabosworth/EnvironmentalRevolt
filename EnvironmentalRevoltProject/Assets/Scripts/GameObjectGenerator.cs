using UnityEngine;
using System.Collections;

public class GameObjectGenerator : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void createAndDisplayGameObject(GameObject terrain, Vector3 position, string name){
		GameObject terrainClone = (GameObject)Instantiate (terrain, position, transform.rotation);
		terrainClone.transform.SetParent(this.transform); 
		terrainClone.name = name;
	}
}

