using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CancelButtonClick : MonoBehaviour {

	public GameObject thisPanel;

	// Use this for initialization
	void Start () {
		thisPanel = transform.parent.gameObject;
	}

	public void CancelMenu(){
		Debug.Log("Cancel button clicked");
		Destroy(thisPanel);
	}
}
