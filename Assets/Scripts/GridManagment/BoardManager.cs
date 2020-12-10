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
                GameObject peice = Instantiate(gamePeices[peiceToUse], tempPostition, Quaternion.identity);
                peice.transform.parent = this.transform;
                peice.name = "( " + x + ", " + y + " )";
                allShapes[x, y] = peice;
            }
        }
    }
}
