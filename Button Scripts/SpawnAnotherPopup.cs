using UnityEngine;
using System.Collections;

public class SpawnAnotherPopup : MonoBehaviour {

	public GameObject myPopup;

	public void OnClick(){
		Debug.Log("clicked");
		Vector2 centerPos = new Vector2(Screen.width / 2, Screen.height / 2);
		GameObject popup = Instantiate(myPopup, centerPos, transform.rotation) as GameObject;
		popup.transform.SetParent(GameObject.Find("Main UI").transform);
		Destroy(GameObject.FindGameObjectWithTag("ActionDicePopupMenu"));
	}
}
