using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScript : MonoBehaviour {

	public List<Order> orderList;

	// Use this for initialization
	void Start () {
		orderList = new List<Order> (); 
	}

	void Update(){
		if (Input.GetKeyDown ("z")) {
			AddOrder(1,7,4);
		}
	}

	public class Order {
		public int food;
		public int x;
		public int y;

		public Order(int food, int x, int y){
			this.food = food;
			this.x = x;
			this.y = y;
		}
	}

	public void AddOrder(int food, int x, int y){
		orderList.Add (new Order (food, x, y)); 
		Debug.Log ("Order recieved at " + x + "," + y);
		//TODO: give the order to the closest free waiter. 
	}

	public void OrderButton(){
		AddOrder(1,7,4);
	}

	public void AssignRandomSeat(){
		List<ChairModelScript> c = new List<ChairModelScript> ();
		foreach (TileScript t in GameManager.mapArray) {
			if (t.it == ItemScript.itemType.chair) {
				ChairModelScript currentScript = t.GetComponentInChildren<ChairModelScript> ();
				if (currentScript.tableChosen && currentScript.TableHasSpace()) {
					c.Add (currentScript);
					Debug.Log ("Adding this chair at " + t.x + ", " + t.y);
				}
			}
		}

		//TODO: choose randomly and choose different people 
		if (c.Count > 0) {
			Vector2 freeSpacePosition = c [0].GetFreeSpace ();
			GameManager.custList [0].transform.position = c [0].transform.position;
			AddOrder (1, (int)freeSpacePosition.x, (int)freeSpacePosition.y);
		}
	}
}
