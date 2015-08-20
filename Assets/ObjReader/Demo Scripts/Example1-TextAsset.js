// This example uses OBJ and MTL files from TextAssets, and doesn't supply any material,
// so the default VertexLit materials are used.
// Note that files used as TextAssets should end with ".txt".

var objFile : TextAsset;
var mtlFile : TextAsset;

function Start () {
	var loadingText = GameObject.Find("LoadingText").GetComponent(GUIText);
	loadingText.enabled = true;
	loadingText.text = "Loading...";
	yield;
	
	if (mtlFile != null) {
		ObjReader.use.ConvertString (objFile.text, mtlFile.text);
	}
	else {
		ObjReader.use.ConvertString (objFile.text);
	}
	
	loadingText.enabled = false;
}