using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//DiceFaceBase is the base class for the specific scripts for each dice face
// All it does is handle bringing up the the correct dice popup menu.

public class DiceFaceBase : MonoBehaviour {

	public GameObject popupPrefab;
	public GameObject popupInstance;

	public virtual void Awake(){
		popupPrefab = null;
	}
	
	public virtual void Start(){
		//Add listener to button for user UI - Calls BringUpMenu when this die is clicked
		gameObject.GetComponent<Button>().onClick.AddListener(() => BringUpMenu());
	}
	
	public void BringUpMenu(){
		if (popupInstance == null) {
			//Highlight this die?

			//Destroy any popups that other dice might have
			Destroy(GameObject.FindGameObjectWithTag("ActionDicePopupMenu"));
			//instantiate the popup for this die
			popupInstance = (Instantiate(popupPrefab, transform.position, transform.rotation) as GameObject);
			//Set the parent as the Main UI canvas
			popupInstance.transform.SetParent(GameObject.Find("Main UI").transform);
		} else {
			Destroy(popupInstance);
			popupInstance = null;
		}
	}

}

