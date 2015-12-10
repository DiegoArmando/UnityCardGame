using UnityEngine;
using System.Collections;

public class MakeGrid : MonoBehaviour {

	public GameObject gridComponent;
	public int uniqueIDNumber = 0;
    public GameManager gm;

	GameObject[,] tiles;
	// Use this for initialization
	void Start () {
		gm = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");

		tiles = new GameObject[gm.GetBoardSize(), gm.GetBoardSize()];

		for(int i = 0; i < gm.GetBoardSize(); i++)
		{
			for(int j = 0; j < gm.GetBoardSize(); j++)
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
            gm.useAction();
			break;
		case 1:
			lower (x,y);
            gm.useAction();
			break;
		case 2:
			level (x,y);
            gm.useAction();
			break;
		case 3:
			fist (x,y);
            gm.useAction();
			break;
		}
	}

	public void raise(int x, int y)
	{
		//CHANGE THIS TO THE GM
		((GameManager)GetComponent ("GameManager")).ChangeHeight (x, y, 1);
		((Tile)tiles [x, y].GetComponent ("Tile")).ChangeTileHeight (1);
	}

	public void lower(int x, int y)
	{
		((GameManager)GetComponent ("GameManager")).ChangeHeight (x, y, -1);
		((Tile)tiles [x, y].GetComponent ("Tile")).ChangeTileHeight (-1);
	}

	public void level(int x, int y)
	{
        int height = gm.CheckHeight(x, y);
		for (int i = -1; i < 2; i++)
		{
			for(int j = -1; j < 2; j++)
			{
                if (x + i == x && y + j == y) { continue; }
                if (x + i >= 0 && x + i < gm.GetBoardSize() && y + j >= 0 && y + j < gm.GetBoardSize())
                {
                    ((Tile)tiles[x + i, y + j].GetComponent("Tile")).ChangeTileHeight(height-gm.CheckHeight(x+i,y+j));
                    ((GameManager)GetComponent("GameManager")).ChangeHeight(x + i, y + j, height-gm.CheckHeight(x+i,y+j));
                }
			}
		}
	}

	public void fist(int x, int y)
	{
		for (int i = -1; i < 2; i++)
		{
			for(int j = -1; j < 2; j++)
			{
                if (x+i >= 0 && x+i < gm.GetBoardSize() && y+j >= 0 && y+j < gm.GetBoardSize())
                {
                    ((Tile)tiles[x+i, y+j].GetComponent("Tile")).ChangeTileHeight(1);
                    ((GameManager)GetComponent("GameManager")).ChangeHeight(x+i, y+j, 1);
                }
			}
		}
        if (x >= 0 && x < gm.GetBoardSize() && y >= 0 && y < gm.GetBoardSize())
        {
            ((Tile)tiles[x, y].GetComponent("Tile")).ChangeTileHeight(1);
            ((GameManager)GetComponent("GameManager")).ChangeHeight(x, y, 1);
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
        if (((GameManager)GetComponent("GameManager")).CheckOwner(x, y) == owner)
        {
            return true;
        }

		return false;
	}
}
