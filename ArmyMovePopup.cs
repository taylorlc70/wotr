using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArmyMovePopup : MonoBehaviour {

	private int startRegCount;
	private int startEliteCount;
	private int startLeaderCount;
	private string startRegionName;
	private int endRegCount;
	private int endEliteCount;
	private int endLeaderCount;
	private string endRegionName;

	private GameObject startArmy;
	private GameObject endArmy;
	
	private int startArmyCount;
	private int endArmyCount;

	private int init_startRegs;
	private int init_startElites;
	private int init_startLeaders;

	private int init_endRegs;
	private int init_endElites;
	private int init_endLeaders;

	public const int MAX_ARMY = 10;

	public void Init_ArmyMovePopup(GameObject moveStartSpace, GameObject moveEndSpace){
		startRegionName = moveStartSpace.name;
		endRegionName = moveEndSpace.name;
		if (moveStartSpace.GetComponentInChildren<Army>() != null) {
			startArmy = moveStartSpace.transform.Find("ArmyPrefab(Clone)").gameObject;
			startRegCount = startArmy.GetComponent<Army>().numRegs;
			startEliteCount = startArmy.GetComponent<Army>().numElites;
			startLeaderCount = startArmy.GetComponent<Army>().numLeaders;
		}
		if (moveEndSpace.GetComponentInChildren<Army>() != null) {
			endArmy = moveEndSpace.transform.Find("ArmyPrefab(Clone)").gameObject;
			endRegCount = endArmy.GetComponent<Army>().numRegs;
			endEliteCount = endArmy.GetComponent<Army>().numElites;
			endLeaderCount = endArmy.GetComponent<Army>().numLeaders;
		}

		init_startRegs = startRegCount;
		init_startElites = startEliteCount;
		init_startLeaders = startLeaderCount;

		init_endRegs = endRegCount;
		init_endElites = endEliteCount;
		init_endLeaders = endLeaderCount;

		UpdateValues();

		//Remember to set thumbnails too
		//Set region names
		transform.Find("StartRegionPanel").Find("StartRegionName").GetComponent<Text>().text = startRegionName;
		transform.Find("EndRegionPanel").Find("EndRegionName").GetComponent<Text>().text = endRegionName;
	}

	private void UpdateValues(){
		//Start Space Values - Update the text in the panel
		transform.Find("StartRegionPanel").Find("RegCountBox").Find("StartRegCount").GetComponent<Text>().text = startRegCount.ToString();
		transform.Find("StartRegionPanel").Find("LeaderCountBox").Find("StartLeaderCount").GetComponent<Text>().text = startLeaderCount.ToString();
		transform.Find("StartRegionPanel").Find("EliteCountBox").Find("StartEliteCount").GetComponent<Text>().text = startEliteCount.ToString();
		//End Space Values - Update the text in the panel
		transform.Find("EndRegionPanel").Find("LeaderCountBox").Find("EndLeaderCount").GetComponent<Text>().text = endLeaderCount.ToString();
		transform.Find("EndRegionPanel").Find("RegCountBox").Find("EndRegCount").GetComponent<Text>().text = endRegCount.ToString();
		transform.Find("EndRegionPanel").Find("EliteCountBox").Find("EndEliteCount").GetComponent<Text>().text = endEliteCount.ToString();

		//Update the army counts
		startArmyCount = startRegCount + startEliteCount;
		endArmyCount = endRegCount + endEliteCount;

		//Disable buttons if need be
		if(startRegCount <= 0 || endArmyCount == MAX_ARMY) {
			transform.Find("AddRegButton").GetComponent<Button>().enabled = false;
		}
		if(startEliteCount <= 0 || endArmyCount == MAX_ARMY) {
			transform.Find("AddEliteButton").GetComponent<Button>().enabled = false;
		}
		if(startLeaderCount <= 0) /*Leaders do not affect army count*/ {
			transform.Find("AddLeaderButton").GetComponent<Button>().enabled = false;
		}
		if (endRegCount == init_endRegs) {
			transform.Find("RemRegButton").GetComponent<Button>().enabled = false;
		}
		if (endEliteCount == init_endElites) {
			transform.Find("RemEliteButton").GetComponent<Button>().enabled = false;
		}
		if (endLeaderCount == init_endLeaders) {
			transform.Find("RemLeaderButton").GetComponent<Button>().enabled = false;
		}
	}
	

	public void AddReg(){
		if (startRegCount > 0 && endArmyCount < MAX_ARMY) {
			startRegCount -= 1;
			endRegCount += 1;
		} 
		UpdateValues();
	}

	public void RemReg(){

	}

	public void AddElite(){
		if (startEliteCount > 0 && endArmyCount < MAX_ARMY) {
			startEliteCount -= 1;
			endEliteCount += 1;
		} else {
			transform.Find("AddEliteButton").GetComponent<Button>().enabled = false;
		}
		UpdateValues();
	}
	
	public void RemElite(){

	}

	public void AddLeader(){
		if (startLeaderCount > 0 && endArmyCount < MAX_ARMY) {
			startLeaderCount -= 1;
			endLeaderCount += 1;
		} else {
			transform.Find("AddLeaderButton").GetComponent<Button>().enabled = false;
		}
		UpdateValues();
	}
	
	public void RemLeader(){
		
	}

	public void MoveAll(){
		int m_regs = startRegCount;
		int m_elites = startEliteCount;
		//First move all regulars
		for (int i = 0; i < m_regs; i++) {
			if (endArmyCount < MAX_ARMY) {
				AddReg();
			}
		}
		//Then move all elites
		for (int i = 0; i < m_elites; i++) {
			if(endArmyCount < MAX_ARMY){
				AddElite();
			}
		}
		//Finally move all leaders. They don't affect army counts.
		endLeaderCount += startLeaderCount;
		startLeaderCount = 0;

		//Update the popup window
		UpdateValues();
	}

	public void ConfirmMove(){
		//Remove appropriate amount of units from stat army
		startArmy.GetComponent<Army>().RemoveUnits(init_startRegs - startRegCount, init_startElites - startEliteCount, init_startLeaders - startLeaderCount);
		//Add appropriate units to end army
		//endArmy.GetComponent<Army>().AddUnits
	}
}
