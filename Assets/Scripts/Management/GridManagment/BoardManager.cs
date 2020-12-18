using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State Machine
/// </summary>
/// 
public  enum GameState
{
    Wait,
    Move
}



public class BoardManager : MonoBehaviour
{
    public GameState currentState = GameState.Move;
    public int width;
    public int height;
    public int offset;
    private BackgroundTile[,] allTiles;
    public GameObject[] gamePeices;
    public GameObject tilePrefab;
    public GameObject[,] allShapes;
    [SerializeField] private GameObject scoreManager;
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
                Vector2 tempPostition = new Vector2(x, y + offset);
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
                peice.GetComponent<GamePeice>().row = y;
                peice.GetComponent<GamePeice>().column = x;
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
            ScoreManager.IncrementScore(1);
            EndGameManager.moves = EndGameManager.moves - 1;
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
        StartCoroutine(DecreaseRow());

    }
    private IEnumerator DecreaseRow()
    {
        int nullCount = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allShapes[x,y] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    allShapes[x, y].GetComponent<GamePeice>().row -= nullCount;
                    allShapes[x, y] = null;
                }
            }
            nullCount = 0;
        }
       yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoard());
    }

    private void RefillBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allShapes[x,y] == null)
                {
                    Vector2 tempPosition = new Vector2(x, (y + offset) * Time.deltaTime);
                    int pieceToUse = Random.Range(0, gamePeices.Length);
                    GameObject piece = Instantiate(gamePeices[pieceToUse], tempPosition, Quaternion.identity);
                    allShapes[x, y] = piece;
                    piece.GetComponent<GamePeice>().row = y;
                    piece.GetComponent<GamePeice>().column = x;
                    piece.transform.SetParent(this.transform);

                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allShapes[x, y] != null)
                {
                    if (allShapes[x,y].GetComponent<GamePeice>().isMatched)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private IEnumerator FillBoard()
    {
        RefillBoard();
        yield return new WaitForSeconds(.5f);

        while (MatchesOnBoard())
        {
            ScoreManager.IncrementMultiplier();
            PlayRandomMeow();
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
        ScoreManager.ResetMultiplier();
        yield return new WaitForSeconds(.5f);
        currentState = GameState.Move;
    }

    void PlayRandomMeow()
    {
        var random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                FindObjectOfType<AudioManager>().Play("GamePieceMove");
                break;
            case 1:
                FindObjectOfType<AudioManager>().Play("UndoGamePieceMove");
                break;
            case 2:
                FindObjectOfType<AudioManager>().Play("CatMeow3");
                break;

            case 3:
                FindObjectOfType<AudioManager>().Play("CatMeow4");
                break;
            case 4:
                FindObjectOfType<AudioManager>().Play("CatMeow5");
                break;

        }
    }
}


