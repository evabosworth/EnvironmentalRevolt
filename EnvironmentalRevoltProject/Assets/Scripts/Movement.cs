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

    // Use this for initialization
    void Start()
    {
        position = gameObject.GetComponent<Rigidbody>().position;
        targets.Add(position);
        targets.Add(position);
        targets.Add(position);
    }

    void Update()
    {
        position = gameObject.GetComponent<Rigidbody>().position;
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // whatever tag you are looking for on your game object
                if (hit.collider.tag == "Terrain")
                {
                    if (hit.transform.position.y + .75 == position.y)
                    {
                        targets.Add(new Vector3(hit.transform.position.x, position.y, position.z));
                        targets.Add(new Vector3(hit.transform.position.x, position.y, hit.transform.position.z));
                        keepMoving = true;
                    }
                    else
                    {
                        targets.Add(new Vector3(hit.transform.position.x, position.y, position.z));
                        targets.Add(new Vector3(hit.transform.position.x, position.y, hit.transform.position.z - 1));
                        targets.Add(new Vector3(hit.transform.position.x, hit.transform.position.y + .75f, hit.transform.position.z - 1));
                        targets.Add(new Vector3(hit.transform.position.x, hit.transform.position.y + .75f, hit.transform.position.z));
                        keepMoving = true;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = gameObject.GetComponent<Rigidbody>().position;
        if (targets.Count.Equals(targetCount))
        {
            keepMoving = false;
        }
        if (keepMoving == true)
        {
            target = targets[targetCount];
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
