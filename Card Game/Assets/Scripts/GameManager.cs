using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private const int gridSize = 5;

    public int[,] gridHeight = new int[gridSize,gridSize];
    public bool[,] gridOccupided = new bool[gridSize, gridSize];

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
        return gridHeight[x, y];
    }

    // Change Height at grid position [x,y];
    // d = amount of change in height
    // returns false if height goes above 5 or below -5
    public bool ChangeHeight(int x, int y, int d)
    {
        gridHeight[x, y] += d;
        if (gridHeight[x, y] > 5)
        {
            gridHeight[x, y] = 5;
            return false;
        }
        else if (gridHeight[x, y] < -5)
        {
            gridHeight[x, y] = -5;
            return false;
        }
        return true;
    }
}
