using UnityEngine;
using System.Collections;

//Written by Taylor, Bargus the Great
// Started Oct. 2014

public static class PointsManager {

	//Victory Point Handlers
	private static int fP_VP = 0;
	public static int FP_VP{
		get{
			return fP_VP;
		}
		set{
			if(fP_VP <= 0){
				fP_VP = 0;
			}
			if(fP_VP >= 10){
				fP_VP = 10;
			}
		}
	}

	private static int sH_VP = 0;
	public static int SH_VP{
		get{
			return sH_VP;
		}
		set{
			if(sH_VP <= 0){
				sH_VP = 0;
			}
			if(sH_VP >= 10){
				sH_VP = 10;
			}
		}
	}

	//Fellowship and Corruption handlers
	private static int fellowshipCounter;
	public static int FellowshipCounter{
		get{
			return fellowshipCounter;
		}
		set{
			fellowshipCounter = value;
			Debug.Log ("Fellowship Counter is now at " + fellowshipCounter);
			if(fellowshipCounter <= 0){
				Debug.Log ("Fellowship counter cannot be less than zero");
				fellowshipCounter = 0;
			}
			if(fellowshipCounter >= 12){
				Debug.Log ("Fellowship counter cannot be more than 12 - declare position");
				fellowshipCounter = 12;
			}
			MoveFellowshipCounterTo(fellowshipCounter);
		}
	}

	public static int corruptionCounter;
	private static int CorruptionCounter{
		get{
			return corruptionCounter;
		}
		set{
			corruptionCounter = value;
			Debug.Log("Corruption is now at " + corruptionCounter);
			if(corruptionCounter <= 0){
				Debug.Log ("Corruption counter cannot be less than zero");
				corruptionCounter = 0;
			}
			if(corruptionCounter >= 12){
				corruptionCounter = 12;
				Debug.Log ("Victory for Shadow Player");
			}
			MoveCorruptionCounterTo(corruptionCounter);
		}
	}

	/*
	 * ****************************************** METHODS ****************************************************
	 * */
	private static void MoveFellowshipCounterTo(int amount){
		//fellowship counter piece moves to where it needs to be
		//Might want to move this method to a script on the counter piece itself
		//If moved forward, call hunt
	}

	private static void MoveCorruptionCounterTo(int value){
		//move the piece - again, might want to move this method to the corrupt. counter piece
		if (value >= 12) {
			Debug.Log ("GAME OVER - SHADOW WINS");
			GameManager.gameRunning = false;
			//Shadow victory - game over
		}
	}

	public static void VictoryCheck(){
		if (fP_VP >= 4) {
			Debug.Log("Military victory for FP Player!");
			GameManager.gameRunning = false;
			//Military victory for FP player! Load FP victory scene
		}
		if (sH_VP >= 10) {
			Debug.Log("Military victory for Shadow Player!");
			GameManager.gameRunning = false;
			//Military victory for SH player! Load SH victory scene
		}
	}
}
