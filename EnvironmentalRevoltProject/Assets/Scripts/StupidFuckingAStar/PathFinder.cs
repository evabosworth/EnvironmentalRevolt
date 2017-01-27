﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;

//namespace SimpleAStarExample
//{
//    public class PathFinder
//    {
//        private int xLength;
//        private int yLength;
//        private int zLength;
//        private Node[,,] nodes;
//        private Node startNode;
//        private Node endNode;
//        private SearchParameters searchParameters;
//        GameObject[] terrains = GameObject.FindGameObjectsWithTag("Terrain");

//        /// <summary>
//        /// Create a new instance of PathFinder
//        /// </summary>
//        /// <param name="searchParameters"></param>
//        public PathFinder(SearchParameters searchParameters)
//        {
//            this.searchParameters = searchParameters;
//            InitializeNodes(searchParameters.Map);
//            this.startNode = this.nodes[(int)searchParameters.StartLocation.x, (int)searchParameters.StartLocation.y, (int)searchParameters.StartLocation.z];
//            this.startNode.State = NodeState.Open;
//            this.endNode = this.nodes[(int)searchParameters.EndLocation.x, (int)searchParameters.StartLocation.y, (int)searchParameters.EndLocation.z];
//        }

//        /// <summary>
//        /// Attempts to find a path from the start location to the end location based on the supplied SearchParameters
//        /// </summary>
//        /// <returns>A List of Points representing the path. If no path was found, the returned list is empty.</returns>
//        public List<Vector3> FindPath()
//        {
//            // The start node is the first entry in the 'open' list
//            List<Vector3> path = new List<Vector3>();
//            bool success = Search(startNode);
//            if (success)
//            {
//                // If a path was found, follow the parents from the end node to build a list of locations
//                Node node = this.endNode;
//                while (node.ParentNode != null)
//                {
//                    path.Add(node.Location);
//                    node = node.ParentNode;
//                }

//                // Reverse the list so it's in the correct order when returned
//                path.Reverse();
//            }

//            return path;
//        }

//        /// <summary>
//        /// Builds the node grid from a simple grid of booleans indicating areas which are and aren't walkable
//        /// </summary>
//        /// <param name="map">A boolean representation of a grid in which true = walkable and false = not walkable</param>
//        private void InitializeNodes(bool[,,] map)
//        {
//            this.xLength = map.GetLength(0);
//            this.yLength = map.GetLength(1);
//            this.nodes = new Node[xLength, yLength,zLength];
//            for (int z = 0; z < this.zLength; z++)
//            {
//                for (int x = 0; x < this.xLength; x++)
//                {
//                    for(int y = 0;y< yLength;y++ )
//                    {
//                        this.nodes[x, y, z] = new Node(x, y, z, map[x, y, z], this.searchParameters.EndLocation);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Attempts to find a path to the destination node using <paramref name="currentNode"/> as the starting location
//        /// </summary>
//        /// <param name="currentNode">The node from which to find a path</param>
//        /// <returns>True if a path to the destination has been found, otherwise false</returns>
//        private bool Search(Node currentNode)
//        {
//            // Set the current node to Closed since it cannot be traversed more than once
//            currentNode.State = NodeState.Closed;
//            List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);

//            // Sort by F-value so that the shortest possible routes are considered first
//            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
//            foreach (var nextNode in nextNodes)
//            {
//                // Check whether the end node has been reached
//                if (nextNode.Location == this.endNode.Location)
//                {
//                    return true;
//                }
//                else
//                {
//                    // If not, check the next set of nodes
//                    if (Search(nextNode)) // Note: Recurses back into Search(Node)
//                        return true;
//                }
//            }

//            // The method returns false if this path leads to be a dead end
//            return false;
//        }

//        /// <summary>
//        /// Returns any nodes that are adjacent to <paramref name="fromNode"/> and may be considered to form the next step in the path
//        /// </summary>
//        /// <param name="fromNode">The node from which to return the next possible nodes in the path</param>
//        /// <returns>A list of next possible nodes in the path</returns>
//        private List<Node> GetAdjacentWalkableNodes(Node fromNode)
//        {
//            List<Node> walkableNodes = new List<Node>();
//            IEnumerable<Vector3> nextLocations = GetAdjacentLocations(fromNode.Location);

//            foreach (var location in nextLocations)
//            {
//                int x = (int) location.x;
//                int terrainY = 0;
//                foreach (var item in terrains)
//                {
//                    if(item.transform.position.x == x && item.transform.position.z == location.z)
//                    {
//                        terrainY = (int) item.transform.position.y;
//                    }
//                }
//                int y = terrainY;
//                int z = (int) location.z;

//                // Stay within the grid's boundaries
//                if (x < 0 || x >= this.xLength || y < 0 || y >= this.yLength)
//                    continue;

//                Node node = this.nodes[x, y, z];

//                // Ignore non-walkable nodes
//                if (!node.IsWalkable)
//                    continue;

//                if (node.Location.y - fromNode.Location.y > 1 || node.Location.y - fromNode.Location.y < -1)
//                    continue;

//                // Ignore already-closed nodes
//                if (node.State == NodeState.Closed)
//                    continue;

//                // Already-open nodes are only added to the list if their G-value is lower going via this route.
//                if (node.State == NodeState.Open)
//                {
//                    float traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
//                    float gTemp = fromNode.G + traversalCost;
//                    if (gTemp < node.G)
//                    {
//                        node.ParentNode = fromNode;
//                        walkableNodes.Add(node);
//                    }
//                }
//                else
//                {
//                    // If it's untested, set the parent and flag it as 'Open' for consideration
//                    node.ParentNode = fromNode;
//                    node.State = NodeState.Open;
//                    walkableNodes.Add(node);
//                }
//            }

//            return walkableNodes;
//        }

//        /// <summary>
//        /// Returns the eight locations immediately adjacent (orthogonally and diagonally) to <paramref name="fromLocation"/>
//        /// </summary>
//        /// <param name="fromLocation">The location from which to return all adjacent points</param>
//        /// <returns>The locations as an IEnumerable of Points</returns>
//        private static IEnumerable<Vector3> GetAdjacentLocations(Vector3 fromLocation)
//        {
//            return new Vector3[]
//            {
//                new Vector3(fromLocation.x-1, fromLocation.y, fromLocation.z-1),
//                new Vector3(fromLocation.x-1, fromLocation.y, fromLocation.z),
//                new Vector3(fromLocation.x-1, fromLocation.y, fromLocation.z+1),
//                new Vector3(fromLocation.x, fromLocation.y, fromLocation.z+1),
//                new Vector3(fromLocation.x+1, fromLocation.y, fromLocation.z+1),
//                new Vector3(fromLocation.x+1, fromLocation.y, fromLocation.z),
//                new Vector3(fromLocation.x+1, fromLocation.y, fromLocation.z-1),
//                new Vector3(fromLocation.x, fromLocation.y,   fromLocation.z-1)
//            };
//        }
//    }
//}
