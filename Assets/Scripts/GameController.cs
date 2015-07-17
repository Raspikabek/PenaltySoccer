using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	[SerializeField]
	private SwipeControl swipeControl;
	[SerializeField]
	private GoalKeeperController goalKeeperController;

	public UnityEvent OnShoot;
	public UnityEvent OnGoal;
	public UnityEvent OnMiss;
	public UnityEvent OnSafe;
	public UnityEvent OnWin;
	public UnityEvent OnLoose;
	public RawImage winImage;
	public Image goal1, goal2, goal3, goal4, goal5;
	public bool ballDirection;
	
	public bool canGoal;
	public int turn = 0; // 0 striker, 1 goalkeeper

	private int count;

	public GameController(){
		OnShoot = new UnityEvent ();
		OnGoal = new UnityEvent ();
		OnMiss = new UnityEvent ();
		OnSafe = new UnityEvent ();
		OnWin = new UnityEvent ();
		OnLoose = new UnityEvent ();
	}

	void Awake(){
		OnShoot.AddListener (moveGoalKeeper);
		OnGoal.AddListener (goalDetected);
		OnWin.AddListener (winMatch);
		canGoal = true;
	}

	void Start(){
		count = 0;
		hideUI ();
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
		displayGoalCounter ();
		//SetCountText ();

		if (count >= 5) {
			OnWin.Invoke();
		}
	}

	private void moveGoalKeeper(){
		goalKeeperController.goalKeeperJump (ballDirection);
	}

	private void winMatch(){
		winImage.enabled = true;
		StartCoroutine (RestartMatch ());
	}

	private void displayGoalCounter(){
		if (count == 1) {
			goal1.enabled = true;
		} else if (count == 2) {
			goal2.enabled = true;
		} else if (count == 3) {
			goal3.enabled = true;	
		} else if (count == 4) {
			goal4.enabled = true;	
		} else if (count == 5) {
			goal5.enabled = true;
		}
	}

	private void hideUI(){
		winImage.enabled = false;
		goal1.enabled = false;
		goal2.enabled = false;
		goal3.enabled = false;
		goal4.enabled = false;
		goal5.enabled = false;
	}

	IEnumerator RestartMatch(){
		yield return new WaitForSeconds(4.0f);
		count = 0;
		hideUI ();
	}
}