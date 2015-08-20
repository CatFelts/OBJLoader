using UnityEngine;
using System.Collections;
using System.IO;

public class FileExplorer : MonoBehaviour {

	public bool showButton;

	private FileLoader fileLoader;
	private string[] fileDirectory;


	// Use this for initialization
	void Start () {
		fileLoader = GameObject.FindGameObjectWithTag ("File Loader").GetComponent<FileLoader> ();
		fileDirectory = Directory.GetFiles ("C://Users/Cat/Documents/8las/Blender Parts/OBJ/Random");
		showButton = true;
	}
	
	void OnGUI(){
		if (showButton) {
			//create background
			GUI.Box (new Rect (10, 20, 170, 450), "File Loader");

			//make corresponding buttons for all files in specified directory
			for (int i = 0; i<fileDirectory.Length; i++) {
				if(!fileDirectory[i].ToString().Contains(".mtl")){
					if (GUI.Button (new Rect (20, 40 + i * 20,150, 20), fileDirectory [i].ToString ().Substring (54))) {
						showButton = false;
						fileLoader.loadFileName (fileDirectory [i].ToString ());
					}
				}
			}

		} 
	}
}
