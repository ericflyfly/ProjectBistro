using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairModelScript : MonoBehaviour {

	public GameObject tableModel;
	public bool tableChosen = false;
	public bool occupied = false;

	public void UpdateTableChosen(GameObject table){
		if (tableChosen) {
			return;
		}

		tableModel = table;
		//Debug.Log ("Chair at " + transform.position + " has been assigned a table!");

		transform.LookAt (table.transform);
		transform.rotation *= Quaternion.Euler (0, 90f, 0);

		tableChosen = true;
	}

	public bool TableHasSpace(){
		int count = 0;
		if (tableChosen) {
			TileScript t = tableModel.GetComponentInParent<TileScript> ();
			foreach (TileScript ts in t.neighbours) {
				if (ts.itemSet || !ts.chosen) {
					count++;
				}
			}
		}

		if (count >= 4) {
			return false;
		}

		return true;
	}

	public Vector2 GetFreeSpace(){
		if (TableHasSpace()) {
			TileScript t = tableModel.GetComponentInParent<TileScript> ();
			foreach (TileScript ts in t.neighbours) {
				if (ts.chosen && !ts.itemSet) {
					return new Vector2 (ts.x, ts.y);
				}
			}
		} 

		Debug.Log ("Table doesn't have space");
		return new Vector2(-1,-1);
	}
}
