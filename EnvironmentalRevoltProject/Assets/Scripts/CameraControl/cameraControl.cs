using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour {
	//TODO: Needs to move on angles! right now it moves equal to field, but should rotate to go to screen

	float zoomSpeed = 1.0f;
	 
	public Transform target;
	public float smoothTime = 0.0f;
	float maxSpeed = 1.0f;


	// Use this for initialization
	void Start () {
		target = gameObject.transform;
	
		InvokeRepeating ("Rotate", 0.0f, 0.5f);
	}

	void Rotate(){

		if (Input.GetAxis ("Rotate") > 0) {
			transform.Rotate (new Vector3 (0, 90, 0));
		} else if (Input.GetAxis ("Rotate") < 0) {
			transform.Rotate (new Vector3 (0, -90, 0));
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Horizontal") > 0){

			Vector3 velocity = Vector3.zero;    
			Vector3 targetPosition = target.TransformPoint(new Vector3 (maxSpeed, 0.0f, -maxSpeed)); 
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);


		} else if (Input.GetAxis ("Horizontal") < 0) {
			Vector3 velocity = Vector3.zero;    
			Vector3 targetPosition = target.TransformPoint(new Vector3 (-maxSpeed, 0.0f, maxSpeed)); 
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

		} 


		if (Input.GetAxis ("Vertical") > 0) {
			Vector3 velocity = Vector3.zero;    
			Vector3 targetPosition = target.TransformPoint(new Vector3 (maxSpeed, 0.0f, maxSpeed)); 
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		} else if (Input.GetAxis ("Vertical") < 0) {
			Vector3 velocity = Vector3.zero;    
			Vector3 targetPosition = target.TransformPoint(new Vector3 (-maxSpeed, 0.0f, -maxSpeed)); 
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		}

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			gameObject.transform.Translate (new Vector3 (0.0f, zoomSpeed, 0.0f));
				
		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			gameObject.transform.Translate (new Vector3 (0.0f, -zoomSpeed, 0.0f));
			
		}

	}
}
