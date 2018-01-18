using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPossibleMovements : MonoBehaviour {
	
    public int maxMovement;
    private List<Node> possibleChoices;
    private Node[,] finalChoices;
    private Node[,] terrainNodes;
    public List<GameObject> possibleMovementTerrains;
    public bool ableToMove = false;
    private mapGen map;

    public GameObject movementOption;

    // Use this for initialization
    void Start () {
        map = FindObjectOfType<mapGen>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && ableToMove)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // whatever tag you are looking for on your game object
                if (hit.collider.tag == "MovementOption")
                {
                    int xtarget = (int) hit.transform.position.x;
                    int ztarget = (int) hit.transform.position.z;
                    GetComponent<Movement>().targets.Add(new Vector3(xtarget, hit.transform.position.y+.5625f, ztarget));
                    FindPath(finalChoices[xtarget,ztarget]);
                }
            }
        }
    }


    public List<Node> FindMovements(Vector3 startLocation)
    {
        foreach (var item in possibleMovementTerrains)
        {
            Destroy(item);
        }

        terrainNodes = new Node[map.xSize, map.zSize];
        for (int x = 0; x < map.xSize; x++)
        {
            for (int z = 0; z < map.zSize; z++)
            {
                terrainNodes[x, z] = new Node(x, map.terrainGrid[x, z], z);
            }
        }
        finalChoices = new Node[map.xSize, map.zSize];
        possibleMovementTerrains = new List<GameObject>();
        possibleChoices = new List<Node>();
        Node startNode = new Node((int)startLocation.x, (int)startLocation.y,(int)startLocation.z);
        CheckNeighbors(startNode,maxMovement);

        foreach (var choice in possibleChoices)
        {
            Vector3 position = new Vector3(choice.Location.x, choice.Location.y+.5f,choice.Location.z);
            GameObject terrainClone = (GameObject)Instantiate(movementOption, position, movementOption.transform.rotation);
            terrainClone.name = "movementOption:x" + choice.Location.x + ":y" + choice.Location.y + ":z" + choice.Location.z;
            possibleMovementTerrains.Add(terrainClone);
            finalChoices[(int)choice.Location.x, (int)choice.Location.z] = choice;
        }
        return possibleChoices;

    }

    void CheckNeighbors(Node startNode,int movementLeft)
    {
        int i = 0;
        while(i<movementLeft)
        {
            if(!startNode.visited)
            {
                possibleChoices.Add(new Node((int)startNode.Location.x, (int)startNode.Location.y, (int)startNode.Location.z));
                int checkZeroX = (int)startNode.Location.x - movementLeft + 1;
                int checkZeroZ = (int)startNode.Location.z - movementLeft + 1;
                int checkMaxX = (int)startNode.Location.x + movementLeft - 1;
                int checkMaxZ = (int)startNode.Location.z + movementLeft - 1;
                if (checkZeroX < 0)
                    checkZeroX = 0;
                if (checkZeroZ < 0)
                    checkZeroZ = 0;
                if (checkMaxX > map.xSize)
                    checkMaxX = map.xSize;
                if (checkMaxZ > map.zSize)
                    checkMaxZ = map.zSize;
                for (int minX = checkZeroX; minX < checkMaxX; minX ++)
                {
                    for(int minZ = checkZeroZ; minZ < checkMaxZ; minZ++)
                    {
                        //Z plus 1 location
                        if (minX == startNode.Location.x && (minZ - startNode.Location.z) == 1 && terrainNodes[minX,minZ].Location.y == startNode.Location.y)
                        {
                            CheckNeighbors(terrainNodes[minX,minZ], movementLeft - 1);
                        }
                        else if (minX == startNode.Location.x && (minZ - startNode.Location.z) == 1 && (terrainNodes[minX,minZ].Location.y - startNode.Location.y) <= 1)
                        {
                            CheckNeighbors(terrainNodes[minX, minZ], movementLeft - 1);
                        }
                        else if (minX == startNode.Location.x && (minZ - startNode.Location.z) == 1 && (terrainNodes[minX,minZ].Location.y - startNode.Location.y) >= -1)
                        {
                            CheckNeighbors(terrainNodes[minX, minZ], movementLeft - 1);
                        }
                        //X plus 1 location
                        else if (minZ == startNode.Location.z && (minX - startNode.Location.x) == 1 && terrainNodes[minX,minZ].Location.y == startNode.Location.y)
                        {
                            CheckNeighbors(terrainNodes[minX, minZ], movementLeft - 1);
                        }
                        else if (minZ == startNode.Location.z && (minX - startNode.Location.x) == 1 && (terrainNodes[minX,minZ].Location.y - startNode.Location.y) <= 1)
                        {
                            CheckNeighbors(terrainNodes[minX, minZ], movementLeft - 1);
                        }
                        else if (minZ == startNode.Location.z && (minX - startNode.Location.x) == 1 && (terrainNodes[minX,minZ].Location.y - startNode.Location.y) >= -1)
                        {
                            CheckNeighbors(terrainNodes[minX, minZ], movementLeft - 1);
                        }
                        //Z minus 1 location
                        else if (minX == startNode.Location.x && (minZ - startNode.Location.z) == -1 && terrainNodes[minX, minZ].Location.y == startNode.Location.y)
                        {
                            CheckNeighbors(terrainNodes[minX, minZ], movementLeft - 1);
                        }
                        else if (minX == startNode.Location.x && (minZ - startNode.Location.z) == -1 && (terrainNodes[minX,minZ].Location.y - startNode.Location.y) <= 1)
                        {
                            CheckNeighbors(terrainNodes[minX, minZ], movementLeft - 1);
                        }
                        else if (minX == startNode.Location.x && (minZ - startNode.Location.z) == -1 && (terrainNodes[minX,minZ].Location.y - startNode.Location.y) >= -1)
                        {
                            CheckNeighbors(terrainNodes[minX, minZ], movementLeft - 1);
                        }
                        //X minus 1 location
                        else if (minZ == startNode.Location.z && (minX - startNode.Location.x) == -1 && terrainNodes[minX, minZ].Location.y == startNode.Location.y)
                        {
                            CheckNeighbors(terrainNodes[minX, minZ], movementLeft - 1);
                        }
                        else if (minZ == startNode.Location.z && (minX - startNode.Location.x) == -1 && (terrainNodes[minX,minZ].Location.y - startNode.Location.y) <= 1)
                        {
                            CheckNeighbors(terrainNodes[minX, minZ], movementLeft - 1);
                        }
                        else if (minZ == startNode.Location.z && (minX - startNode.Location.x) == -1 && (terrainNodes[minX,minZ].Location.y - startNode.Location.y) >= -1)
                        {
                            CheckNeighbors(terrainNodes[minX, minZ], movementLeft - 1);
                        }
                        startNode.visited = true;
                    }
                }  
            }
            i++;
        }        
    }

    public List<Vector3> FindPath(Node endNode)
    {
    // The start node is the first entry in the 'open' list
    List<Vector3> path = new List<Vector3>();
        // If a path was found, follow the parents from the end node to build a list of locations
        Node node = endNode;
        while (node.ParentNode != null)
        {
            
            path.Add(node.Location);
            node = node.ParentNode;
        }

        // Reverse the list so it's in the correct order when returned
        path.Reverse();
        foreach (var step in path)
        {
            gameObject.GetComponent<Movement>().targets.Add(step);
        }
        return path;
    }
    
}
