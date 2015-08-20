
using UnityEngine;
using System.Collections;
using System.IO;

public class FileLoader : MonoBehaviour {
	//path to obj file
	public string objFileName = "C://Users/8las";
	public Material standardMat;
	public Material transparentMat;
	public Vector3 objPos;

	private FileExplorer fileExplorer;
	private GUIText loadingText;
	private GameObject[] loadedObjs;
	private GameObject parent ;
	private float x = 0;
	private float y = 0;
	private float z = 0;
	private bool displayError;

	void Start () {
		//display loading text
		loadingText = GameObject.Find("LoadingText").GetComponent<GUIText>();
		loadingText.enabled = true;
		loadingText.text = "Loading...";

		fileExplorer = gameObject.GetComponent<FileExplorer> ();
	

		//load obj file from path specified by objfilename
		if (objFileName != "")
						ObjReader.use.ConvertFile (objFileName, true, standardMat, transparentMat);
				else 
						Debug.Log ("Must either enter file path or select obj from menu");

		//disable loading text
		loadingText.enabled = false;
		displayError = false;
	}

	public void loadFileName(string fileName){

		Debug.Log ("trying to open file from path : " + fileName);

		//count the number of vertices in the obj file
		int[] vertCounts = CountVerts (fileName);
		int maxint = 0;
		foreach (int value in vertCounts) {
			if (value > maxint) maxint = value;
		}
		Debug.Log ("maxint vertCounts = "+maxint);

		//display error message instead of trying to load the file if it has >65535 vertices
		if (maxint > 65535) {
			displayError = true;
			fileExplorer.showButton = true;	
			return;
		}
		


		//set path to obj file as fileName
		objFileName = fileName;

		//display loading text
		loadingText = GameObject.Find("LoadingText").GetComponent<GUIText>();
		loadingText.enabled = true;
		loadingText.text = "Loading...";
		
		//load obj file from path specified by objfilename
		loadedObjs = ObjReader.use.ConvertFile (objFileName, true, standardMat, transparentMat);
		//space apart loaded objs 

		//set common parent to all loaded objs
		parent = new GameObject ();
		for (int i = 0; i<loadedObjs.Length; i++) {
			GameObject obj = loadedObjs [i];
			obj.transform.SetParent (parent.transform);
		}


		//disable loading text
		loadingText.enabled = false;

		//add various components to new parent gameobject
		AddComponents ();

		//normalize the scale
		Normalize (); 

		//RecalcNormals (); //doesnt seem to help

	}

	void OnGUI(){
		if(displayError){
			GUI.Box(new Rect (500, 50, 300, 250), "Error\n\n\n.obj file specified has too many vertices.\nPlease choose a different .obj file\n with <65535 vertices.");

			if(GUI.Button(new Rect (630, 200, 40, 20), "OK"))
				displayError = false;

		}
	}

	//parse the .obj file to find how many vertices there are per object before trying to convert to gameobject[]
	static int[] CountVerts(string filename){
		int index = 0;
		int[] vertCounts = new int[100];
		int vertCount = 0;
		string[] lines = File.ReadAllLines (filename);


		for(int i = 0; i<lines.Length; i++){
			string line = lines[i];

			if(line.StartsWith("v ")){
				vertCount++;
			}
			else if(line.StartsWith("o " ) && vertCount !=0){	
				Debug.Log ("vertex count = "+vertCount);
			    vertCounts[index] = vertCount;
				index++;
				vertCount = 0;
			}
		}
		vertCounts [index] = vertCount;


		return vertCounts;
	
	}

	void AddComponents(){
		//add scripts for rotate, zoom, and pan functionality
		parent.AddComponent<Rotate> ();
		parent.AddComponent<Zoom> ();
		parent.AddComponent<Pan> ();

	}


	void Normalize(){
		Vector3 centerSum = Vector3.zero;
		//find the largest axis of all the objects loaded && find renderer center and average them
		float largestScale = -1f;
		for (int i = 0; i<loadedObjs.Length; i++) {
			GameObject obj = loadedObjs[i];
			centerSum += obj.renderer.bounds.center;
			if(obj.renderer.bounds.extents.magnitude > largestScale)
				largestScale = obj.renderer.bounds.extents.magnitude;
		}
		Vector3 avgCenter = centerSum / loadedObjs.Length;


		//scale the parent 
		parent.transform.localScale = new Vector3(1f/largestScale, 1f/largestScale, 1f/largestScale);

		//offset the parent so obj appears at (0, 0, 0)
		parent.transform.position = new Vector3 (avgCenter.x/largestScale * -1, avgCenter.y/largestScale * -1, avgCenter.z/largestScale * -1);
	
	}

	void RecalcNormals(){
		/*
		 for(int i = 0; i<loadedObjs.Length; i++){
			GameObject obj = loadedObjs[i];
			Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
			mesh.RecalculateNormals();
		}
		*/

		for( int i = 0; i<loadedObjs.Length; i++){
			GameObject obj = loadedObjs[i];
			Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
			Vector3[] normals = mesh.normals;
			for( int j = 0; j<normals.Length; j++){
				normals[j] = normals[j] * -1;
				Debug.Log("flipped normal : "+normals[j].ToString());
			}
		}
	}
}
