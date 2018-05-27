using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dice {

	public static int roll(int num){
		int rolled = Random.Range (0, num - 1);

		return rolled;
	}

	public static  int rollAll(List<int> nums){
		int total = 0;
		foreach (int num in nums) {
			total += roll (num);

		}

		return total;
	}



}
