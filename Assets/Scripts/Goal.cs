using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.tag == "Ball") {
			Debug.Log ("GOAL!");
		}
	}
}
