using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPCharacterDice : DiceFaceBase {

	public override void Awake(){
		popupPrefab = Resources.Load("UI_Prefabs/ScreenSpaceUI/DiceMenus/FPCharacterDiceActions")as GameObject;
	}
}
