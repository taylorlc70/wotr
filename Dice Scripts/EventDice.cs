using UnityEngine;
using System.Collections;

public class EventDice : DiceFaceBase {

	public override void Awake(){
		popupPrefab = Resources.Load("UI_Prefabs/ScreenSpaceUI/DiceMenus/EventDiceActions")as GameObject;
	}
	
}
