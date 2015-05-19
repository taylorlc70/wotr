using UnityEngine;
using System.Collections;

public class ArmyDice : DiceFaceBase {

	public override void Awake(){
		popupPrefab = Resources.Load("UI_Prefabs/ScreenSpaceUI/DiceMenus/ArmyDiceActions")as GameObject;
	}
}
