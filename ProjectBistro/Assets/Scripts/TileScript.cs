using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	private bool chosen = false;
	private Material mat;
	private Color baseColor;

	public Color highlightColor;

	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer>().material;
		baseColor = mat.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
		if (!chosen) {
			mat.color = highlightColor;
		}
	}

	void OnMouseExit(){
		mat.color = baseColor;
	}

	public void ChangeBaseColor(){
		baseColor = Color.red;
		mat.color = baseColor;
	}

	public void Choose(){
		chosen = true;
	}

}
