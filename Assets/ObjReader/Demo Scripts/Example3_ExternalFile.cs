using UnityEngine;
using System.Collections;

public class Example3_ExternalFile : MonoBehaviour {

	public string objFileName = "Car_obj.txt";
	public Material standardMaterial;
	public Material transparentMaterial;

	IEnumerator Start () {
		var loadingText = GameObject.Find("LoadingText").GetComponent<GUIText>();
		loadingText.enabled = true;
		loadingText.text = "Loading...";
		yield return null;
		
		objFileName = Application.dataPath + "/ObjReader/Sample Files/" + objFileName;
		
		ObjReader.use.ConvertFile (objFileName, true, standardMaterial, transparentMaterial);
		
		loadingText.enabled = false;
	}
}