using UnityEngine;
using System.Collections;

public class WWDice : DiceFaceBase {

	public override void Awake(){
		popupPrefab = Resources.Load("UI_Prefabs/ScreenSpaceUI/DiceMenus/WillWestPopup")as GameObject;
	}
}
