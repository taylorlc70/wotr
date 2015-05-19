using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HuntBox : MonoBehaviour {

	public int huntStrength;
	public Fellowship theFellowship;
	public RectTransform currentPos;
	public Vector2 huntBox_InPos = new Vector2(50.0f, -100.0f);
	public Vector2 huntBox_OutPos = new Vector2(-50.0f, -100.0f);

	public GameObject shadowPanel;

	void Start(){
		theFellowship = Fellowship.Instance;
		currentPos = gameObject.GetComponent<RectTransform>();
		shadowPanel = GameObject.FindGameObjectWithTag("SH_DicePanel");
	}

	public void IncreaseHuntStrength(){
		if(huntStrength < theFellowship.companionCount){
			huntStrength ++;
			gameObject.transform.GetComponentInChildren<Text>().text = huntStrength.ToString();
		} else{
			Debug.Log ("Cannot add more dice to the hunt box than there are companions in the fellowship.");
		}
	}

	public void ResetHuntStrength(){
		huntStrength = 0;
		gameObject.transform.GetComponentInChildren<Text>().text = huntStrength.ToString();
	}

	
	public void HuntBox_MoveIn(){
		StartCoroutine(HuntBox_SlideInCR());
	}
	public void HuntBox_MoveOut(){
		StartCoroutine(HuntBox_SlideOutCR());
	}

	void OnEnable(){
	//	EventManager.HuntDiceChosen += HuntBox_MoveIn;
		EventManager.RecAndDraw += ResetHuntStrength;
	}
	void OnDisable(){
	//	EventManager.HuntDiceChosen -= HuntBox_MoveIn;
		EventManager.RecAndDraw -= ResetHuntStrength;
	}
	//------------------------------------------------------------------------------
	//TODO: Finish my UI SlideIn/Out script so this is modularized to work with any UI
	//------------------------------------------------------------------------------
	private IEnumerator HuntBox_SlideInCR(){ 
		float elapsedTime = 0.0f;
		while(elapsedTime < 1.0f){
			currentPos.anchoredPosition = Vector2.Lerp( huntBox_OutPos, huntBox_InPos, elapsedTime );
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(2.5f);
		StartCoroutine(HuntBox_SlideOutCR());
	}

	private IEnumerator HuntBox_SlideOutCR(){
		float elapsedTime = 0;
		while(elapsedTime < 1.0f){
			currentPos.anchoredPosition = Vector2.Lerp( huntBox_InPos, huntBox_OutPos, elapsedTime );
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

}
