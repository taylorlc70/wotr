using UnityEngine;
using System.Collections;

public class RegularUnit : MonoBehaviour {

	float speed = 15;

	Vector3 dest;
	Vector3 currentPos;

	void Start(){
		dest = GameObject.Find ("Orthanc").transform.position;
		currentPos = transform.position;
	}

	/*void Update(){
		currentPos = transform.position;
		Vector3 dir = (dest - currentPos).normalized * speed;
		gameObject.GetComponent<Rigidbody>().velocity = dir;
	}*/
}
