using UnityEngine;
using System.Collections;

public static class EventManager {
	
	//Events for switching phases.
	//Different scripts/objects can subscribe to this event depending on what they are currently doing.
	public delegate void SwitchPhaseHandler();
	public static event SwitchPhaseHandler RecAndDraw;
	public static event SwitchPhaseHandler Fellowship;
	public static event SwitchPhaseHandler HuntAllocation;
	public static event SwitchPhaseHandler RollingDice;
	public static event SwitchPhaseHandler ActionPhase;
	public static event SwitchPhaseHandler VictoryCheck;

	public delegate void SubPhaseHandler();
	public static event SubPhaseHandler GuideSwitched;

	/*public delegate void FellowshipEventHandler();
	public static event FellowshipEventHandler GuideSwitched;*/ //No need to call event when guide is switched

	public delegate void CardEventHandler();
	public static event CardEventHandler CardPlayed;

	public delegate void DiceHandler(DiceFaceBase usedDie);
	public static event DiceHandler DiceUsedEvent; 
	//public static event DiceHandler DiceDoneRolling;

	public delegate void GeneralEventHandler();
	public static event GeneralEventHandler HuntDiceChosen;
	//-------------------------------------------------------------------------------------------------
	//               Event calling functions
	//-------------------------------------------------------------------------------------------------

	//Comes from TurnManager phase switch - StartGameTurn() and AdvanceGamePhase()
	public static void PhaseEventTrigger(TurnManager.GamePhase phase){
		switch (phase) {
		case TurnManager.GamePhase.RecoverDiceAndDrawCards:
			if(RecAndDraw != null){
				RecAndDraw();
				Debug.Log("RecAndDraw triggered");
			}
			break;
		case TurnManager.GamePhase.FellowshipPhase:
			if(Fellowship != null){
				Fellowship();
				Debug.Log("Fellowship triggered");
			}
			break;
		case TurnManager.GamePhase.HuntAllocation:
			if(HuntAllocation != null){
				HuntAllocation();
				Debug.Log("HuntAllocation triggered");
			}
			break;
		case TurnManager.GamePhase.RollingActionDice:
			if(RollingDice != null){
				RollingDice();
				Debug.Log("RollingDice triggered");
			}
			break;
		case TurnManager.GamePhase.ACTION_PHASE:
			if(ActionPhase != null){
				ActionPhase();
				Debug.Log("ActionPhase triggered");
			}
			break;
		case TurnManager.GamePhase.VictoryCheck:
			if(VictoryCheck != null){
				VictoryCheck();
				Debug.Log("VictoryCheck triggered");
			}
			break;
		}
	}

	public static void GuideSwitchedFunction(){
		if(GuideSwitched != null){
			GuideSwitched();
		}
	}

	public static void CardPlayedFunction(){
		if (CardPlayed != null) {
			CardPlayed();
		}
	}

	public static void DicePlayedFunction(DiceFaceBase usedDie){ //When die is used, it calls this function and passes itself
		if (DiceUsedEvent != null) {
			DiceUsedEvent(usedDie);
		}
	}

	public static void HuntDiceChosenFunction(){
		if(HuntDiceChosen != null){
			HuntDiceChosen();
		}
	}
}
