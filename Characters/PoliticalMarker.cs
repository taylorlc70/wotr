using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PoliticalMarker : MonoBehaviour {

	//Political markers are highlighted for movement from the PlayerClass function HighlightPoliticalMarkers()

	public bool isActive;
	public bool atWar;
	public GameManager.Nation thisNation;
	public int currentStance;
	public GameObject[] politicalStances;

	// Use this for initialization
	void Start () {
		politicalStances = new GameObject[4] {
			GameObject.Find("Stage1"),
			GameObject.Find("Stage2"),
			GameObject.Find("Stage3"),
			GameObject.Find("AtWar")
		};

		transform.SetParent(politicalStances[currentStance].transform);
	}

	//MarkerClickedOn function removes all outlines and deactivates buttons for this player's markers
	public void MarkerClickedOn(){
		foreach(GameObject marker in (GameObject.FindGameObjectsWithTag(gameObject.tag))){
			Destroy(marker.GetComponent<Outline>());
			marker.GetComponent<Button>().enabled = false;
		}
	}

	public void AdvanceOne(){
			currentStance += 1;
			transform.SetParent(politicalStances [currentStance].transform);
		if (currentStance >= 3) {
			NationGoesToWar();
		}
	}
	

	public void SetActive(){
		//change image to active side image
		isActive = true;
		//gameObject.GetComponent<Button>().enabled = true;
	}

	public void NationGoesToWar(){
		Debug.Log("The nation of " + thisNation.ToString() + " has declared WAR!");

		gameObject.GetComponent<Button>().enabled = false;
		atWar = true;

		//Send all spaces of the nation to war!
		foreach (SettlementSpace settlement in (FindObjectsOfType<SettlementSpace>())) {
			if(settlement.nation == thisNation){
				settlement.atWar = true;
			}
		}
	}
}
