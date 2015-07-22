using UnityEngine;
using System.Collections;

public class GoalKeeperController : MonoBehaviour {

	Animator anim;
	int jumpLeftHash = Animator.StringToHash("JumpLeft");
	int jumpLongLeftHash = Animator.StringToHash ("JumpLongLeft");
	int jumpRightHash = Animator.StringToHash ("JumpRight");
	int jumpLongRightHash = Animator.StringToHash ("JumpLongRight");
	public bool jump = false;

	void Start(){
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
}
