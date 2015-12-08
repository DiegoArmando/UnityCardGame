using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private const int gridSize = 5;

    public int[,] gridHeight = new int[gridSize,gridSize];
    public bool[,] gridOccupided = new bool[gridSize, gridSize];

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
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Check Height at grid position [x,y]
    public int CheckHeight(int x, int y)
    {
        if (x < 0 || x > gridSize || y < 0 || y > gridSize) { return 0; }
        return gridHeight[x, y];
    }

    // Change Height at grid position [x,y];
    // d = amount of change in height
    // returns false if height goes above 5 or below -5
    public bool ChangeHeight(int x, int y, int d)
    {
        if (x < 0 || x > gridSize || y < 0 || y > gridSize) { return false; }
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
        if (x < 0 || x > gridSize || y < 0 || y > gridSize) { return true; }
        return gridOccupided[x, y];
    }

    // Change Occupancy of grid position [x,y] to value of b
    public bool ChangeOccupy(int x, int y, bool b)
    {
        if (x < 0 || x > gridSize || y < 0 || y > gridSize) { return false; }
        gridOccupided[x, y] = b;
        return true;
    }

    // Acessor fnction to get board size
    public int GetBoardSize() { return gridSize; }
}
