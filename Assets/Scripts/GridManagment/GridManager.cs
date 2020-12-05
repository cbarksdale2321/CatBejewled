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

   System.Random rnd;



    void Start()
    {
        
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
                gameBoard[x, y] = new Node((customLayout.rows[y].row[y])? -1 : FillPeice(), new Point(x, y));
            }
        }
    }

    void CheckBoard()
    {
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
            }
        }
    }
    List<Point> isConnected(Point p, bool main)
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
            if (same > 1)
            {
                
            }
        }
    }

    int getValueAtPoint(Point p)
    {
        return gameBoard[p.x, p.y].value;
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
