using System;
using UnityEngine;
using System.Collections;

public class BellCurve: IObject
{
	public double mu; //changes center pos
	public double sigma; //changes variance
	public double modifier; //for gaming, to add things like luck
	public double std; //standard deviation

	public BellCurve(double mu, double std){
		this.mu = mu;
		this.sigma = std/2;
		this.std = std;
	
	}

	public float getRandomValue(float modifier){
		float mu = (float)this.mu + modifier;
		float endRange = ((float)sigma * 3.0f);



		double x = UnityEngine.Random.Range (0.0f, 100.0f);

		double value = Math.Exp (-(Math.Pow ((x - mu), 2)) / (2 * (Math.Pow (sigma, 2)))) / Math.Sqrt (2 * Math.PI * Math.Pow (sigma, 2));

		return (float)value;



	}


}

