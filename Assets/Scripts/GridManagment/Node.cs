using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Node
{
    public int value; //determines what sprite is used
    public Point index;

    public Node(int v, Point i)
    {
        value = v;
        index = i;
    }
}
