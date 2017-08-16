using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	public bool chosen = false;
	private Material mat;
	private Color baseColor;

	//For Item Setting
	private GameObject model;
	private GameObject currentModel;
	public bool itemSet = false;
	private float yOffset;
	public ItemScript.itemType it;

	public Color highlightColor;
	public Color chosenColor;

	//For path finding
	public List<TileScript> neighbours;
	public int x;
	public int y;

	// Use this for initialization
	void Awake () {
		mat = GetComponent<Renderer>().material;
		baseColor = mat.color;
		neighbours = new List<TileScript>();
		it = ItemScript.itemType.blank;
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
		if (!chosen) {
			mat.color = baseColor;
		}
	}

	public bool ChooseTile(){
		if (!chosen) {
			mat.color = chosenColor;
			chosen = true;
			return true;
		} else {
			//Debug.Log ("Can't choose any more tiles!");
			return false;
		}
	}

	public bool UnchooseTile(){
		if (chosen) {
			mat.color = baseColor;
			chosen = false;
			itemSet = false;
			if (it != ItemScript.itemType.blank) {
				it = ItemScript.itemType.blank;
			}
			if (currentModel != null) {
				Destroy (currentModel);
			}
			return true;
		} else {
			return false;
		}
	}

	//Set the item chosen to be displayed on this tile
	public void SetItem(ItemScript.itemType it){
		
		if (chosen) {
			this.it = it;
			if (model.GetComponent<ItemScript> () != null) {
				model.GetComponent<ItemScript> ().enabled = false;
			}
			currentModel = Instantiate (this.model, new Vector3 (transform.position.x, yOffset, transform.position.z), model.transform.rotation);
			currentModel.transform.parent = this.transform;

			if (it == ItemScript.itemType.table) {
				//Check all neighbours, if any are chairs call UpdateTableChosen on their ChairModelScripts
				foreach (TileScript t in neighbours) {
					if (t.it == ItemScript.itemType.chair) {
						ChairModelScript c = t.currentModel.GetComponent<ChairModelScript> ();
						c.UpdateTableChosen(currentModel);
					}
				}
			}

			if (it == ItemScript.itemType.chair) {
				foreach (TileScript t in neighbours) {
					if (t.it == ItemScript.itemType.table) {
						ChairModelScript c = currentModel.GetComponent<ChairModelScript> ();
						c.UpdateTableChosen(t.currentModel);
					}
				}
			}

			//Wont set an item down if it's waiter: will cause blockage.
			if (it != ItemScript.itemType.waiter) {
				itemSet = true;
			}
		} else {
			Debug.Log ("Tile has not been chosen");
		}
	}

	public void SetModel(GameObject g){
		this.model = g;
	}

	public void SetYOffset(float yOff){
		this.yOffset = yOff;
	}

	//Calculate distance to the tile in question
	public float DistanceTo(TileScript t) {
		if(t == null) {
			Debug.LogError("TileScript is empty!");
		}

		return Vector2.Distance(
			new Vector2(x, y),
			new Vector2(t.x, t.y)
		);
	}
}
