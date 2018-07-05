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

	public void addHighlights(List<IObject> codeObjects, string highlightName){
		if (codeObjects == null) {
			return;
		}


		foreach (IObject codeObject in codeObjects) {
			GameObject builtGameObject = GameObject.Find (codeObject.uniqueName);
			changeHighlight(codeObject, highlightName, true);


		}
	}

	public void removeHighlights(List<IObject> codeObjects, string highlightName){
		if (codeObjects == null) {
			return;
		}

		foreach (IObject codeObject in codeObjects) {
			GameObject builtGameObject = GameObject.Find (codeObject.uniqueName);
			changeHighlight(codeObject, highlightName, false);


		}
	}

	public void changeHighlight(IObject gamePiece, string searchName, bool active){

		if (gamePiece != null) {
			GameObject builtGameObject = GameObject.Find (gamePiece.uniqueName);
			Transform gameTransform = builtGameObject.gameObject.transform.Find (searchName);
			if (gameTransform != null) {
				gameTransform.gameObject.SetActive (active);
				gamePiece.gameObject = builtGameObject;
			}

		}

	}

	public void moveObject(IObject codeObject, Vector3 toPos){
		toPos.y += gv.unitHeightModifier;

		gv.log ("GameObjectController: Move unit " + codeObject.uniqueName + " from " + codeObject.position.ToString() + " to " + toPos.ToString());
		GameObject builtGameObject = GameObject.Find (codeObject.uniqueName);
		if (builtGameObject != null) {
			builtGameObject.transform.SetPositionAndRotation (toPos, Quaternion.identity);
		}
	}

	public void removeObject(IObject codeObject){
		GameObject builtGameObject = GameObject.Find (codeObject.uniqueName);

		GameObject.Destroy (builtGameObject);
	}
}

