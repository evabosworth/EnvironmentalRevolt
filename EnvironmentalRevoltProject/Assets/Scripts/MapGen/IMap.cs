using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMap 
{
	List<Vector3> runCreateTerrain(int xSize, int zSize, int maxHeight, int redistributeThreshold, int stepSize, int passes, int numParticleStarts, int numParticleSteps);
}

