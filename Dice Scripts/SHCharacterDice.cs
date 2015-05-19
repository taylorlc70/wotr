using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SHCharacterDice : DiceFaceBase {

	public override void Awake(){
		popupPrefab = Resources.Load("UI_Prefabs/ScreenSpaceUI/DiceMenus/SHCharacterDiceActions")as GameObject;
	}
	
}