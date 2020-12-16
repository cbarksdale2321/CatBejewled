using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour
{
    private BoardManager board;
    public List<GameObject> currentMatches = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<BoardManager>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private IEnumerator FindAllMatchesCo()
    {

        yield return new WaitForSeconds(.1f);
        for (int x = 0; x < board.width; x++)
        {
            for (int y = 0; y < board.height; y++)
            {
                GameObject currentPiece = board.allShapes[x, y];
                if (currentPiece != null)
                {
                    if (x > 0 && x < board.width - 1)
                    {
                        GameObject leftPiece = board.allShapes[x - 1, y];
                        GameObject rightPiece = board.allShapes[x - 1, y];
                        if (leftPiece != null && rightPiece != null)
                        {
                            if (leftPiece.tag == currentPiece.tag && rightPiece.tag == currentPiece.tag)
                            {
                                leftPiece.GetComponent<GamePeice>().isMatched = true;
                                rightPiece.GetComponent<GamePeice>().isMatched = true;
                                currentPiece.GetComponent<GamePeice>().isMatched = true;
                            }
                        }
                    }
                    if (y > 0 && y < board.height - 1)
                    {
                        GameObject upPiece = board.allShapes[x, y + 1];
                        GameObject downPiece = board.allShapes[x, y - 1];
                        if (upPiece != null && downPiece != null)
                        {
                            if (upPiece.tag == currentPiece.tag && downPiece.tag == currentPiece.tag)
                            {
                                upPiece.GetComponent<GamePeice>().isMatched = true;
                                downPiece.GetComponent<GamePeice>().isMatched = true;
                                currentPiece.GetComponent<GamePeice>().isMatched = true;
                            }
                        }
                    }
                }
               

            }
        }
    }
}

