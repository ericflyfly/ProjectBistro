using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public static MapGenerator instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	public Transform tilePrefab;
	public Vector2 mapSize;

	[Range(0,1)]
	public float outlinePercent;

	void Awake(){
		//Check if instance already exists
		if (instance == null)
			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);  

		DontDestroyOnLoad (gameObject);
	}

	public void GenerateMap() {
		GameManager.mapArray = new TileScript[(int)mapSize.x,(int)mapSize.y];

		string holderName = "Generated Map";
		if (transform.Find (holderName)) {
			DestroyImmediate(transform.Find(holderName).gameObject);
		}

		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.parent = transform;

		for (int x = 0; x < mapSize.x; x ++) {
			for (int y = 0; y < mapSize.y; y ++) {
				//Vector3 tilePosition = new Vector3(-mapSize.x/2 +0.5f + x, 0, -mapSize.y/2 + 0.5f + y);

				//Instantiate new tiles for every x and y value and set it to 0.1 scale to fit 1 unit square
				Vector3 tilePosition = new Vector3(x + 0.5f, 0, y + 0.5f); //Ofset tile location by 0.5f
				Transform newTile = Instantiate (tilePrefab, tilePosition, Quaternion.identity) as Transform; 
				newTile.localScale = new Vector3(0.1f,0.1f,0.1f) * (1-outlinePercent);
				newTile.parent = mapHolder;

				GameManager.mapArray [x, y] = newTile.GetComponent<TileScript>(); //Add each tile instantiated to the mapArray array. 
			}
		}

	}

}