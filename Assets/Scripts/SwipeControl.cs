using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwipeControl : MonoBehaviour
{
	//variables for swipe input detection
	private Vector3 fp; //First finger position
	private Vector3 lp; //Last finger position
	private float dragDistance;  //Distance needed for a swipe to register
	
	//variables for determining the shot power and position
	public float power;  //power at which the ball is shot
	private Vector3 footballPos; //initial football position for replacing the ball at the same posiiton
	private float factor = 34f; // keep this factor constant, also used to determine force of shot
	
	public bool canShoot = true;  //flag to check if shot can be taken
	public int scorePlayer = 0;  //score of player
	public int scoreOpponent = 0; //score of oponent
	public int turn = 0;   //0 for striker, 1 for goalie
	public bool isGameOver = false; //flag for game over detection
	Vector3 oppKickDir;   //direction at which the ball is kicked by opponent
	public int shotsTaken = 0;  //number of rounds of penalties taken
	private bool returned = true;  //flag to check if the ball is returned to its initial position
	public bool isKickedPlayer = false; //flag to check if the player has kicked the ball
	public bool isKickedOpponent = false; //flag to check if the opponent has kicked the ball
	
	void Start(){
		Time.timeScale = 1;    //set it to 1 on start so as to overcome the effects of restarting the game by script
		dragDistance = Screen.height*20/100; //20% of the screen should be swiped to shoot
		Physics.gravity = new Vector3(0, -20, 0); //reset the gravity of the ball to 20
		footballPos = transform.position;  //store the initial position of the football
	}
	
	// Update is called once per frame
	void Update()
	{
		if(returned){     //check if the football is in its initial position
			if(turn==0 && !isGameOver){ //if its users turn to shoot and if the game is not over
				playerLogic();   //call the playerLogic fucntion
			}
			else if(turn==1 && !isGameOver){ //if its opponent's turn to shoot
				opponentLogic(); //call the respective function
			}
		}
	}
	
	void playerLogic(){
		//Examine the touch inputs
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				fp = touch.position;
				lp = touch.position; 
			}
			
			if (touch.phase == TouchPhase.Ended)
			{
				lp = touch.position; 
				//First check if it's actually a drag
				
				if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
				{   //It's a drag
					
					//x and y repesent force to be added in the x, y axes.
					float x = (lp.x - fp.x) / Screen.height * factor; 
					float y = (lp.y-fp.y)/Screen.height*factor;
					//Now check what direction the drag was
					//First check which axis
					if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
					{   //If the horizontal movement is greater than the vertical movement...
						
						if ((lp.x>fp.x) && canShoot)  //If the movement was to the right)
						{   //Right move
							GetComponent<Rigidbody>().AddForce((new Vector3(x,10,15))*power); 
						}
						else
						{   //Left move
							GetComponent<Rigidbody>().AddForce((new Vector3(x,10,15))*power);
						}
					}
					else
					{   //the vertical movement is greater than the horizontal movement
						if (lp.y>fp.y)  //If the movement was up
						{   //Up move
							GetComponent<Rigidbody>().AddForce((new Vector3(x,y,15))*power);
						}
						else
						{   //Down move
							
						}
					}
				}
				canShoot = false;
				returned = false;
				isKickedPlayer = true;
				StartCoroutine(ReturnBall());
			}
			else
			{   //It's a tap
				
			}
		}
	}
	
	IEnumerator ReturnBall() {
		yield return new WaitForSeconds(5.0f);  //set a delay of 5 seconds before the ball is returned
		GetComponent<Rigidbody>().velocity = Vector3.zero;   //set the velocity of the ball to zero
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;  //set its angular vel to zero
		transform.position = footballPos;   //re positon it to initial position
		//take turns in shooting
		if(turn==1)      
			turn=0;    
		else if(turn==0)
			turn=1;
		canShoot =true;     //set the canshoot flag to true
		returned = true;     //set football returned flag to true as well
	}
	
	
	void opponentLogic(){
		//check for screen tap
		int fingerCount = 0;
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				fingerCount++; 
		}
		//if tapped, the opponent will shoot the football after some time delay as mentioned below
		if(fingerCount>0){
			StartCoroutine(DelayAdd());  //add delay before the ball is shot
			isKickedOpponent = true;  //set opponent kicked to true
			shotsTaken++;    //increase set of penalty taken
			returned = false;   
			StartCoroutine(ReturnBall()); //return the ball back to its initial position
		}
	}
	
	IEnumerator DelayAdd() {
		yield return new WaitForSeconds(0.2f);  //I have added a delay of 0.2 seconds
		oppKickDir = new Vector3(Random.Range(-4f, 4f), Random.Range(5f, 10f), Random.Range(6f, 12f));     //generate a random x and y value in the range mentioned
		GetComponent<Rigidbody>().AddForce(oppKickDir, ForceMode.Impulse); //add the force 
	} 
	
}
