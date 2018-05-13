using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectController : MonoBehaviour
{
	GlobalVariables gv;

	public void Start(){
		gv = FindObjectOfType<GlobalVariables> ();

	}

	public GameObject createAndDisplayGameObject(IObject codeObject){
		GameObject objectClone = null;
		objectClone = (GameObject)Instantiate (codeObject.gameObject, codeObject.position, Quaternion.identity);

		objectClone.transform.SetParent (this.transform);
		objectClone.transform.name = codeObject.uniqueName;


		return objectClone;
	}

	public void highlightAll(List<IObject> codeObjects, string highlightName){

		foreach (IObject codeObject in codeObjects) {
			GameObject builtGameObject = GameObject.Find (codeObject.uniqueName);
			changeHighlighting(codeObject.gameObject, highlightName, true);


		}
	}

	public void removeHighlight(List<IObject> codeObjects, string highlightName){

		foreach (IObject codeObject in codeObjects) {
			GameObject builtGameObject = GameObject.Find (codeObject.uniqueName);
			changeHighlighting(codeObject.gameObject, highlightName, false);


		}
	}

	public void changeHighlighting(GameObject gamePiece, string searchName, bool active){

		if (gamePiece != null) {
			Transform gameTransform = gamePiece.transform.Find (searchName);
			if (gameTransform != null) {
				gameTransform.gameObject.SetActive(active);
			}
		}	
	}

	public void moveObject(IObject codeObject, Vector3 toPos){
		toPos.y += gv.unitHeightModifier;

		gv.log ("Move unit " + codeObject.uniqueName + " from " + codeObject.position.ToString() + " to " + toPos.ToString());
		GameObject builtGameObject = GameObject.Find (codeObject.uniqueName);
		if (builtGameObject != null) {
			builtGameObject.transform.SetPositionAndRotation (toPos, Quaternion.identity);
		}
	}
}

