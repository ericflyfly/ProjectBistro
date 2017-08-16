using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

	OrderScript os;

	void Start(){
		StartCoroutine (seatRandomCust());
		os = GameObject.FindGameObjectWithTag ("OrderHandler").GetComponent<OrderScript> ();
	}

	IEnumerator seatRandomCust(){
		for (int i = 0; i < 5; i++) {
			yield return new WaitForSeconds (5.0f);
			os.AssignRandomSeat ();
		}
	}
}
