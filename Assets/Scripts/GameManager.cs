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

    public int[,] gridHeight = new int[gridSize,gridSize];
    public bool[,] gridOccupided = new bool[gridSize, gridSize];
    private int[,] gridOwner = new int[gridSize, gridSize];
    public int selectionMode = 0; //0 = none; 1 = unitMove; 2 = spellTarget; 3 = unitPlace; 
    public int playerTurn = 0; // 0 = neither; 1 = player1; 2 = player2
	//public bool hasSelection = false;

	public GameObject currentHand;

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
		currentHand = GameObject.Find ("P1Hand");
		playerTurn = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
        textboxPosX = (int)(Screen.width / 2.0f - textboxWidth / 2.0f);
        textboxPosY = (int)(Screen.height - textboxHeight / 2.0f - 30);

        if (Time.time - showTimeStart > 2.0f) { showTB = false; }

        if (Input.GetKeyDown(KeyCode.T))
        {
            switch (playerTurn) {
                case 0:
                    SwitchTurns(1);
                    break;
                case 1:
                    SwitchTurns(2);
                    break;
                case 2:
                    SwitchTurns(1);
                    break;
                default:
                    print("Invalid turn ID");
                    break;
            }
        }
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
        if (x < 0 || x > gridSize || y < 0 || y > gridSize) { return 0; }
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return 0; }
        return gridHeight[x, y];
    }

    // Change Height at grid position [x,y];
    // d = amount of change in height
    // returns false if height goes above 5 or below -5
    // Change Height at grid position [x,y] by d;
    public bool ChangeHeight(int x, int y, int d)
    {
        if (x < 0 || x > gridSize || y < 0 || y > gridSize) { return false; }
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return false; }
        gridHeight[x, y] += d;
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
    // 0 = neither; 1 = player1; 2 = player2
    public bool ChangeOwner(int x, int y, int o)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize) { return false; }
        if (o < 0 || o > 2) { return false; }
        gridOwner[x, y] = o;
        return true;
    }

    // Obtain the current selection mode; 0 = none; 1 = unitMove; 2 = spellTarget; 3 = unitPlace
    public int GetSelectMode()
    {
        return selectionMode;
    }

    // Change the Selection mode to s
    // s must be either 0, 1, 2, or 3
    // 0 = none; 1 = unitMove; 2 = spellTarget; 3 = unitPlace
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

    // Get the which player's turn it is
    // 0 = neither; 1 = player1; 2 = player2
    public int GetWhoseTurn()
    {
        return playerTurn;
    }

    // Switches the player's turn; t = whose turn it is
    // 0 = neither(Start/End Game); 1 = player1; 2 = player2;
    public void SwitchTurns(int t)
    {
        if (t >= 0 && t <= 2) { 
            playerTurn = t;
            ShowTBMessage("It is Player " + t + "'s turn.");
        }
    }

}
