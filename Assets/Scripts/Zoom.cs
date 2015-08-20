using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {
	
	private float scaleFactor = 2.5f;
	
	// Update is called once per frame
	void Update () {
		//if the up arrow or the left bumper on xbox controller pressed, scale bike up("zoom" in)
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.JoystickButton8)) {
			ScaleUp();
		}
		//if down arrow or right bumper on xbox controller pressed, scaled bike down("zoom" out)
		if(Input.GetKey (KeyCode.DownArrow) || Input.GetKey(KeyCode.JoystickButton9)){
			ScaleDown();
		}
		
	}
	
	void ScaleUp(){
		//dont scale bigger if scale factor already 4
		if (scaleFactor <= 5) {
			transform.localScale += new Vector3 (.1f, .1f, .1f);
			scaleFactor+=.1f;
		}
		
	}
	
	void ScaleDown(){
		//dont scale down if scale factor already -1
		if (scaleFactor > 1.7) {
			transform.localScale -= new Vector3 (.1f, .1f, .1f);
			scaleFactor-=.1f;
		}
	}
}
