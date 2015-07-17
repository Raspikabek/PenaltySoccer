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

	public void goalKeeperJump(bool ballDirection){
		if (ballDirection) {
			anim.SetTrigger (jumpLongRightHash);
		} else {
			anim.SetTrigger (jumpLongLeftHash);
		}
	}
}
