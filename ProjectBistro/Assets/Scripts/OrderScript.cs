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

	public void AssignRandomSeat(){
		List<ChairModelScript> c = new List<ChairModelScript> ();
		foreach (TileScript t in GameManager.mapArray) {
			if (t.it == ItemScript.itemType.chair) {
				ChairModelScript currentScript = t.GetComponentInChildren<ChairModelScript> ();
				if (currentScript.tableChosen && currentScript.TableHasSpace() && !currentScript.occupied) {
					c.Add (currentScript);
					//Debug.Log ("Adding this chair at " + t.x + ", " + t.y);
				}
			}
		}

		if (c.Count > 0) {
			//Choose a random unoccupied seat
			int randomSeat = Random.Range (0, c.Count);
			//Debug.Log ("Random seat index: " + randomSeat);
			while (c [randomSeat].occupied == true) {
				randomSeat = Random.Range (0, c.Count);
				//Debug.Log ("Next seat: " + randomSeat);
			}
				
			int randomCust = Random.Range (0, GameManager.custNumber);
			//Debug.Log ("Random customer index: " + randomCust);
			while (GameManager.custList [randomCust].GetComponent<CustomerScript> ().isSeated == true) {
				randomCust = Random.Range (0, GameManager.custNumber);
				//Debug.Log ("Next customer: " + randomCust);
			}


			Vector2 freeSpacePosition = c [randomSeat].GetFreeSpace ();
			GameManager.custList [randomCust].transform.position = c [randomSeat].transform.position;
			c [randomSeat].occupied = true;
			GameManager.custList [randomCust].GetComponent<CustomerScript> ().isSeated = true;
			AddOrder (1, (int)freeSpacePosition.x, (int)freeSpacePosition.y);
		} else {
			Debug.Log ("No chairs to seat customers");
		}
	}
}
