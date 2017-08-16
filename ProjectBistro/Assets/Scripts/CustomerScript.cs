using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour {

	public int ID;
	public Material[] mat;
	public TileScript currentTile;

	public void StartCustomer(){
		/*mat = gameObject.GetComponentsInChildren<Material> ();
		Color newCol =  Random.ColorHSV ();
		foreach (Material m in mat) {
			m.color = newCol;
		}*/
	}

	public void RefreshCustomer(){
		/*Color newCol =  Random.ColorHSV ();
		foreach (Material m in mat) {
			m.color = newCol;
		}*/
	}

	public void EnterShop(){
		
	}

	public void TakeASeat(){
		//Order as soon as seat is taken with OrderScript.AddOrder
	}
}
