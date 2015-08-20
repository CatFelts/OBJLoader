using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//rotate clockwise about the y axis when right arrow key or the right trigger on xbox controller pressed
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.JoystickButton5)){
			RotateClockwise();
		}
		
		//rotate counterclockwise about the y axis when left arrow key or the left trigger on xbox controller pressed
		else if(Input.GetKey(KeyCode.LeftArrow) ||Input.GetKey(KeyCode.JoystickButton4)){
			RotateCounterClockwise();
		}
		
	}
	
	void RotateClockwise(){
		
		transform.Rotate (0f, 1f, 0f);
		
	}
	
	void RotateCounterClockwise(){
		
		transform.Rotate (0f, -1f, 0f);
		
	}
}
