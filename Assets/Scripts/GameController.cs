using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	[SerializeField]
	private NewSwipeControl _newSwipeControl;
	[SerializeField]
	private GoalKeeperController goalKeeperController;
	private int count;
	
	public UnityEvent OnShoot;
	public UnityEvent OnGoal;
	public UnityEvent OnMiss;
	public UnityEvent OnSafe;
	public UnityEvent OnWin;
	public UnityEvent OnLoose;
	public RawImage winImage;
	public Image goal1, goal2, goal3, goal4, goal5;
	public bool canShoot = true;
	public bool canGoal;


	public GameController(){
		OnShoot = new UnityEvent ();
		OnGoal = new UnityEvent ();
		OnMiss = new UnityEvent ();
		OnSafe = new UnityEvent ();
		OnWin = new UnityEvent ();
		OnLoose = new UnityEvent ();
	}

	void Awake(){
		OnGoal.AddListener (GoalDetected);
		OnWin.AddListener (WinMatch);
		canGoal = true;
	}

	void Start(){
		count = 0;
		HideUI ();
	}

	void Update(){
		if(_newSwipeControl.ballReturned && canShoot){
			_newSwipeControl.PlayerLogic();
		}
	}

	public void MoveGoalKeeper(int shootDirection){
		goalKeeperController.GoalKeeperJump(shootDirection);
	}
	
	private void GoalDetected(){
		canGoal = false;
		count = count + 1;
		DisplayGoalCounter ();
		//SetCountText ();

		if (count >= 5) {
			OnWin.Invoke();
		}
	}

	private void WinMatch(){
		winImage.enabled = true;
		StartCoroutine (RestartMatch ());
	}

	private void DisplayGoalCounter(){
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

	private void HideUI(){
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
		HideUI ();
	}
}