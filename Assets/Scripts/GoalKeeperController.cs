using UnityEngine;
using System.Collections;

public class GoalKeeperController : MonoBehaviour {

	Animator anim;
	int jumpHash = Animator.StringToHash("Jump");
	public bool jump = false;

	void Start(){
		anim = GetComponent<Animator> ();
	}

	void Update(){
		if(jump == true){
			anim.SetTrigger (jumpHash);
		}
	}
}
