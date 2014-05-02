using UnityEngine;
using System.Collections;

public class BackgroundMouvement : MonoBehaviour {

	public float speed;
	public Transform spawnPoint;
	public Transform destination;

	void Update (){
		transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
		if(transform.position == destination.position)
			transform.position = spawnPoint.position;
	}
}
