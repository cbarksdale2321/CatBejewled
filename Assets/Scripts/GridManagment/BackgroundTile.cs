using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    public GameObject[] gamePeices;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize()
    {
        int peiceToUse = Random.Range(0, gamePeices.Length);
        GameObject peice = Instantiate(gamePeices[peiceToUse], transform.position, Quaternion.identity);
        peice.transform.parent = this.transform;
        peice.name = this.gameObject.name;
    }
}
