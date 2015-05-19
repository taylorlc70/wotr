using UnityEngine;
using System.Collections;

public class Moveable {

	void Move(GameObject go, int amt){
		SpaceClass mySpace = go.transform.parent.GetComponent<SpaceClass>();
		mySpace.HighlightForMovement (amt);
	}
}
