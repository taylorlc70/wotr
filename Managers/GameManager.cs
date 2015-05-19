using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager: MonoBehaviour {

	//make this into a singleton
	private static GameManager instance;
	public static GameManager Instance{
		get{
			if (instance == null) {
				instance = GameObject.FindObjectOfType<GameManager>();
				DontDestroyOnLoad(instance.gameObject);
			} 
			return instance;
		}
	}
	//Put this here instead of in SpaceClass - makes more sense
	public enum Nation{
		None,
		Elves,
		Dwarves,
		Gondor,
		Rohan,
		Northmen,
		Sauron,
		Isengard,
		Easterlings
	}

	//These might need to be moved to Game instead, as they are specific to each game
	public static bool singlePlayerGame;
	public static bool multiplayerGame;
	public static bool gameRunning;
	//***** Set default value of userIfFP to true to playtest the FP player
	public static bool userIsFP;// = true;


	//Setting up the names of the regions and their adjacent spaces
	public static List<string> regionNamesList = new List<string>();
	public static Dictionary<string, List<string>> adjacencyMap;

	public static Dictionary<string, GameObject> theSpacesDictionary;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			if(this != instance){
				Destroy(instance);
			}
			instance = this;
		}
		GetAdjacencyMap();
	}

	void Start(){
		regionNamesList = GetRegionNamesList();
		Debug.Log("From GameManager: regionsNamesList.Count: " + GameManager.regionNamesList.Count);
		Debug.Log("From GameManager: adjacencyMap.Count: " + GameManager.adjacencyMap.Count);
	}

	private static Dictionary<string, List<string>> GetAdjacencyMap() {
				if (adjacencyMap == null) {
						adjacencyMap = new Dictionary<string, List<string>> ();
						adjacencyMap.Add("Andrast", new List<string>(){"Anfalas", "Druwaith Iaur"});
						adjacencyMap.Add("Anfalas", new List<string>(){"Andrast", "Erech", "Dol Amroth"});
						adjacencyMap.Add("Angmar", new List<string>(){"Arnor", "Ettenmoors", "Mount Gram"});
						adjacencyMap.Add("Arnor", new List<string>(){"Angmar", "Ettenmoors", "Evendim", "North Downs"});
						adjacencyMap.Add("Ash Mountains", new List<string>(){"Dagorlad", "South Rhun", "Southern Dorwinion"});
						adjacencyMap.Add("Barad-Dur", new List<string>(){"Gorgoroth"});
						adjacencyMap.Add("Bree", new List<string>(){"Buckland", "Ettenmoors","North Downs", "South Downs", "Weather Hills"});
						adjacencyMap.Add("Buckland", new List<string>(){"Bree", "Cardolan", "Evendim", "North Downs", "Old Forest", "South Downs", "The Shire"});
						adjacencyMap.Add("Cardolan", new List<string>(){"Buckland", "Minhiriath", "North Dunland", "Old Forest", "South Downs", "South Ered Luin", "Tharbad"});
						adjacencyMap.Add("Carrock", new List<string>(){"Eagle's Eyrie", "Northern Mirkwood", "Old Ford", "Old Forest Road", "Rhosgobel", "Northern Mirkwood"});
						adjacencyMap.Add("Dagorlad", new List<string>(){"Ash Mountains", "Eastern Emyn Muil", "Morannon", "Noman-Lands", "North Ithilien"});
						adjacencyMap.Add("Dale", new List<string>(){"Erebor", "Iron Hills", "Northern Rhovanion", "Old Forest Road", "Woodland Realm", "Vale of the Carnen", "Withered Heath"});
						adjacencyMap.Add("Dead Marshes", new List<string>(){"Druadan Forest", "Eastern Emyn Muil", "North Ithilien", "Osgiliath", "Western Emyn Muil"});
						adjacencyMap.Add("Dimrill Dale", new List<string>(){"Gladden Fields", "Lorien", "North Anduin Vale", "Parth Celebrant", "South Anduin Vale"});
						adjacencyMap.Add("Dol Amroth", new List<string>(){"Anfalas", "Erech", "Lamedon"});
						adjacencyMap.Add("Dol Guldur", new List<string>(){"Eastern Brown Lands", "Eastern Mirkwood", "Narrows of the Forest", "North Anduin Vale", "South Anduin Vale", "Southern Mirkwood", "Western Brown Lands"});
						adjacencyMap.Add("Druadan Forest", new List<string>(){"Dead Marshes", "Eastemnet", "Folde", "Minas Tirith", "Osgiliath", "Western Emyn Muil"});
						adjacencyMap.Add("Druwaith Iaur", new List<string>(){"Andrast", "Enedwaith", "Fords of Isen", "Gap of Rohan"});
						adjacencyMap.Add("Eagle's Eyrie", new List<string>(){"Carrock", "Old Ford", "Mount Gundabad"});
						adjacencyMap.Add("East Harondor", new List<string>(){"Near Harad", "South Ithilien", "West Harondor"});
						adjacencyMap.Add("East Rhun", new List<string>(){"Iron Hills", "North Rhun", "South Rhun", "Vale of the Carnen"});
						adjacencyMap.Add("Eastemnet", new List<string>(){"Druadan Forest", "Fangorn", "Folde", "Parth Celebrant", "Westemnet", "Western Brown Lands", "Western Emyn Muil"});
						adjacencyMap.Add("Eastern Brown Lands", new List<string>(){"Dol Guldur", "Eastern Emyn Muil", "Noman-Lands", "Southern Mirkwood", "Southern Rhovanion", "Western Brown Lands", "Western Emyn Muil"});
						adjacencyMap.Add("Eastern Emyn Muil", new List<string>(){"Dagorlad", "Dead Marshes","Eastern Brown Lands", "Noman-Lands", "North Ithilien", "Western Brown Lands", "Western Emyn Muil"});
						adjacencyMap.Add("Eastern Mirkwood", new List<string>(){"Dol Guldur", "Old Forest Road", "Narrows of the Forest", "Northern Rhovanion", "Southern Mirkwood"});
						adjacencyMap.Add("Edoras", new List<string>(){"Folde", "Westemnet"});
						adjacencyMap.Add("Enedwaith", new List<string>(){"Druwaith Iaur", "Gap of Rohan", "Minhiriath","South Dunland", "Tharbad"});
						adjacencyMap.Add("Erebor", new List<string>(){"Dale", "Iron Hills", "Withered Heath"});
						adjacencyMap.Add("Erech", new List<string>(){"Anfalas", "Dol Amroth", "Lamedon"});
						adjacencyMap.Add("Ered Luin", new List<string>(){"Evendim", "Grey Havens", "North Ered Luin", "Tower Hills"});
						adjacencyMap.Add("Ettenmoors", new List<string>(){"Angmar", "Arnor", "Mount Gram", "North Downs", "Trollshaws", "Weather Hills"});
						adjacencyMap.Add("Evendim", new List<string>(){"Arnor", "Buckland", "Ered Luin", "North Downs", "North Ered Luin", "The Shire", "Tower Hills"});
						adjacencyMap.Add("Fangorn", new List<string>(){"Eastemnet","Fords of Isen","Parth Celebrant", "Westemnet"});
						adjacencyMap.Add("Far Harad", new List<string>(){"Khand","Near Harad"});
						adjacencyMap.Add("Folde", new List<string>(){"Druadan Forest", "Eastemnet", "Edoras", "Westemnet"});
						adjacencyMap.Add("Fords of Bruinen", new List<string>(){"High Pass","Hollin","Rivendell","Trollshaws"});
						adjacencyMap.Add("Fords of Isen", new List<string>(){"Druwaith Iaur", "Gap of Rohan", "Helm's Deep", "Orthanc", "Westemnet"});
						adjacencyMap.Add("Forlindon", new List<string>(){"Grey Havens"});
						adjacencyMap.Add("Gap of Rohan", new List<string>(){"Druwaith Iaur", "Enedwaith", "Fords of Isen", "Orthanc", "South Dunland"});
						adjacencyMap.Add("Gladden Fields", new List<string>(){"Dimrill Dale", "North Anduin Vale", "Old Ford", "Rhosgobel"});
						adjacencyMap.Add("Grey Havens", new List<string>(){"Ered Luin","Forlindon","Harlindon","Tower Hills"});
						adjacencyMap.Add("Goblin's Gate", new List<string>(){"High Pass", "Old Ford"});
						adjacencyMap.Add("Gorgoroth", new List<string>(){"Barad-Dur", "Minas Morgul", "Morannon", "Nurn"});
						adjacencyMap.Add("Harlindon", new List<string>(){"Grey Havens","South Ered Luin"});
						adjacencyMap.Add("Helm's Deep", new List<string>(){"Fords of Isen","Westemnet"});
						adjacencyMap.Add("High Pass", new List<string>(){"Fords of Bruinen", "Goblin's Gate"});
						adjacencyMap.Add("Hollin", new List<string>(){"Fords of Bruinen", "Moria", "North Dunland", "South Downs", "Trollshaws"});
						adjacencyMap.Add("Iron Hills", new List<string>(){"Dale", "East Rhun", "Erebor", "Vale of the Carnen"});
						adjacencyMap.Add("Khand", new List<string>(){"Far Harad","Near Harad"});
						adjacencyMap.Add("Lamedon", new List<string>(){"Dol Amroth","Erech","Pelargir"});
						adjacencyMap.Add("Lorien", new List<string>(){"Dimrill Dale", "Parth Celebrant"});
						adjacencyMap.Add("Lossarnach", new List<string>(){"Minas Tirith", "Osgiliath", "Pelargir"});
						adjacencyMap.Add("Minas Tirith", new List<string>(){"Druadan Forest", "Lossarnach", "Osgiliath"});
						adjacencyMap.Add("Minas Morgul", new List<string>(){"Gorgoroth", "North Ithilien", "South Ithilien"});
						adjacencyMap.Add("Minhiriath", new List<string>(){"Cardolan", "Enedwaith", "South Ered Luin", "Tharbad"});
						adjacencyMap.Add("Morannon", new List<string>(){"Dagorlad", "Gorgoroth"});
						adjacencyMap.Add("Moria", new List<string>(){"Dimrill Dale", "Hollin", "North Dunland"});
						adjacencyMap.Add("Mount Gram", new List<string>(){"Angmar", "Ettenmoors", "Mount Gundabad"});
						adjacencyMap.Add("Mount Gundabad", new List<string>(){"Eagle's Eyrie", "Mount Gram"});
						adjacencyMap.Add("Narrows of the Forest", new List<string>(){"Dol Guldur", "Eastern Mirkwood", "Old Forest Road", "North Anduin Vale", "Rhosgobel"});
						adjacencyMap.Add("Near Harad", new List<string>(){"East Harondor", "Far Harad", "Khand", "Umbar", "West Harondor"});
						adjacencyMap.Add("Noman-Lands", new List<string>(){"Ash Mountains","Dagorlad", "Eastern Brown Lands", "Eastern Emyn Muil", "Southern Dorwinion", "Southern Rhovanion"});
						adjacencyMap.Add("North Anduin Vale", new List<string>(){"Dimrill Dale", "Dol Guldur", "Gladden Fields","Narrows of the Forest", "Rhosgobel", "South Anduin Vale"});
						adjacencyMap.Add("North Downs", new List<string>(){"Arnor", "Bree", "Buckland", "Ettenmoors", "Evendim", "Weather Hills"});
						adjacencyMap.Add("North Dunland", new List<string>(){"Cardolan", "Hollin", "Moria", "South Downs", "South Dunland", "Tharbad"});
						adjacencyMap.Add("North Ered Luin", new List<string>(){"Ered Luin","Evendim"});
						adjacencyMap.Add("North Ithilien", new List<string>(){"Dagorlad", "Dead Marshes", "Eastern Emyn Muil", "Morannon", "Osgiliath", "South Ithilien"});
						adjacencyMap.Add("North Rhun", new List<string>(){"East Rhun", "Northern Dorwinion", "Vale of the Carnen", "Vale of the Celduin"});
						adjacencyMap.Add("Northern Dorwinion", new List<string>(){"North Rhun", "Southern Dorwinion", "Southern Rhovanion", "Vale of the Celduin"});
						adjacencyMap.Add("Northern Mirkwood", new List<string>(){"Carrock","Withered Heath", "Western Mirkwood", "Woodland Realm"});
						adjacencyMap.Add("Northern Rhovanion", new List<string>(){"Dale", "Eastern Mirkwood", "Old Forest Road", "Southern Mirkwood", "Southern Rhovanion", "Vale of the Carnen", "Vale of the Celduin"});
						adjacencyMap.Add("Nurn", new List<string>(){"Gorgoroth"});
						adjacencyMap.Add("Old Ford", new List<string>(){"Carrock", "Eagle's Eyrie", "Gladden Fields", "Goblin's Gate", "Rhosgobel"});
						adjacencyMap.Add("Old Forest", new List<string>(){"Buckland", "Cardolan", "South Ered Luin", "The Shire"});
						adjacencyMap.Add("Old Forest Road", new List<string>(){"Carrock","Dale", "Eastern Mirkwood", "Narrows of the Forest", "Northern Rhovanion","Rhosgobel", "Western Mirkwood", "Woodland Realm"});
						adjacencyMap.Add("Orthanc", new List<string>(){"Fords of Isen","Gap of Rohan"});
						adjacencyMap.Add("Osgiliath", new List<string>(){"Dead Marshes", "Druadan Forest", "Lossarnach", "Minas Tirith", "North Ithilien", "Pelargir", "South Ithilien", "West Harondor"});
						adjacencyMap.Add("Parth Celebrant", new List<string>(){"Dimrill Dale", "Eastemnet", "Fangorn", "Lorien", "South Anduin Vale", "Western Brown Lands"});
						adjacencyMap.Add("Pelargir", new List<string>(){"Lamedon", "Lossarnach", "Osgiliath", "West Harondor"});
						adjacencyMap.Add("Rhosgobel", new List<string>(){"Carrock", "Gladden Fields", "Old Ford", "Old Forest Road", "Narrows of the Forest", "North Anduin Vale"});
						adjacencyMap.Add("Rivendell", new List<string>(){"Fords of Bruinen", "Trollshaws"});
						adjacencyMap.Add("South Anduin Vale", new List<string>(){"Dimrill Dale", "Dol Guldur", "North Anduin Vale", "Parth Celebrant", "Western Brown Lands"});
						adjacencyMap.Add("South Downs", new List<string>(){"Bree","Buckland","Cardolan","Hollin","North Dunland","Trollshaws","Weather Hills"});
						adjacencyMap.Add("South Dunland", new List<string>(){"Enedwaith", "Gap of Rohan", "North Dunland", "Tharbad"});
						adjacencyMap.Add("South Ered Luin", new List<string>(){"Cardolan", "Harlindon", "Minhiriath", "Old Forest", "The Shire", "Tower Hills"});
						adjacencyMap.Add("South Ithilien", new List<string>(){"East Harondor", "Osgiliath", "North Ithilien", "West Harondor"});
						adjacencyMap.Add("South Rhun", new List<string>(){"Ash Mountains", "East Rhun", "Southern Dorwinion"});
						adjacencyMap.Add("Southern Dorwinion", new List<string>(){"Ash Mountains", "Noman-Lands", "Northern Dorwinion", "South Rhun", "Southern Rhovanion"});
						adjacencyMap.Add("Southern Mirkwood", new List<string>(){"Dol Guldur", "Eastern Brown Lands", "Eastern Mirkwood", "Northern Rhovanion", "Southern Rhovanion", "Western Brown Lands"});
						adjacencyMap.Add("Southern Rhovanion", new List<string>(){"Eastern Brown Lands", "Noman-Lands", "Northern Dorwinion", "Northern Rhovanion", "Southern Dorwinion", "Southern Mirkwood", "Vale of the Celduin"});
						adjacencyMap.Add("Tharbad", new List<string>(){"Cardolan", "Enedwaith", "Minhiriath", "North Dunland", "South Dunland"});
						adjacencyMap.Add("The Shire", new List<string>(){"Buckland", "Evendim", "Old Forest", "South Ered Luin", "Tower Hills"});
						adjacencyMap.Add("Tower Hills", new List<string>(){"Ered Luin", "Evendim", "Grey Havens", "South Ered Luin", "The Shire"});
						adjacencyMap.Add("Trollshaws", new List<string>(){"Ettenmoors", "Fords of Bruinen", "Hollin", "Rivendell", "South Downs", "Weather Hills"});
						adjacencyMap.Add("Umbar", new List<string>(){"West Harondor", "Near Harad"});
						adjacencyMap.Add("Vale of the Carnen", new List<string>(){"Dale", "East Rhun", "Iron Hills", "North Rhun", "Northern Rhovanion", "Vale of the Celduin"});
						adjacencyMap.Add("Vale of the Celduin", new List<string>(){"North Rhun", "Northern Dorwinion", "Northern Rhovanion", "Southern Rhovanion", "Vale of the Carnen"});
						adjacencyMap.Add("Weather Hills", new List<string>(){"Bree", "Ettenmoors", "South Downs", "Trollshaws"});
						adjacencyMap.Add("West Harondor", new List<string>(){"East Harondor", "Near Harad", "Osgiliath", "Pelargir", "South Ithilien", "Umbar"});
						adjacencyMap.Add("Westemnet", new List<string>(){"Eastemnet", "Edoras", "Fangorn", "Folde", "Fords of Isen", "Helm's Deep"});
						adjacencyMap.Add("Western Brown Lands", new List<string>(){"Dol Guldur", "Eastemnet", "Eastern Brown Lands", "Parth Celebrant", "South Anduin Vale", "Western Emyn Muil"});
						adjacencyMap.Add("Western Emyn Muil", new List<string>(){"Dead Marshes", "Druadan Forest", "Eastemnet", "Eastern Brown Lands", "Eastern Emyn Muil", "Western Brown Lands"});
						adjacencyMap.Add("Western Mirkwood", new List<string>(){"Carrock", "Old Forest Road", "Northern Mirkwood", "Woodland Realm"});
						adjacencyMap.Add("Withered Heath", new List<string>(){"Dale", "Erebor", "Northern Mirkwood", "Woodland Realm"});
						adjacencyMap.Add("Woodland Realm", new List<string>(){"Dale", "Old Forest Road", "Northern Mirkwood", "Western Mirkwood", "Withered Heath"});
				}
		return adjacencyMap;
	}

	//Make the list of all the region names
	public static List<string> GetRegionNamesList(){
		if (adjacencyMap == null) {
			adjacencyMap = GetAdjacencyMap();
		}
		if (regionNamesList.Count < 1) {
			foreach (string regionName in adjacencyMap.Keys) {
				regionNamesList.Add(regionName);
			}
		}
		return regionNamesList;
	}

	//Method for spaces to get their adjacent spaces
	public static List<string> GetAdjacentSpaces(string name){
		List<string> result;
		if (adjacencyMap.TryGetValue(name, out result)){
			return result;
		}
		else{
			return null;
		}
	}
	

	public static void EndGame(){
		gameRunning = false;
		singlePlayerGame = false;
		multiplayerGame = false;
	}
}
