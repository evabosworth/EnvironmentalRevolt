using UnityEngine;
using System.Collections;

public class UnityAnchor : MonoBehaviour
{
	BattlefieldManager battlefieldManager;

	// Use this for initialization
	void Start ()
	{
		battlefieldManager = new BattlefieldManager ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		string input = Input.inputString;
		if (input.Length > 0) {
			char charInput = input [0];
			int intInput = (int)charInput;
			//(values "0" to "9"
			if (intInput >= 48 && intInput <= 57) {
				intInput -= 48;
				//Vales "a" to "z"
			} else if (intInput >= 97 && intInput <= 122) {
				intInput -= 87;
			}

			battlefieldManager.acceptInput (intInput);

			GlobalVariables.getInstance ().printToConsole (""+intInput);
		}
	}
}

