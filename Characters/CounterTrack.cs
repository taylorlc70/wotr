using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CounterTrack : MonoBehaviour {

	//These are assigned in the inspector
	public GameObject[] counters = new GameObject[12];
	public GameObject marker;
	//CurrentCount is the number the marker is on
	public int currentCount = 0;
	//This is for debugging. It will change to an image eventually
	private Color myColor;

	void Start(){
		//This gets the debugging color
		if (gameObject.name == "FellowshipTrack") {
			myColor = Color.blue;
		} else {
			myColor = Color.red;
		}
		SetTrack();
	}

	public void AdvanceTrack(){
		if (currentCount < counters.Length - 1) {
			currentCount += 1;
		} else {
			currentCount = counters.Length - 1;
			Debug.Log("Cannot advance any farther on the track.");
		}

		SetTrack();
	}

	//This function will be called by the player choosing a space for the fellowship to move to.
	public void MoveFellowship(){
		currentCount = 0;
		SetTrack();
	}

	public void MoveBack(int amt){
		currentCount -= amt;
		SetTrack();
	}

	private void SetTrack(){
		marker.transform.SetParent(counters [currentCount].transform);
		marker.transform.position = marker.transform.parent.position;
		for(int j = 0; j <= currentCount; j++){
			counters[j].GetComponent<Image>().color = myColor;
		}
		if(currentCount < counters.Length - 1){
			for (int i = currentCount + 1; i < counters.Length; i++) {
				counters [i].GetComponent<Image>().color = Color.white;
			}
		}
	}
}
