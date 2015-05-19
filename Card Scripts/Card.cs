using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Written by Taylor, Bargus the Great
// Started Oct. 2014

public class Card : MonoBehaviour{
	public enum CardType{
		Character,
		Army
	}
	public bool playsOnTable;
	public uint combatPriority;

	public void PlayAsEvent(){
		
	}
	
	public void PlayAsCombat(){
		//wait for combat to start
		//if other card, compare to other card's combat priority
		//	if lower, go. If higher, wait
	}
}