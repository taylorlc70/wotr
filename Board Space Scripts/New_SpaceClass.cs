using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class New_SpaceClass : MonoBehaviour {

	public string regionName;
	public GameManager.Nation nation;
	public GameObject myInfoPanel;
	public List<New_SpaceClass> adjacentSpaces;
	public bool highlighted = false;

	public delegate void SpaceClicked();
	public event SpaceClicked SpaceClickEvent;

	public GameObject armyPrefab;

	private Button btn;
	private ColorBlock cb;

	public bool atWar = true;

	//---------------------------------------FUNCTIONS----------------------------------------------

	public virtual void Awake () { //Virtual for different space types
		regionName = gameObject.name;
		myInfoPanel = Resources.Load("UI_Prefabs/WorldSpaceUI/NormalSpaceInfo") as GameObject; //Load correct info panel
		armyPrefab = Resources.Load("Prefabs/Army/ArmyPrefab") as GameObject;
		btn = gameObject.GetComponent<Button>();
		cb = btn.colors;
	}
	
	public virtual void Start(){
		foreach (string x in GameManager.GetAdjacentSpaces(regionName)) {
			adjacentSpaces.Add(GameObject.Find(x).GetComponent<New_SpaceClass>());
		}
		//Add the SpaceButtonClick function to the click event on the Button component
		gameObject.GetComponent<Button> ().onClick.AddListener(() => {SpaceButtonClick (); });
		SpaceClickEvent += BringUpInfoPanel; //Default event for clicks
	}


	//Call the SpaceClickEvent when this button is clicked (SpaceButtonClick should be called when button is clicked)
	public void SpaceButtonClick(){
		if (SpaceClickEvent != null) {
			SpaceClickEvent();
		}
	}

	public void HighlightSpace(){
		//Add a function to SpaceClickEvent based on the highlight state ... Will there be "highlight states"?
		highlighted = true;
		cb.normalColor = Color.white;
		btn.colors = cb;
		SpaceClickEvent += SpaceHlClick;
		SpaceClickEvent -= BringUpInfoPanel;
	}

	public void UnhighlightSpace(){
		highlighted = false;
		SpaceClickEvent -= SpaceHlClick;
		SpaceClickEvent += BringUpInfoPanel;
	}

	private void SpaceHlClick (){
		Debug.Log ("Highlighted space "+gameObject.name+" has been clicked");
	}

	public virtual void BringUpInfoPanel(){
		//for debugging ----------------------
		foreach(New_SpaceClass space in adjacentSpaces){
			Debug.DrawRay(gameObject.transform.position, GameObject.Find(space.regionName).transform.position - gameObject.transform.position, Color.red, 2.0f);
		}

		GameObject infoPanel;
		//Destroy previous info windows
		GameObject[] other = GameObject.FindGameObjectsWithTag("InfoWindow");
		if(other.Length > 0){
			foreach(GameObject go in other){
				Destroy(go);
			}
		}
		infoPanel = Instantiate(myInfoPanel) as GameObject;
		infoPanel.transform.SetParent(gameObject.transform.parent); //Set space UI canvas as parent
		infoPanel.transform.position = gameObject.transform.position;
		infoPanel.transform.Find("RegionName").GetComponent<Text>().text = regionName;
		infoPanel.transform.Find("Nation").GetComponent<Text>().text = nation.ToString();
		//Also set custom image and text here
	}

	public void StartMoveOptions(int counter){
		if (counter == 0) {
			return;
		} else {
			counter -= 1;
			foreach(New_SpaceClass spc in adjacentSpaces){
				if(!spc.highlighted){
					spc.HighlightSpace();
				}
				spc.StartMoveOptions(counter);
			}
		}
	}

	public void RecruitUnit(uint amt, GameManager.Nation nation, string rank){
		Army army = null;
		Army.Side side = Army.Side.Unassigned;
		if (GetComponentInChildren<Army>() != null) {
			army = GetComponentInChildren<Army>();
			army.AddUnits(amt, nation, rank);
		} else {
			Debug.Log (regionName + " is making a new Army");
			GameObject newArmy = Instantiate(armyPrefab) as GameObject;
			newArmy.transform.parent = transform;
			if (nation == GameManager.Nation.Easterlings || nation == GameManager.Nation.Isengard || nation == GameManager.Nation.Sauron) {
				side = Army.Side.Shadow;
			}
			if (nation == GameManager.Nation.Dwarves || nation == GameManager.Nation.Gondor || nation == GameManager.Nation.Elves || nation == GameManager.Nation.Rohan || nation == GameManager.Nation.Northmen) {
				side = Army.Side.Free;
			}
			newArmy.GetComponent<Army>().InitArmy(side);
			newArmy.GetComponent<Army>().AddUnits(amt, nation, rank);
		}
	}
}
