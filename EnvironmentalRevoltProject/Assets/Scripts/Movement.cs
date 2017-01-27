using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float goalX;
    private float goalZ;
    public List<Vector3> targets;
    public float toVel = 2.5f;
    public float maxVel = 15.0f;
    public float maxForce = 40.0f;
    public float gain = 5f;
    private Vector3 position;
    private Vector3 target;
    private int targetCount = 0;
    private bool keepMoving = true;
    public Vector3 force;


    // Use this for initialization
    void Start()
    {
        position = gameObject.GetComponent<Rigidbody>().position;
        targets = new List<Vector3>();
        targets.Add(new Vector3(position.x,position.y+.75f,position.z));
    }

    void Update()
    {
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(targets.Count != 0)
        {
            position = gameObject.GetComponent<Rigidbody>().position;
            target = targets[targetCount];
            //target.y -= .75f;
            Vector3 dist = target - position;
            if (dist.magnitude < .01f)
            {
                targets.RemoveAt(targetCount);
            }
            // calc a target vel proportional to distance (clamped to maxVel)
            Vector3 tgtVel = Vector3.ClampMagnitude(toVel * dist, maxVel);
            // calculate the velocity error
            Vector3 error = tgtVel - gameObject.GetComponent<Rigidbody>().velocity;
            // calc a force proportional to the error (clamped to maxForce)
            force = Vector3.ClampMagnitude(gain * error, maxForce);
            gameObject.GetComponent<Rigidbody>().AddForce(force);
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0,0);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }
    }

}
