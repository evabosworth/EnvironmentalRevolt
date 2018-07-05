using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour {
	//TODO: Needs to move on angles! right now it moves equal to field, but should rotate to go to screen

	float zoomSpeed = 1.0f;
	 
	public Transform target;
	public float smoothTime = 0.50f;
	float maxSpeed = 0.125f;
	private bool lockRotate = false;

	// Use this for initialization
	void Start () {
		target = gameObject.transform;
	
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("HorizontalCamera") > 0){

			Vector3 velocity = Vector3.zero;    
			Vector3 targetPosition = target.TransformPoint(new Vector3 (maxSpeed, 0.0f, -maxSpeed)); 
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);


		} else if (Input.GetAxis ("HorizontalCamera") < 0) {
			Vector3 velocity = Vector3.zero;    
			Vector3 targetPosition = target.TransformPoint(new Vector3 (-maxSpeed, 0.0f, maxSpeed)); 
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

		} 
			
		if (Input.GetButtonUp ("Rotate")) {
			lockRotate = false;
		}

		if (!lockRotate) {
			Transform from = transform;
			Transform to = transform;
			float rotateSpeed = 0.01f;
			if (Input.GetAxis ("Rotate") > 0) {
				//transform.Rotate (new Vector3 (0, 45, 0));
				to.Rotate (new Vector3 (0, 45, 0));
				transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * rotateSpeed);
				lockRotate = true;
			} else if (Input.GetAxis ("Rotate") < 0) {
				//transform.Rotate (new Vector3 (0, -45, 0));
				to.Rotate (new Vector3 (0, -45, 0));
				transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * rotateSpeed);
				lockRotate = true;
			}
		}

		if (Input.GetAxis ("VerticalCamera") > 0) {
			Vector3 velocity = Vector3.zero;    
			Vector3 targetPosition = target.TransformPoint(new Vector3 (maxSpeed, 0.0f, maxSpeed)); 
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		} else if (Input.GetAxis ("VerticalCamera") < 0) {
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
