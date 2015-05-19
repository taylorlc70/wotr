using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecruitButton : MonoBehaviour {
	//This script is attached to all popup buttons that spawn units on a space. It will recruit whatever type of unit
	//is set to the string "unitForButton" (regular, leader or elite) based on the RecruitUnit function in the SpaceClass
	//of the selected space.

	public SpaceClass mySpaceClass;
	public string unitForButton;
	//unitForButton is set in the inspector!!!
	//The options for it are: regular, elite, leader
	//These are defined in the Army class script.


	void Start () {
		mySpaceClass = transform.parent.parent.GetComponent<SpaceClass>();
		gameObject.GetComponent<Button>().onClick.AddListener(()=> mySpaceClass.RecruitUnit(1, unitForButton));
	}

}
