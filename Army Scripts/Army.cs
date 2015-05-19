using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Army : MonoBehaviour {

	public enum Side{
		Unassigned,
		Free,
		Shadow
	}
	public Side side;

	public enum Rank{
		Regular,
		Elite,
		Prefab
	}
	public Rank rank;

	public string location;
	public int combatStrength;
	public int leadership;
	public int numElites;
	public int numRegs;
	public int numLeaders;
	//public Dictionary<string, bool> nations;
	public List<string> nations;
	
	//*****************Functions Begin *******************************************

	void Start(){
		location = gameObject.GetComponentInParent<New_SpaceClass>().regionName;
		transform.position = transform.parent.transform.position;
	}

	public void InitArmy(Side s){
		this.side = s;
		combatStrength = CalculateCombatStrength();
		leadership = CalculateLeadership();
		GetNations();
	}
	
	public int CalculateCombatStrength(){
		numRegs = GetComponentsInChildren<RegularUnit>().Length;
		numElites = GetComponentsInChildren<EliteUnit>().Length;
		combatStrength = numElites + numRegs;
		if (combatStrength > 5) {
			combatStrength = 5;
		}
		return combatStrength;
	}

	public int CalculateLeadership(){
		numLeaders = GetComponentsInChildren<LeaderUnit>().Length;
		leadership = numLeaders /* +plus leadership of characters involved */;
		return leadership;
	}

	public void GetNations(){
		if(transform.childCount == 0){
			return;
		}
		foreach(Transform child in transform){
			switch(child.tag){
			case "DwarfUnit":
				if(!nations.Contains("Dwarves")){
					nations.Add("Dwarves");
				}
				break;
			case "ElfUnit":
				if(!nations.Contains("Elves")){
					nations.Add("Elves");
				}
				break;
			case "GondorUnit":
				if(!nations.Contains("Gondor")){
					nations.Add("Gondor");
				}
				break;
			case "RohanUnit":
				if(!nations.Contains("Rohan")){
					nations.Add("Rohan");
				}
				break;
			case "NorthmanUnit":
				if(!nations.Contains("Northmen")){
					nations.Add("Northmen");
				}
				break;
			case "SauronUnit":
				if(!nations.Contains("Sauron")){
					nations.Add("Sauron");
				}
				break;
			case "IsengardUnit":
				if(!nations.Contains("Isengard")){
					nations.Add("Isengard");
				}
				break;
			case "EasterlingUnit":
				if(!nations.Contains("Easterlings")){
					nations.Add("Easterlings");
				}
				break;
			}
		}
	}
	
	//******************************* MOVE FUNCTION *****************************************************
	//***************************************************************************************************
	public void Move(Transform dest){
		gameObject.transform.parent = dest;
		transform.position = transform.parent.transform.position;
		location = gameObject.GetComponentInParent<New_SpaceClass>().regionName;
	}
	
	// ****************************** ADD UNITS FUNCTION ***********************************************
	// *************************************************************************************************

	private void createUnit(GameManager.Nation race, string rank){
		string prefabName = race.ToString() + rank.ToString();
		if(rank == "Leader"){
			if(race == GameManager.Nation.Easterlings || race == GameManager.Nation.Isengard || race == GameManager.Nation.Sauron ){
				prefabName = "Nazgul";
			}
		}
		GameObject prefab = Resources.Load ("Prefabs/Army/" + prefabName) as GameObject;
		Debug.Log ("Making a " + prefabName + " in " + transform.parent.name);
		(Instantiate (prefab, transform.position, transform.rotation) as GameObject).transform.parent = gameObject.transform;
	}

	public void AddUnits(uint amount, GameManager.Nation race, string rank){
		if (race == GameManager.Nation.None) {
			Debug.Log("Must choose a nation for units."); return;
		}
		if (side == Side.Free) {
			if (race == GameManager.Nation.Easterlings || race == GameManager.Nation.Isengard || race == GameManager.Nation.Sauron) {
				Debug.Log("This army cannot recruit units from the opposite team!"); return;
			}
		} else if (side == Side.Shadow) {
			if (race == GameManager.Nation.Dwarves || race == GameManager.Nation.Gondor || race == GameManager.Nation.Elves || race == GameManager.Nation.Rohan || race == GameManager.Nation.Northmen) {
				Debug.Log("This army cannot recruit units from the opposite team!"); return;
			}
		}
		for(int i = 0; i < amount; i++){
			createUnit(race, rank);
		}

		CalculateCombatStrength();
		CalculateLeadership();
		GetNations();
	}

	//********************************************************************************************************************
	//************************* REMOVE UNITS FUNCTION ********************************************************************

	public void RemoveUnits(int regs, int elites, int leaders){
		//Make sure we aren't removing too many of any unit
		if (numRegs <= regs) {	
			regs = numRegs;
		}
		if (numElites <= elites) {
			elites = numElites;
		}
		if (numLeaders <= leaders) {
			leaders = numLeaders;
		}

		//Remove the appropriate units from this army
		for (int i = 0; i < regs; i++) {
			Destroy(transform.GetComponentInChildren<RegularUnit>().gameObject);
		}
		for (int i = 0; i < elites; i++) {
			Destroy(transform.GetComponentInChildren<EliteUnit>().gameObject);
		}
		for (int i = 0; i < leaders; i++) {
			Destroy(transform.GetComponentInChildren<LeaderUnit>().gameObject);
		}

		//destry army object if empty
		if (transform.childCount <= 0) {
			Destroy(gameObject);
		}
		CalculateCombatStrength();
		CalculateLeadership();
		GetNations();
		}
	}
