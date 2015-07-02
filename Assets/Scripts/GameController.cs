using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	[SerializeField]
	private SwipeControl swipeControl;

	public UnityEvent OnGoal;
	public UnityEvent OnMiss;
	public UnityEvent OnSafe;
	public UnityEvent OnWin;
	public UnityEvent OnLoose;
	public Text countText;
	public Text winText;
	
	public bool canGoal;
	public int turn = 0; // 0 striker, 1 goalkeeper

	private int count;

	public GameController(){
		OnGoal = new UnityEvent ();
		OnMiss = new UnityEvent ();
		OnSafe = new UnityEvent ();
		OnWin = new UnityEvent ();
		OnLoose = new UnityEvent ();
	}

	void Awake(){
		OnGoal.AddListener (goalDetected);
		OnWin.AddListener (winMatch);
		canGoal = true;
	}

	void Start(){
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void Update(){
		if(swipeControl.returned){     //check if the football is in its initial position
			if(turn==0 && !swipeControl.isGameOver){ //if its users turn to shoot and if the game is not over
				swipeControl.playerLogic();   //call the playerLogic fucntion
			}
			else if(turn==1 && !swipeControl.isGameOver){ //if its opponent's turn to shoot
				swipeControl.opponentLogic(); //call the respective function
			}
		}
	}

	private void goalDetected(){
		canGoal = false;
		count = count + 1;
		SetCountText ();

		if (count >= 5) {
			OnWin.Invoke();
		}
	}

	private void SetCountText(){
		countText.text = "Goal Count: " + count.ToString ();
	}

	private void winMatch(){
		winText.text = "YOU WIN!";
		StartCoroutine (RestartMatch ());
	}

	IEnumerator RestartMatch(){
		yield return new WaitForSeconds(4.0f);
		count = 0;
		SetCountText ();
		winText.text = "";
	}
}
