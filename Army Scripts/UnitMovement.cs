using UnityEngine;
using System.Collections;
/*
	General script for all unit movement - designed for groups of units.
	All units have a single parent object that they are attached to.
	If the parent object moves, they follow in a group.
 */
public class UnitMovement : MonoBehaviour {
	//TODO: implement state machine
	private enum UnitState{
		Waiting,
		Moving
	}

	private Transform parent;

	void Awake(){
		if(transform.parent.GetComponent<Transform>().position == null){
			Debug.LogError("Error: UnitMovement requires gameObject to have parent object with transform position");
		} else{
			parent = transform.parent.GetComponent<Transform>();
		}
	}

	void Update(){
		float distance = Vector3.Distance(this.transform.position, parent.position);
		if(distance > 100){
			//Move towards parent object
		}
	}
}
