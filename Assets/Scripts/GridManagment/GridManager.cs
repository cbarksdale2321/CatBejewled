using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public ArrayLayout customLayout;
    public Sprite[] peices;
    int width = 8;
    int height = 8;
    Node[,] gameBoard;
    public GameObject nodePeice;
    public RectTransform gameboardRect;

   System.Random rnd;



    void Start()
    {
        StartGame();
    }

    void Update()
    {
        
    }
    private void InitializeBoard()
    {
        gameBoard = new Node[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //gameBoard[x, y] = new Node((customLayout.rows[y].row[y]) ? - 1 : FillPeice(), new Point(x, y));
            }
        }
    }

    void CheckBoard()
    {
        List<int> remove;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Point p = new Point(x, y);
                int val = getValueAtPoint(p);
                if (val <= 0)
                {
                    continue;
                }
                remove = new List<int>();
                while (isConnected(p,true).Count > 0)
                {
                    val = getValueAtPoint(p);
                    if (remove.Contains(val))
                    {
                        remove.Add(val);
                        setValueAtPoint(p, newValue(ref remove));
                    }
                }
            }
        }
    }

    void InstantiateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int val = gameBoard[x, y].value;
                if (val <= 0)
                {
                    continue;
                }
                GameObject p = Instantiate(nodePeice, gameboardRect);
                RectTransform rect = p.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(32 + (64 * x), -32 - (64 * y));
            }
        }
    }
    List<Point> isConnected(Point p, bool main) //checking what shapes are in what direction
    {
        List<Point> connected = new List<Point>();
        int val = getValueAtPoint(p);
        Point[] directions =
        {
            Point.Up,
            Point.Right,
            Point.Down,
            Point.Left
        };


        foreach (Point dir in directions)
        {
            List<Point> line = new List<Point>();

            int same = 0;
            
            for (int i = 0; i < 3; i++)
            {
                Point check = Point.Add(p, Point.Multiply(dir, i));
                if (getValueAtPoint(check) == val)
                {
                    
                    line.Add(check);
                    same++;
                }
            }
            if (same > 1)//if more than one of same shape in a direction then it matches
            {
                AddPoint(ref connected, line);
            }
        }
        for (int i = 0; i < 2; i++)
        {
            List<Point> line = new List<Point>();
            int same = 0;
            Point[] check = { Point.Add(p, directions[i]),Point.Add(p, directions[i + 2]) };
            foreach (Point next in check)
            {
               if (getValueAtPoint(next) == val)
                {
                    line.Add(next);
                    same++;
                }
            }
            if (same > 1)
            {
                AddPoint(ref connected, line);
            }
        }
        for (int i = 0; i < 4; i++)//check 2x2
        {
            List<Point> square = new List<Point>();

            int same = 0;
            int next = i + 1;
            if (next >= 4)
            {
                next -= 4;
            }
            Point[] check = { Point.Add(p, directions[i]), Point.Add(p, directions[next]), Point.Add(p, Point.Add(directions[i], directions[next])) };//checking diagonals
            foreach (Point point in check)
            {
                if (getValueAtPoint(point) == val)
                {
                    square.Add(point);
                    same++;
                }
            }
            if (same > 2)
            {
                AddPoint(ref connected, square);
            }
        }
        if (main) //checks for matches
        {
            for (int i = 0; i < connected.Count; i++)
            {
                AddPoint(ref connected, isConnected(connected[i], false));
            }
        }
        if (connected.Count > 0)
        {
            connected.Add(p);

        }
        return connected;
    }
    void AddPoint(ref List<Point> points, List<Point> addPoint )
    {
        foreach (Point p in addPoint)
        {
            bool add = true;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Equals(p))
                {
                    add = false;
                    break;
                }
            }

            if (add)
            {
                points.Add(p);
            }
        }
    }

    int getValueAtPoint(Point p)
    {
        if (p.x < 0 || p.x >= width || p.y < 0 || p.y >= height)
        {
            return -1;
        }
        return gameBoard[p.x, p.y].value;
    }
    void setValueAtPoint(Point p, int v)
    {
        gameBoard[p.x, p.y].value = v;
    }
    int newValue(ref List<int> remove)
    {
        List<int> avaliable = new List<int>();
        for (int i = 0; i < peices.Length; i++)
        {
            avaliable.Add(i + 1);
        }
            foreach (int i in remove)
            {
                avaliable.Remove(i);
            }
            if (avaliable.Count < 0)
            {
                return 0;
            }
            else
            {
                return avaliable[rnd.Next(0, avaliable.Count)];
            }
        
    }
    int FillPeice()
    {
        int val = 1;
        val = (rnd.Next(0, 100) / (100 / peices.Length) + 1);
        return val;
    }
    void StartGame()
    {
        
        string seed = GetRandomSeed();
        rnd = new System.Random(seed.GetHashCode());

        InitializeBoard();
        CheckBoard();
    }

    private string GetRandomSeed()
    {
        string seed = "";
        string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYXabcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()";
        for (int i = 0; i < 20; i++)
        {
            seed += allowedChars[UnityEngine.Random.Range(0, allowedChars.Length)];
        }
        return seed;
    }
}
