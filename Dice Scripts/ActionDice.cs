using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

//This script is attached to ALL dice prefabs, Shadow and Free
// It handles rolling the die and getting the correct image
// It subscrbies to the RollDie event

public class ActionDice : MonoBehaviour {

	public List<Object> images;
	private TurnManager theTurnManager;
	private UIManager theUIManager;
	private bool thisIsFPdice;

	//   Dice are automatically rolled when the TurnManager switches to the RollingDice phase,
	//   which triggers the RollingDice action that this script subscribes to in the Start function.
	//*************************************************************************************

	void Start(){
		theTurnManager = TurnManager.Instance;
		theUIManager = UIManager.Instance;
		//Get all the images for this dice depending on which prefab it is
		if (gameObject.name == "FPActionDice(Clone)") {
			thisIsFPdice = true;
			images = Resources.LoadAll("Artwork/Sprites/Dice/FP", typeof(Sprite)).ToList();
			//Load the character dice image twice
			images.Add(Resources.Load("Artwork/Sprites/Dice/FP/fpChar", typeof(Sprite)));

		} else if (gameObject.name == "SHActionDice(Clone)") {
			thisIsFPdice = false;
			images = Resources.LoadAll("Artwork/Sprites/Dice/SH", typeof(Sprite)).ToList();
			EventManager.HuntDiceChosen += MoveToHuntBox;
		}
	}

	private void RollDie(){
		StartCoroutine("DiceRollCo");
	}

	// Here we assign the dice face script depending on which button it is. Called from the rolling coroutine.
	public void GetDiceResult(){

		//HERE IS WHERE THE BUTTON COMPONENT IS ADDED TO DICE
		gameObject.AddComponent<Button>();
		gameObject.GetComponent<Button>().interactable = false;

		Sprite mySprite = gameObject.GetComponent<Image>().sprite as Sprite;
		switch (mySprite.name) {
			case "fpEV":
				gameObject.AddComponent<EventDice>();
				break;
			case "fpChar":
				gameObject.AddComponent<FPCharacterDice>();
				break;
			case "fpMuster":
				gameObject.AddComponent<MusterDice>();
				break;
			case "fpMusterArmy":
				gameObject.AddComponent<MusterArmyDice>();
				break;
			case "fpWW":
				gameObject.AddComponent<WWDice>();
				break;
			case "shEV":
				gameObject.AddComponent<EventDice>();
				break;
			case "shChar":
				gameObject.AddComponent<SHCharacterDice>();
				break;
			case "shMuster":
				gameObject.AddComponent<MusterDice>();
				break;
			case "shMusterArmy":
				gameObject.AddComponent<MusterArmyDice>();
				break;
			case "shArmy":
				gameObject.AddComponent<ArmyDice>();
				break;
			case "shEye":
				//gameObject.AddComponent<EyeDice>();
			StartCoroutine (GoToHuntBoxCR());
				break;
		}
		theUIManager.diceDone += 1;
		theUIManager.CheckIfDiceDone();
	}

	void Dice_StartHuntPhase(){
		if(!thisIsFPdice){
			gameObject.AddComponent<Toggle>();
		}
	}

	public void MoveToHuntBox(){
		Toggle myToggle = gameObject.GetComponent<Toggle>();
		if(!myToggle){
			Debug.Log("ActionDice: Cannot find toggle component");
		} else{
			if(myToggle.isOn){
				StartCoroutine(GoToHuntBoxCR()); //TODO: Finish modular UI movement coroutine
			}
		}
		if(myToggle){
			Destroy (myToggle);
		}
	}

	void EnableDie(){
		gameObject.GetComponent<Button>().interactable = true;
	}

	void OnEnable(){
		EventManager.RollingDice += RollDie;
		EventManager.ActionPhase += EnableDie;
		EventManager.HuntAllocation += Dice_StartHuntPhase;
	}

	void OnDisable(){
		EventManager.RollingDice -= RollDie;
		EventManager.ActionPhase -= EnableDie;
		EventManager.HuntAllocation -= Dice_StartHuntPhase;
		EventManager.HuntDiceChosen -= MoveToHuntBox;
		EventManager.HuntDiceChosen -= MoveToHuntBox; //This might cause an error...
	}

	//DiceRolling animation coroutine
	IEnumerator DiceRollCo(){
		if(gameObject.GetComponent<Toggle>()){
			Destroy (gameObject.GetComponent<Toggle>());
		}
		theUIManager.diceStarted += 1;
		float timer = 0.01f;
		/*for(int i = 0; i < 60; i++){
			gameObject.GetComponent<Image>().sprite = images [Random.Range(0, 5)] as Sprite;
			yield return new WaitForSeconds(timer);
		}*/
		while(timer < 0.2f){
			gameObject.GetComponent<Image>().sprite = images [Random.Range(0, 5)] as Sprite;
			yield return new WaitForSeconds(timer);
			timer += 0.01f;
		}
		GetDiceResult();
	}

	IEnumerator GoToHuntBoxCR(){
		//Call the HuntBox slide in
		GameObject.FindObjectOfType<HuntBox>().HuntBox_MoveIn();

		//Move to it!
		gameObject.transform.SetParent(GameObject.Find ("Main UI").transform, true);
		Vector2 startPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
		Vector2 endPos = GameObject.FindObjectOfType<HuntBox>().huntBox_InPos;
		RectTransform currentPos = gameObject.GetComponent<RectTransform>();
		Image img = gameObject.GetComponent<Image>();
		float elapsedTime = 0.0f;
		while(elapsedTime < 1.0f){
			currentPos.anchoredPosition = Vector2.Lerp( startPos, endPos, elapsedTime );
			Color newColor = new Color(1,1,1, Mathf.Lerp(1.0f, 0.0f, elapsedTime));
			img.color = newColor;
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		//Increase the hunt box value
		GameObject.FindObjectOfType<HuntBox>().IncreaseHuntStrength();
		//Destroy this dice
		Destroy (gameObject);
	}

}
