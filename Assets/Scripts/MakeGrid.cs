using UnityEngine;
using System.Collections;

public class MakeGrid : MonoBehaviour {

	public GameObject gridComponent;
	public int uniqueIDNumber = 0;

	GameObject[,] tiles;
	// Use this for initialization
	void Start () {
		//print ("I AM BEING CALLED");
		//print ("I AM THING " + uniqueIDNumber);
		tiles = new GameObject[5, 5];

		for(int i = 0; i < 5; i++)
		{
			for(int j = 0; j < 5; j++)
			{

				GameObject temp = Instantiate(gridComponent);
				tiles[i, j] = temp;
				//gridComponent.
				//tiles[i, j].transform.parent = this.transform.parent;
				tiles[i, j].transform.localPosition = (new Vector3( i * 1.05f, 0f, j * 1.05f));
				//tiles[i,j].GetComponent(Tile).SendMessage("SendPos", i, j);
				object[] pos = new object[2];
				pos[0] = i;
				pos[1] = j;
				tiles[i,j].SendMessage("setPos", pos);
				//print("Cube " + i + ", " + j + " is at position: " + tiles[i,j].transform.position);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
