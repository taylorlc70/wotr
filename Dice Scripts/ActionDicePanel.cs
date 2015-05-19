using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionDicePanel : MonoBehaviour {

	public PlayerClass myPlayer;
	private GameObject dicePanelPrefab;
	
	void Start () {
		//Find which player this dice panel belongs to based on the tag of this panel parent
		//
		if (myPlayer == null) {
			if(gameObject.tag == "FP_DicePanel"){
				myPlayer = GameObject.FindGameObjectWithTag("FreePlayer").GetComponent<PlayerClass>();
			}
			if(gameObject.tag == "SH_DicePanel"){
				myPlayer = GameObject.FindGameObjectWithTag("ShadowPlayer").GetComponent<PlayerClass>();
			}
		}

		//TODO: Instantiate the correct background image for this player's dice panel
	}

}
