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

	public void doSpell(int spell, int x, int y)
	{
		switch (spell) {
		case 0:
			raise (x, y);
			break;
		case 1:
			lower (x,y);
			break;
		case 2:
			level (x,y);
			break;
		case 3:
			fist (x,y);
			break;
		}
	}

	public void raise(int x, int y)
	{
		//CHANGE THIS TO THE GM
		((GameManager)GetComponent ("GameManager")).ChangeHeight (x, y, 1);
		((Tile)tiles [x, y].GetComponent ("Tile")).ChangeTileHeight (((Tile)tiles [x, y].GetComponent ("Tile")).height + 1);
	}

	public void lower(int x, int y)
	{
		((GameManager)GetComponent ("GameManager")).ChangeHeight (x, y, -1);
		((Tile)tiles [x, y].GetComponent ("Tile")).ChangeTileHeight (((Tile)tiles [x, y].GetComponent ("Tile")).height - 1);
	}

	public void level(int x, int y)
	{
		for (int i = -1; i < 2; i++)
		{
			for(int j = -1; j < 2; j++)
			{
				int heightDif = ( ((Tile)tiles [x, y].GetComponent ("Tile")).height - ((Tile)tiles [x + i, y + j].GetComponent ("Tile")).height) * 2;
				((GameManager)GetComponent ("GameManager")).ChangeHeight (x, y, heightDif);
			}
		}
	}

	public void fist(int x, int y)
	{
		for (int i = -1; i < 2; i++)
		{
			for(int j = -1; j < 2; j++)
			{
				((GameManager)GetComponent ("GameManager")).ChangeHeight (x, y, 1);
				if(x == 0 && y == 0) ((GameManager)GetComponent ("GameManager")).ChangeHeight (x, y, 1);
			}
		}
	}

	public bool checkValid(int x, int y, int owner)
	{

		if(((GameManager)GetComponent ("GameManager")).CheckOwner(x, y + 1) == owner)
		{
			return true;
		}
		if(((GameManager)GetComponent ("GameManager")).CheckOwner(x, y - 1) == owner)
		{
			return true;
		}
		if(((GameManager)GetComponent ("GameManager")).CheckOwner(x + 1, y) == owner)
		{
			return true;
		}
		if(((GameManager)GetComponent ("GameManager")).CheckOwner(x - 1, y) == owner)
		{
			return true;
		}

		return false;
	}
}
