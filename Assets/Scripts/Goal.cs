using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	[SerializeField]
	private GameController gameController;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Ball" && gameController.canGoal) {
			gameController.OnGoal.Invoke ();
		}
	}
}
