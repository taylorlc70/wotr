using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(RectTransform))]
public class UI_SlideInOut : MonoBehaviour {

	public void SlideInUI(int sec){
		StartCoroutine(SlideInCR(sec));
	}

	//TODO: I have to make this coroutine where the end position is generalizable
	//TODO: I should make it so the time for slide in is variable

	private IEnumerator SlideInCR(int seconds){
		float elapsedTime = 0.0f;
		while(elapsedTime < 1.0f){
			//currentPos.anchoredPosition = Vector2.Lerp( huntBox_OutPos, huntBox_InPos, elapsedTime );
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(seconds);
		StartCoroutine(SlideOutCR());
	}
	
	private IEnumerator SlideOutCR(){
		float elapsedTime = 0;
		while(elapsedTime < 1.0f){
			//currentPos.anchoredPosition = Vector2.Lerp( huntBox_InPos, huntBox_OutPos, elapsedTime );
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}
