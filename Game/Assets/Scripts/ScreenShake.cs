using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	private Vector3 basePosition;

	void Awake(){
		basePosition = transform.position;
	}

	public void Shake( float duration, float power, float intensity){
		StartCoroutine(DoShake(duration, power, intensity));
	}

	IEnumerator DoShake (float duration, float power, float intensity){
		float t = 0;
		while(t < duration){
			t += intensity;
			transform.position =  new Vector3(transform.position.x + power * Random.Range(-1f,1f), transform.position.y + power * Random.Range(-1f,1f), transform.position.z);
			yield return new WaitForSeconds(intensity);
		}
		transform.position = basePosition;
		yield break;
	}
}
