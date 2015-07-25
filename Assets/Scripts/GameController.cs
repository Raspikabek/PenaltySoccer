using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	[SerializeField]
	private NewSwipeControl _newSwipeControl;
	[SerializeField]
	private GoalKeeperController _goalKeeperController;
	[SerializeField]
	private SoccerPlayerController _soccerPlayerController;
	private int goalCount;
	
	public UnityEvent OnShoot;
	public UnityEvent OnGoal;
	public UnityEvent OnMiss;
	public UnityEvent OnSafe;
	public UnityEvent OnFinish;
	public UnityEvent OnLoose;
	public RawImage winImage;
	public RawImage looseImage;
	public Image goal1, goal2, goal3, goal4, goal5;
	public int shootsTaken;
	public bool canShoot = true;
	public bool canGoal;
	public bool winState;


	public GameController(){
		OnShoot = new UnityEvent ();
		OnGoal = new UnityEvent ();
		OnMiss = new UnityEvent ();
		OnSafe = new UnityEvent ();
		OnFinish = new UnityEvent ();
		OnLoose = new UnityEvent ();
	}

	void Awake(){
		OnGoal.AddListener (GoalDetected);
		//OnFinish.AddListener (WinMatch);
		//OnFinish.AddListener (_goalKeeperController.MatchEndedMove);
		//OnFinish.AddListener (_soccerPlayerController.MatchEndedMove);
		canGoal = true;
	}

	void Start(){
		goalCount = 0;
		shootsTaken = 0;
		HideUI ();
	}

	void Update(){
		if(_newSwipeControl.ballReturned && canShoot){
			_newSwipeControl.PlayerLogic();
		}
	}

	public void MoveGoalKeeper(int shootDirection){
		_goalKeeperController.GoalKeeperJump(shootDirection);
	}

	public void MoveSoccerPlayer(){
		_soccerPlayerController.MoveSoccerPlayer();
	}
	
	private void GoalDetected(){
		//TODO REFACTOR THIS & WinMatch + LooseMatch functions
		canGoal = false;
		goalCount++;
		DisplayGoalCounter ();

		if(goalCount >= 3){
			WinMatch();
		}
	}

	/*private void MatchFinished(){
		if(goalCount >= 3){
			winImage.enabled = true;
			winState = true;
			StartCoroutine(RestartMatch ());
			_goalKeeperController.MatchEndedMove();
			_soccerPlayerController.MatchEndedMove();
		}
		else if (shootsTaken == 5 && goalCount < 3){
			looseImage.enabled = true;
			winState = false;
			StartCoroutine(RestartMatch());
			_goalKeeperController.MatchEndedMove();
			_soccerPlayerController.MatchEndedMove();
		}
	}*/

	private void WinMatch(){
		winImage.enabled = true;
		StartCoroutine (RestartMatch ());
	}

	private void DisplayGoalCounter(){
		if (goalCount == 1) {
			goal1.enabled = true;
		} else if (goalCount == 2) {
			goal2.enabled = true;
		} else if (goalCount == 3) {
			goal3.enabled = true;	
		} else if (goalCount == 4) {
			goal4.enabled = true;	
		} else if (goalCount == 5) {
			goal5.enabled = true;
		}
	}

	private void HideUI(){
		winImage.enabled = false;
		looseImage.enabled = false;
		goal1.enabled = false;
		goal2.enabled = false;
		goal3.enabled = false;
		goal4.enabled = false;
		goal5.enabled = false;
	}

	IEnumerator RestartMatch(){
		yield return new WaitForSeconds(4.0f);
		goalCount = 0;
		shootsTaken = 0;
		HideUI ();
	}
}