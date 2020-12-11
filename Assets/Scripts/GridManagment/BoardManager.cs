using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    public int width;
    public int height;
    private BackgroundTile[,] allTiles;
    public GameObject[] gamePeices;
    public GameObject tilePrefab;
    public GameObject[,] allShapes;
    // Start is called before the first frame update
    void Start()
    {
        allTiles = new BackgroundTile[width, height];
        allShapes = new GameObject[width, height];

        Setup();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Setup()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 tempPostition = new Vector2(x, y);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPostition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + x + ", " + y + " )";

                int peiceToUse = Random.Range(0, gamePeices.Length);
                int maxIterations = 0;

                while (MatchesAt(x, y, gamePeices[peiceToUse]) && maxIterations < 100)
                {
                    peiceToUse = Random.Range(0, gamePeices.Length);
                    maxIterations++;
                }
                maxIterations = 0;

                GameObject peice = Instantiate(gamePeices[peiceToUse], tempPostition, Quaternion.identity);
                peice.transform.parent = this.transform;
                peice.name = "( " + x + ", " + y + " )";
                allShapes[x, y] = peice;
            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allShapes[column - 1, row].tag == piece.tag && allShapes[column - 2, row].tag == piece.tag)
            {
                return true;
            }
            if (allShapes[column, row - 1].tag == piece.tag && allShapes[column, row - 2].tag == piece.tag)
            {
                return true;
            }
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allShapes[column, row - 1].tag == piece.tag && allShapes[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (allShapes[column - 1, row].tag == piece.tag && allShapes[column - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void DestroyMatchAt(int column, int row)
    {
        if (allShapes[column,row].GetComponent<GamePeice>().isMatched)
        {
            Destroy(allShapes[column, row]);
            allShapes[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allShapes[x,y] != null)
                {
                    DestroyMatchAt(x, y);
                }
            }
        }

    }
}


