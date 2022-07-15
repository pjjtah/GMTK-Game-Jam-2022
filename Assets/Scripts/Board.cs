using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tetronimo;

public class Board : MonoBehaviour
{
    public float tickspeed;
    public float lastTick;
    
    public int[,] board;
    public SpriteRenderer[] boardSprites;
    public Vector2Int spawnPosition;

    public Vector2Int tetronimoPosition;
    public TetronimoShape shape;

    public TetronimoData[] tetronimeos;
    public SpriteRenderer renderModel;


    // Start is called before the first frame update
    void Start()
    {
        board = new int[5,8];
        boardSprites = new SpriteRenderer[40];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                boardSprites[i * j] = Instantiate(renderModel, new Vector3(i * 1.1428f, j * -1.1428f, 0), transform.rotation);
 

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.deltaTime > lastTick + tickspeed)
        {
            Tick();
        }
    }

    private void Tick()
    {
        
    }
}
