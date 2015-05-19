using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour {

	//public GUITexture fader;
	public Vector3[] startPositions;

	// Use this for initialization
	void Start () {
		//startPositions [0] = new Vector3 (65, 8, 5);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += Vector3.right*Time.deltaTime;
	}

	//IEnumerator StartPan(){
		//yield return StartCoroutine(Pan(startPositions[0], startPositions[1]));
	//}
}
