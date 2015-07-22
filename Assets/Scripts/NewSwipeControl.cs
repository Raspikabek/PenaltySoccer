using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewSwipeControl : MonoBehaviour {

	[SerializeField]
	private GameController gameController;
	private AudioSource shootAudio;
	private Rigidbody rb;
	
	private Vector3 startPos;
	private Vector3 endPos;
	private float dragDistance;

	private Vector3 defaultBallPosition;
	private float factor = 34f; // keep this factor constant, also used to determine force of shot
	public float power;
	public bool ballReturned = true;  //flag to check if the ball is returned to its initial position

	void Start(){
		rb = GetComponent<Rigidbody> ();
		shootAudio = GetComponent<AudioSource> ();
		Time.timeScale = 1;    //set it to 1 on start so as to overcome the effects of restarting the game by script
		dragDistance = Screen.height * 20/100; //20% of the screen should be swiped to shoot
		Physics.gravity = new Vector3(0, -20, 0); //reset the gravity of the ball to 20
		defaultBallPosition = transform.position;  //store the initial position of the football
	}

	public void PlayerLogic(){
		//NEW FUNCTION
		if (Input.touchCount > 0){
			var touch = Input.GetTouch(0);

			switch(touch.phase) {
				case TouchPhase.Began:
				startPos = touch.position;
				break;

				case TouchPhase.Ended:
				endPos = touch.position;

				if(Mathf.Abs(endPos.x - startPos.x) > dragDistance || Mathf.Abs(endPos.y - startPos.y) > dragDistance){
					float x = (endPos.x - startPos.x) / Screen.height * factor;
					float y = (endPos.y - startPos.y) / Screen.height * factor;
					int shootDirection;

					if(Mathf.Abs(endPos.x - startPos.x) > Mathf.Abs(endPos.y - startPos.y)){
						//If the horizontal movement is greater than the vertical...
						if((endPos.x > startPos.x)){ 
							//Right move
							shootDirection = 25;
							ShootBall(x,y,shootDirection);
						}
						else{
							//Left move
							shootDirection = 75;
							ShootBall(x,y,shootDirection);						}
					}
					else{ // If the vertical movement is greater than the horizontal movement...
						if (endPos.y > startPos.y){
							//Up move
							if(endPos.x > startPos.x){
								shootDirection = 50 ;
							}
							else{
								shootDirection = 75;
							}
							ShootBall(x,y,shootDirection);						}
						else{
							//Down move
						}
					}
				}
				break;
			}
		}
	}

	private void ShootBall(float x, float y, int shootDirection){
		rb.AddForce(( new Vector3(x,y,15)) * power);
		gameController.MoveGoalKeeper(shootDirection);
		shootAudio.Play();
		gameController.canShoot = false;
		ballReturned = false;
		StartCoroutine(ReturnBall());
}

IEnumerator ReturnBall() {  // EVENT IN GAMECONTROLLER
		yield return new WaitForSeconds(4.0f);  //set a delay of 5 seconds before the ball is returned
		GetComponent<Rigidbody>().velocity = Vector3.zero;   //set the velocity of the ball to zero
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;  //set its angular vel to zero
		transform.position = defaultBallPosition;   //re positon it to initial position
		gameController.canShoot = true;     //set the canshoot flag to true
		ballReturned = true;     //set football returned flag to true as well
		gameController.canGoal = true;
	}

}
