using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
	public float fadeDuration = 3.0f;

	void Awake ()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	private void Start ()
	{
		StartCoroutine(FadeIn());
	}
	
	private IEnumerator FadeIn()
	{
		yield return StartCoroutine(Fade(1.0f, 0.0f, fadeDuration));
	}

	private IEnumerator FadeOut(){
		yield return StartCoroutine(Fade(1.0f, 0.0f, fadeDuration));
	}
	
	private IEnumerator Fade (float startLevel, float endLevel, float time)
	{
		float speed = 1.0f/time;
		
		for (float t = 0.0f; t < 1.0; t += Time.deltaTime*speed)
		{
			float a = Mathf.Lerp(startLevel, endLevel, t);
			GetComponent<GUITexture>().color = new Color(GetComponent<GUITexture>().color.r,
			                                      GetComponent<GUITexture>().color.g,
			                                      GetComponent<GUITexture>().color.b, a);
			yield return 0;
		}
	}
}