using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class GameController : MonoBehaviour {

	public UnityEvent OnGoal;
	public UnityEvent OnMiss;
	public UnityEvent OnSafe;
	public UnityEvent OnWin;
	public UnityEvent OnLoose;
	public Text countText;
	public Text winText;

	private int count; 

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

	void Start(){
		count = 0;
		SetCountText ();
	}

	private void goalDetected(){
		count = count + 1;
		SetCountText ();

		if (count == 5) {
			SetWinText();
		}
	}

	private void SetCountText(){
		countText.text = "Goal Count: " + count.ToString ();
	}

	private void SetWinText(){
		winText.text = "YOU WIN!";
	}

}
