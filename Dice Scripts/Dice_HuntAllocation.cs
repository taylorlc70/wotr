using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dice_HuntAllocation : MonoBehaviour {
	
	void Awake () {
		if(gameObject.GetComponent<Button>()){
			Destroy (gameObject.GetComponent<Button>());
		}
		if(!gameObject.GetComponent<Toggle>()){
			gameObject.AddComponent<Toggle>();
		}
	}

	void Start(){
		Toggle myToggle = gameObject.GetComponent<Toggle>();
		//myToggle.onValueChanged.AddListener (() => FUNCTION_NAME());
	}

}
