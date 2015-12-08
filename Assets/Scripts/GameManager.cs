using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private const int gridSize = 5;
    
    private int textboxWidth = 150;
    private int textboxHeight = 30;
    private int textboxPosX;
    private int textboxPosY;
    private string textboxMessage = "";
    private bool showTB = false;
    private float showTimeStart;

    private int[,] gridHeight = new int[gridSize,gridSize];
    private bool[,] gridOccupided = new bool[gridSize, gridSize];
    private int[,] gridOwner = new int[gridSize, gridSize];
    public int selectionMode = 0; //0 = none; 1 = unitMove; 2 = spellTarget; 3 = unitPlace; 

	// Use this for initialization
	void Start () {
        // initalize the two grid arrays
        for (int i = 0; i < gridSize; ++i)
        {
            for (int j = 0; j < gridSize; ++j)
            {
                gridHeight[i, j] = 0;
                gridOccupided[i, j] = false;
                gridOwner[i, j] = 0;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        textboxPosX = (int)(Screen.width / 2.0f - textboxWidth / 2.0f);
        textboxPosY = (int)(Screen.height - textboxHeight / 2.0f - 30);

        if (Time.time - showTimeStart > 2.0f) { showTB = false; }
	}

    void OnGUI()
    {
        if (showTB)
        { GUI.Box(new Rect(textboxPosX, textboxPosY, textboxWidth, textboxHeight), textboxMessage); }
    }

    // Acessor fnction to get board size
    public int GetBoardSize() { return gridSize; }

    // Check Height at grid position [x,y]
    public int CheckHeight(int x, int y)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return 0; }
        return gridHeight[x, y];
    }

    // Change Height at grid position [x,y] by d;
    public bool ChangeHeight(int x, int y, int d)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return false; }
        gridHeight[x, y] += d;
        /*
        if (gridHeight[x, y] > 5)
        {
            gridHeight[x, y] = 5;
            return false;
        }
        else if (gridHeight[x, y] < -5)
        {
            gridHeight[x, y] = -5;
            return false;
        }*/
        return true;
    }

    // Check Occupancy of grid postion [x,y]
    public bool CheckOccupy(int x, int y)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return true; }
        return gridOccupided[x, y];
    }

    // Change Occupancy of grid position [x,y] to value of b
    public bool ChangeOccupy(int x, int y, bool b)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return false; }
        gridOccupided[x, y] = b;
        return true;
    }

    // Check the Owner of block at grid position [x,y]
    public int CheckOwner(int x, int y)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return 0; }
        return gridOwner[x, y];
    }

    // Change the Owner of block at grid position [x,y] to o
    // o must be either 0, 1, or 2
    public bool ChangeOwner(int x, int y, int o)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return false; }
        if (o < 0 || o > 2) { return false; }
        gridOwner[x, y] = o;
        return true;
    }

    // Obtain the current selection mode; 0 = none; 1 = unitMove; 2 = spellTarget
    public int GetSelectMode()
    {
        return selectionMode;
    }

    // Change the Selection mode to s
    // s must be either 0, 1, 2, or 3
    public bool ChangeSelectMode(int s)
    {
        if (s < 0 || s > 3) { return false; }
        selectionMode = s;
        return true;
    }

    // Change the message in center textbox
    public void ShowTBMessage(string message)
    {
        textboxWidth = message.Length * 10;
        textboxMessage = message;
        showTB = true;
        showTimeStart = Time.time;
    }
}
