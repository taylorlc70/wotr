using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{
	
	public Camera SourceCamera;

	void Start(){
		SourceCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	void Update()
	{
		if (SourceCamera != null)
		{
			transform.rotation = SourceCamera.transform.rotation;
		}
	}
}
