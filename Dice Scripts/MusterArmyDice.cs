﻿using UnityEngine;
using System.Collections;

public class MusterArmyDice : DiceFaceBase {

	public override void Awake(){
		popupPrefab = Resources.Load("UI_Prefabs/ScreenSpaceUI/DiceMenus/MusterArmyDiceActions")as GameObject;
	}
}
