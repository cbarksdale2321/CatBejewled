using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePeice : MonoBehaviour
{
    [Header("Board Options")]
    public int column, row, targetX, targetY;
    public int previousColumn;
    public int previousRow;
    public bool isMatched = false;
    private FindMatches findMatches;
    private GameObject otherPiece;
    private BoardManager board;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0f;
    public float swipeResist = 1f;

    //Ok, So the temporary sounds might get annoying after a bit, My bad -Dj
    private float PieceResetTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<BoardManager>();
        findMatches = FindObjectOfType<FindMatches>();
        /*
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
        previousRow = row;
        previousColumn = column;
        */

    }

    // Update is called once per frame
    void Update()
    {
       FindMatches();
        if (isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(1f, 1f, 1f, .2f);
        }
        targetX = column;
        targetY = row;

        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allShapes[column,row] != this.gameObject)
            {
                board.allShapes[column, row] = this.gameObject;
                //findMatches.FindAllMatches();
            }
            
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            board.allShapes[column, row] = this.gameObject;
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allShapes[column, row] != this.gameObject)
            {
                
                board.allShapes[column, row] = this.gameObject;
                //findMatches.FindAllMatches();
            }
            
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
          
        }



    }

    public IEnumerator CheckMove()
    {
        yield return new WaitForSeconds(.5f);
        if (otherPiece != null)
        {
            if (!isMatched && !otherPiece.GetComponent<GamePeice>().isMatched)
            {
                otherPiece.GetComponent<GamePeice>().row = row;
                otherPiece.GetComponent<GamePeice>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentState = GameState.Move;

            }
            else
            {
                board.DestroyMatches();
                
            }
            otherPiece = null;
        }
       
        
    }

    private void OnMouseDown()
    {
        if (board.currentState == GameState.Move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
       
    }
    private void OnMouseUp()
    {
        if (board.currentState == GameState.Move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalcAngle();
        }
       
    }

    void CalcAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * (180 / Mathf.PI);

            MovePieces();
            board.currentState = GameState.Wait;
        }
        else
        {
            board.currentState = GameState.Move;
        }


        //This UndoMove might be in the wrong spot. It misfires if the 1st shape is incorrect.  
        // Invoke("UndoMove", PieceResetTime);

    }

    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < (board.width - 1))//right swipe
        {
            otherPiece = board.allShapes[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherPiece.GetComponent<GamePeice>().column -= 1;
            column += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)//up swipe
        {


            otherPiece = board.allShapes[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherPiece.GetComponent<GamePeice>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)//left swipe
        {


            otherPiece = board.allShapes[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherPiece.GetComponent<GamePeice>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)//down swipe
        {
            otherPiece = board.allShapes[column, row - 1];
            previousRow = row;
            previousColumn = column;
            otherPiece.GetComponent<GamePeice>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMove());

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

    /// <summary>
    /// Same as move but reversed. To use we can Invoke it and have a timer.
    /// </summary>
    /// 
    /*
    private void UndoMove()
    {
        if (isMatched ==false)
        {

            if (swipeAngle > -45 && swipeAngle <= 45 && column < (board.width))//right swipe
            {
                otherPiece = board.allShapes[column - 1, row];
                otherPiece.GetComponent<GamePeice>().column += 1;
                column -= 1;
            }
            else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height)//up swipe
            {


                otherPiece = board.allShapes[column, row - 1];
                otherPiece.GetComponent<GamePeice>().row += 1;
                row -= 1;
            }
            else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)//left swipe
            {


                otherPiece = board.allShapes[column + 1, row];
                otherPiece.GetComponent<GamePeice>().column -= 1;
                column += 1;
            }
            else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)//down swipe
            {
                otherPiece = board.allShapes[column, row + 1];
                otherPiece.GetComponent<GamePeice>().row -= 1;
                row += 1;
            }
            FindObjectOfType<AudioManager>().Play("UndoGamePieceMove");
        }

    }
    */

    void FindMatches()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftPiece1 = board.allShapes[column - 1, row];
            GameObject rightPiece1 = board.allShapes[column + 1, row];
            if (leftPiece1 != null && rightPiece1 != null)
            {
                if (leftPiece1.tag == this.gameObject.tag && rightPiece1.tag == this.gameObject.tag)
                {
                    leftPiece1.GetComponent<GamePeice>().isMatched = true;
                    rightPiece1.GetComponent<GamePeice>().isMatched = true;
                    isMatched = true;
                }
            }

        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upPiece1 = board.allShapes[column, row - 1];
            GameObject downPiece1 = board.allShapes[column, row + 1];
            if (upPiece1 != null && downPiece1 != null)
            {


                if (upPiece1.tag == this.gameObject.tag && downPiece1.tag == this.gameObject.tag)
                {
                    upPiece1.GetComponent<GamePeice>().isMatched = true;
                    downPiece1.GetComponent<GamePeice>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }

}