using UnityEngine;
using System.Collections;

public class MusterDice : DiceFaceBase {

	public override void Awake(){
		popupPrefab = Resources.Load("UI_Prefabs/ScreenSpaceUI/DiceMenus/MusterDiceActions")as GameObject;
	}
}
