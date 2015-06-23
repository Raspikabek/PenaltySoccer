using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GameController : MonoBehaviour {

	public UnityEvent OnGoal;
	public UnityEvent OnMiss;
	public UnityEvent OnSafe;
	public UnityEvent OnWin;
	public UnityEvent OnLoose;

	public GameController(){
		OnGoal = new UnityEvent ();
		OnMiss = new UnityEvent ();
		OnSafe = new UnityEvent ();
		OnWin = new UnityEvent ();
		OnLoose = new UnityEvent ();
	}

	void Awake(){
		//ADDLISTENERS
		OnGoal.AddListener (goalDetected);
	}

	private void goalDetected(){
		Debug.Log ("GOAL!");
	}

}
