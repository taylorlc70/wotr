using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fellowship : MonoBehaviour {

	//Instance for the singleton
	private static Fellowship instance;
	public static Fellowship Instance{
		get{
			if (instance == null) {
				instance = GameObject.FindObjectOfType<Fellowship>();
			} 
			return instance;
		}
	}

	public int companionCount;

	public New_SpaceClass lastKnownLocation;
	private CounterTrack fTrack;

	public enum Guide{
		Gandalf = 7,
		Strider = 6,
		Boromir = 5,
		Legolas = 4,
		Gimli = 3,
		Merry = 2,
		Pippin = 1,
		Gollum = 0,
	}
	private static Guide currentGuide; //Gandalf starts as guide


	//--------------------------------------------------------------------------------

	void Start(){
		companionCount = 7;
		fTrack = GameObject.Find("FellowshipTrack").GetComponent<CounterTrack>();
		ResetLastLocation();
		currentGuide = (Guide)7;
		Debug.Log ("The ftrack is " + fTrack.name);
	}

	public void ChangeGuide(int gIndex){
		currentGuide = (Guide)gIndex;
		//EventManager.GuideSwitchedFunction (); //Do I need a guide switched event?

		//Change the guide ability to the current guide's ability
		switch(currentGuide){
			case Guide.Gandalf:
				//Guide Ability = Gandalf's
				break;
			case Guide.Strider:
				//Guide ability = Strider's
				break;
		}
		Debug.Log ("The guide is now " + currentGuide.ToString());
		/*if (GameManager.userIsFP) { //If this is the FP user, then they changed the guide
			UIManager.Instance.Prompt_ConfirmAction (); //Give player option to confirm
		} //Otherwise wait for AI to finish OR other player to confirm*/
		EventManager.GuideSwitchedFunction();
	}

	public void ResetLastLocation(){
		lastKnownLocation = gameObject.transform.parent.GetComponent<New_SpaceClass>();
		Debug.Log ("The last know fellowship location is " + lastKnownLocation.name);
	}

	public void StartMoveFellowship(){
		Debug.Log ("StartMoveFellowship called");
		lastKnownLocation.StartMoveOptions (fTrack.currentCount);
	}

	public void GandalfGuideAbility(){ //Triggers when a card is played by using an event die

	}

	public void StriderGuideAbility(){ //Use any die to hide fellowship
		//Have button available when Strider is the guide
		//Make button active when Fellowship is revealed
		//Clicking on the button allows player to select a die to use to hide the fellowship
	}
}
