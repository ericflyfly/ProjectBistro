using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public MapGenerator mg;

	public static TileScript[,] mapArray; 

	private Vector2 mouseOver;

	// Use this for initialization
	void Start () {
		mg.GenerateMap ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMouseOver ();
		//Debug.Log (mouseOver);

		if (Input.GetMouseButtonDown(0) && mouseOver.x >= 0) {
			Debug.Log ("chosen tile at " + mouseOver.x + ", " + mouseOver.y);
			mapArray [(int)mouseOver.x, (int)mouseOver.y].Choose ();
			mapArray [(int)mouseOver.x, (int)mouseOver.y].ChangeBaseColor ();

		}
	}

	//Method to check the location of the mouse on screen in terms of x and y
	private void UpdateMouseOver(){
		if (!Camera.main) {
			Debug.Log ("Unable to find main camera");
			return;
		}

		RaycastHit hit; 
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("Item"))) {
			mouseOver.x = Mathf.Floor(hit.point.x);
			mouseOver.y = Mathf.Floor(hit.point.z); //note that it is z because it's in 3D
		} else {
			mouseOver.x = -1;
			mouseOver.y = -1;
		}

	}
}
