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
		//TODO: give the order to the closest free waiter. 
	}
}
