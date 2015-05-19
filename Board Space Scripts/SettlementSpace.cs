using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettlementSpace : New_SpaceClass {

	//NOTE: Moved the atWar bool to New_SpaceClass
	public enum ControlledBy{
		Free,
		Shadow
	}
	public ControlledBy controlledBy;

	public bool fortress;

	public GameObject myPlayer;

	public override void Awake(){
		base.Awake();
		//Change myInfoPanel to the Settlement popup prefab instead
		myInfoPanel = Resources.Load("UI_Prefabs/WorldSpaceUI/SettlementSpaceInfo") as GameObject;

		if (nation == GameManager.Nation.Dwarves || nation == GameManager.Nation.Elves || nation == GameManager.Nation.Gondor || nation == GameManager.Nation.Rohan || nation == GameManager.Nation.Northmen) {
			gameObject.tag = "FP_Settlement";
			controlledBy = ControlledBy.Free;
		} else {
			gameObject.tag = "SH_Settlement";
			controlledBy = ControlledBy.Shadow;
		}
	}

	public override void Start(){
		base.Start(); //Gets adjacent spaces, add spaceclick event button listener
		if (controlledBy == ControlledBy.Free) {
			if (GameManager.userIsFP) {
				myPlayer = GameObject.Find("FP_UserPlayer");
			} else {
				myPlayer = GameObject.Find("FP_AIPlayer");
			}
		} else {
			if(GameManager.userIsFP){
				myPlayer = GameObject.Find("Shadow_AIPlayer");
			} else{
				myPlayer = GameObject.Find("Shadow_UserPlayer");
			}
		}
	}

	public override void BringUpInfoPanel(){
		GameObject infoPanel;
		//Destroy previous info windows
		GameObject[] other = GameObject.FindGameObjectsWithTag("InfoWindow");
		if(other.Length > 0){
			foreach(GameObject go in other){
				Destroy(go);
			}
		}

		infoPanel = Instantiate(myInfoPanel) as GameObject;
		infoPanel.transform.SetParent(gameObject.transform.parent);
		infoPanel.transform.position = gameObject.transform.position;
		infoPanel.transform.Find("RegionName").GetComponent<Text>().text = regionName;
		infoPanel.transform.Find("Nation").GetComponent<Text>().text = nation.ToString();
		//Also set custom image and text here
		infoPanel.transform.Find("ControllingSide").GetComponent<Text>().text = controlledBy.ToString();
		if (atWar) {
			infoPanel.transform.Find("AtWar").GetComponent<Toggle>().isOn = true;
		} else {
			infoPanel.transform.Find("AtWar").GetComponent<Toggle>().isOn = false;
		}
		if(fortress){
			infoPanel.transform.Find("VPtext").GetComponent<Text>().text = "Worth 2 Victory Points";
		}else{
			infoPanel.transform.Find("VPtext").GetComponent<Text>().text = "Worth 1 Victory Point";
		}
	}
}
