using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour {

	public List<TileScript> currentPath = null;

	public int x;
	public int y;

	// Use this for initialization
	void Start () {
		currentPath = new List<TileScript> ();
		x = (int)transform.position.x;
		y = (int)transform.position.z;


	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("x")) {
			GeneratePathTo (7, 6);
		}

		// Draw our debug line showing the pathfinding!
		// NOTE: This won't appear in the actual game view.
		if(currentPath != null) {
			int currNode = 0;

			while( currNode < currentPath.Count-1 ) {

				Vector3 start = new Vector3( currentPath[currNode].x, 0, currentPath[currNode].y) + 
					new Vector3(0.5f, 0, 0.5f) ;
				Vector3 end   = new Vector3(currentPath[currNode + 1].x, 0, currentPath[currNode + 1].y)  + 
					new Vector3(0.5f, 0, 0.5f) ;

				Debug.DrawLine(start, end, Color.red);

				currNode++;
			}
		}
	}

	// Based on Dijkstra's Algorithm for graph navigation
	public void GeneratePathTo(int x, int y) {
		
		// Clear out our unit's old path.
		this.currentPath = null;

		// This means that if there is an item there (table/chair) the waiter cannot walk there
		if (GameManager.mapArray [x, y].itemSet == true || GameManager.mapArray[x,y].chosen == false) {
			return;
		}

		Dictionary<TileScript, float> dist = new Dictionary<TileScript, float> ();
		Dictionary<TileScript, TileScript> prev = new Dictionary<TileScript, TileScript> ();

		// Setup the "Q"; the list of TileScript	s we haven't checked yet.
		List<TileScript> unvisited = new List<TileScript>();

		TileScript source = GameManager.mapArray[this.x, this.y];

		TileScript target = GameManager.mapArray[x, y];

		dist[source] = 0;
		prev[source] = null;

		// Initialize everything to have INFINITY distance, since
		// we don't know any better right now. Also, it's possible
		// that some nodes CAN'T be reached from the source,
		// which would make INFINITY a reasonable value
		foreach(TileScript v in GameManager.mapArray) {
			if(v != source) {
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}

			unvisited.Add(v);
		}

		while(unvisited.Count > 0) {
			// "u" is going to be the unvisited node with the smallest distance.
			TileScript u = null;

			foreach(TileScript possibleU in unvisited) {
				if(u == null || dist[possibleU] < dist[u]) {
					u = possibleU;
				}
			}

			if(u == target) {
				break;	// Exit the while loop!
			}

			unvisited.Remove(u);

			foreach(TileScript v in u.neighbours) {
				if (v.chosen && !v.itemSet) {
					float alt = dist [u] + u.DistanceTo (v);
					//Debug.Log (alt + " at " + v.x + ", " + v.y);
					//float alt = dist[u];
					if (alt < dist [v]) {
						dist [v] = alt;
						prev [v] = u;
					}
				}
			}
		}

		// If we get there, the either we found the shortest route
		// to our target, or there is no route at ALL to our target.

		if(prev[target] == null) {
			// No route between our target and the source
			return;
		}

		List<TileScript> currentPath = new List<TileScript>();

		TileScript curr = target;

		// Step through the "prev" chain and add it to our path
		while(curr != null) {
			currentPath.Add(curr);
			curr = prev[curr];
		}

		// Right now, currentPath describes a route from out target to our source
		// So we need to invert it!
		currentPath.Reverse();

		this.currentPath = currentPath;

		Debug.Log ("Generated");
	}
}
