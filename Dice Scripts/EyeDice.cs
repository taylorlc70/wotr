using UnityEngine;
using System.Collections;

public class EyeDice : DiceFaceBase {
	GameObject shadowPlayer;

	public override void Start(){
		shadowPlayer = GameObject.FindGameObjectWithTag("ShadowPlayer");
		shadowPlayer.GetComponent<PlayerDiceScript>().myDice_UIbuttons.Remove(gameObject);
	}
}
