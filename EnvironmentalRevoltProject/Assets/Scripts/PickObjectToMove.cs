using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObjectToMove : MonoBehaviour {

    public GameObject lastChosen;
    public FindPossibleMovements lastMove;
    public FindPossibleMovements move;
    public List<Node> possibleMovements;

	// Use this for initialization
	void Start () {
        possibleMovements = new List<Node>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {

                // whatever tag you are looking for on your game object
                if (hit.collider.tag == "Character")
                {
					/*
                    System.Console.Out.WriteLine("clicked character");
                    possibleMovements = new List<Node>();
                    lastMove = lastChosen.GetComponent<FindPossibleMovements>();
                    lastMove.ableToMove = false; 
                    move = hit.collider.gameObject.GetComponent<FindPossibleMovements>();
                    move.ableToMove = true;
                    lastChosen = hit.rigidbody.gameObject;
                    possibleMovements = move.FindMovements(new Vector3(hit.transform.position.x, hit.transform.position.y-.75f, hit.transform.position.z));
					*/
				}
            }
        }
    }
    
    void FixedUpdate()
    {

    }
}
