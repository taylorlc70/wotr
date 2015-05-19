using UnityEngine;
using System.Collections;

//Written by Taylor, Bargus the Great
// Started Oct. 2014

public class CounterTester : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			PointsManager.corruptionCounter += 1;
		}
		if(Input.GetKeyDown(KeyCode.F)){
			PointsManager.FellowshipCounter += 1;
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			PointsManager.corruptionCounter -= 1;
		}
		if(Input.GetKeyDown(KeyCode.V)){
			PointsManager.FellowshipCounter -= 1;
		}
	}
}
