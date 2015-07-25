using UnityEngine;
using System.Collections;

public class SoccerPlayerController : MonoBehaviour {

	[SerializeField]
	private NewSwipeControl _newSwipeControl;
	[SerializeField]
	private GameController _gameController;
	Animator anim;
	private int moveSoccerHash = Animator.StringToHash("Shoot");
	private int victoryHash = Animator.StringToHash ("Victory");
	private int defeatedHash = Animator.StringToHash ("Defeated");

	void Awake(){
		_gameController = GetComponent<GameController>();
		anim = GetComponent<Animator> ();
	}

	public void MoveSoccerPlayer(){
		anim.SetTrigger(moveSoccerHash);
	}

	private void shootBall(){
		_newSwipeControl.ShootBall();
	}

	public void MatchEndedMove(){
		if (_gameController.winState == true){
			anim.SetTrigger(victoryHash);
		}
		else{
			anim.SetTrigger(defeatedHash);
		}
	}
}
