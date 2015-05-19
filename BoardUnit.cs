/* War of the Ring board game
 * Taylor Coleman
 * March 6, 2015
 * New attempt at managing units on the board. The goal is that all board pieces will inherit from this class,
 * so all common functionality will be encapsulated here and additional functionality can be added with additional scripts.
 * All board units will be children of empty Army game objects.
 * */
using UnityEngine;
using System.Collections;

public class BoardUnit : MonoBehaviour {

	public SpaceClass myLocation;

	//These attributes are set in the inspector according to the prefab object used
	public enum Side{
		Unassigned,
		Free,
		Shadow
	}
	public Side side;
	public GameManager.Nation nation;

	//Event that is triggered when unit is spawned
	public delegate void UnitSpawn();
	public static event UnitSpawn BoardUnitSpawned;

	void Start(){
		if (BoardUnitSpawned != null) {
			BoardUnitSpawned();		
		}
	}

	public void Move(int amt){

	}
}
