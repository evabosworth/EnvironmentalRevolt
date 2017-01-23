using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private int x = 0;
    public float goalX;
    public float goalZ;
    public List<Vector3> targets;
    public float toVel = 2.5f;
    public float maxVel = 15.0f;
    public float maxForce = 40.0f;
    public float gain = 5f;
    private Vector3 position;
    private Vector3 target;
    private int targetCount = 0;
    private bool stopped = false;

    // Use this for initialization
    void Start () {
        position = gameObject.GetComponent<Rigidbody>().position;
        target = targets[targetCount];
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        x++;
        position = gameObject.GetComponent<Rigidbody>().position;
        if (x > 100)
        {
            Vector3 dist = target - position;
            if (dist.magnitude < .01f)
            {
                //Reached Target, go to next target
                targetCount++;
                target = targets[targetCount];
            }
            // calc a target vel proportional to distance (clamped to maxVel)
            Vector3 tgtVel = Vector3.ClampMagnitude(toVel * dist, maxVel);
            // calculate the velocity error
            Vector3 error = tgtVel - gameObject.GetComponent<Rigidbody>().velocity;
            // calc a force proportional to the error (clamped to maxForce)
            Vector3 force = Vector3.ClampMagnitude(gain * error, maxForce);
            gameObject.GetComponent<Rigidbody>().AddForce(force);
           
        }
    }

}
