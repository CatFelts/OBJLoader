using UnityEngine;
using System.Collections;

public class Pan : MonoBehaviour {

	private Vector3 initPos;
	private Vector3 zDir;
	private Vector3 xDir;

	// Use this for initialization
	void Start () {
		initPos = gameObject.transform.position;
		zDir = new Vector3 (0f, 0f, .5f);
		xDir = new Vector3 (.5f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			gameObject.transform.Translate(zDir);
		}
		else if (Input.GetKey (KeyCode.S)) {
			gameObject.transform.Translate(zDir * -1);
		}
		if(Input.GetKey(KeyCode.D)){
			gameObject.transform.Translate(xDir);
		}
		else if(Input.GetKey(KeyCode.A)){
			gameObject.transform.Translate(xDir * -1);
		}
	}
}
