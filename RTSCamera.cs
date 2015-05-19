using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour {

	private float mousePosX;
	private float mousePosY;
	private int ScrollAmount = 5;
	private int scrollDistance = 8;
	private int ZoomAmount = 10;
	private int maxZoomHeight = 500, minZoomHeight = 100, leftBoundary = 375, rightBoundary = 2000, bottomBoundary = -10, topBoundary = 850;
	
	// Update is called once per frame
	void Update () {
		mousePosX = Input.mousePosition.x;
		mousePosY = Input.mousePosition.y;
		//Constraints
		if(transform.position.x < leftBoundary){
			transform.position = new Vector3(leftBoundary, transform.position.y, transform.position.z);
		}
		if(transform.position.x > rightBoundary){
			transform.position = new Vector3(rightBoundary, transform.position.y, transform.position.z);
		}
		if(transform.position.z < bottomBoundary){
			transform.position = new Vector3(transform.position.x, transform.position.y, bottomBoundary);
		}
		if(transform.position.z > topBoundary){
			transform.position = new Vector3(transform.position.x, transform.position.y, topBoundary);
		}
		//Zooming parameters
		if(transform.position.y > maxZoomHeight){
			transform.position = new Vector3(transform.position.x, maxZoomHeight, transform.position.z);
		}
		if(transform.position.y < minZoomHeight){
			transform.position = new Vector3(transform.position.x, minZoomHeight, transform.position.z);
		}

		// right
		if ((mousePosX >= Screen.width - scrollDistance) || (Input.GetKey(KeyCode.RightArrow))){
			transform.Translate (ScrollAmount,0,0, Space.World);
		}
		// left
		if ((mousePosX <= scrollDistance) || (Input.GetKey(KeyCode.LeftArrow))) {
			transform.Translate (-ScrollAmount,0,0, Space.World);
		}
		// up
		if ((mousePosY >= Screen.height - scrollDistance) || (Input.GetKey(KeyCode.UpArrow))){
			transform.Translate (0,0,ScrollAmount, Space.World);
		}
		// down
		if ((mousePosY <= scrollDistance) || (Input.GetKey(KeyCode.DownArrow))){
			transform.Translate (0,0,-ScrollAmount, Space.World);
		}

		// Zoom in and Out

		if (Input.GetAxis("Mouse ScrollWheel") < 0){ //Going up
			transform.Translate (0,0,-ZoomAmount, Space.Self);
			if(transform.rotation.x < 0.5f){
				transform.Rotate(Vector3.right, Space.World);
			}
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0){ //Going down
			transform.Translate (0,0,ZoomAmount, Space.Self);
			if(transform.rotation.x > 0.33f && transform.position.y < 250){
				transform.Rotate(Vector3.left, Space.World);
			}
		}


	}

}

