using UnityEngine;
using System.Collections;

public class GoalKeeperController : MonoBehaviour {

	[SerializeField]
	private GameController _gameController;
	Animator anim;
	private int jumpLeftHash = Animator.StringToHash("JumpLeft");
	private int jumpLongLeftHash = Animator.StringToHash ("JumpLongLeft");
	private int jumpRightHash = Animator.StringToHash ("JumpRight");
	private int jumpLongRightHash = Animator.StringToHash ("JumpLongRight");
	private int victoryHash = Animator.StringToHash ("VictoryGK");
	private int defeatedHash = Animator.StringToHash ("DefeatedGK");

	void Awake(){
		_gameController = GetComponent<GameController>();
		anim = GetComponent<Animator> ();
	}

	public void GoalKeeperJump(int shootDirection){
		//UNDONE Seleccionar una animacion segun la dificultad, dependiendo del valor recibido.
		switch(shootDirection){
			case 25:
			anim.SetTrigger(jumpLongRightHash);
			break;

			case 50:
			anim.SetTrigger(jumpRightHash);
			break;

			case 75:
			anim.SetTrigger(jumpLongLeftHash);
			break;

			case 100:
			anim.SetTrigger(jumpLeftHash);
			break;
		}
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
