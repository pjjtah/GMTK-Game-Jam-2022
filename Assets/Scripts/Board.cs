using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tetronimo;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public float tickspeed;
    public float lastTick;
    
    public int[,] board;
    public SpriteRenderer[,] boardSprites;
    public Sprite[] diceSprites;
    public Vector3Int spawnPosition;

    public Vector3Int tetronimoPosition;
    public Vector3Int[] tetronimoCells;
    public TetronimoShape shape;

    public TetronimoData[] tetronimoes;
    public SpriteRenderer renderModel;

    public Stack changeStack;


    // Start is called before the first frame update
    void Start()
    {
        changeStack = new Stack();

        tetronimoCells = new Vector3Int[4];

        board = new int[5,9];
        boardSprites = new SpriteRenderer[5,9];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                board[i, j] = 0;
                boardSprites[i,j] = Instantiate(renderModel, new Vector3(i * 0.857f, j * -0.857f, 0), transform.rotation);
            }
        }

        SpawnTetromino();
        lastTick = Time.deltaTime;
        Draw();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bool bounds = true;
            for (int i = 0; i < 4; i++)
            {
                Vector3Int position = tetronimoCells[i] + tetronimoPosition;
                Vector3Int newPos = new Vector3Int(position.x -1, position.y, 0);
                if(newPos.x < 0)
                {
                    bounds = false;
                    break;
                }

            }
            if (bounds)
            {
                Debug.Log("SS");
                for (int j = 0; j < 4; j++)
                {
                    Vector3Int position = tetronimoCells[j] + tetronimoPosition;
                    board[position.x, position.y] = 0;
                }
                tetronimoPosition.x = tetronimoPosition.x - 1;
                Set();
                Draw();
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            bool bounds = true;
            for (int i = 0; i < 4; i++)
            {
                Vector3Int position = tetronimoCells[i] + tetronimoPosition;
                Vector3Int newPos = new Vector3Int(position.x + 1, position.y, 0);
                if (newPos.x > 4)
                {
                    bounds = false;
                    break;
                }

            }
            if (bounds)
            {
                Debug.Log("SS");
                for (int j = 0; j < 4; j++)
                {
                    Vector3Int position = tetronimoCells[j] + tetronimoPosition;
                    board[position.x, position.y] = 0;
                }
                tetronimoPosition.x = tetronimoPosition.x + 1;
                Set();
                Draw();
            }

        }

        if (Time.time > lastTick + tickspeed)
        {
            Tick();
            lastTick = Time.time;
        }
    }

    private void Tick()
    {
            bool ground = false;
            for (int i = 0; i < 4; i++)
            {
                Vector3Int position = tetronimoCells[i] + tetronimoPosition;
                //dice has hit ground
                if (position.y == 8)
                {
                    ground = true;
                    CheckLines();
                    SpawnTetromino();
                    break;
                }
                Vector3Int newPos = new Vector3Int(position.x, position.y + 1, 0);
            //check if new position empty
                if (board[newPos.x, newPos.y] != 0)
                {
                    ground = true;
                    for (int j = 0; j < 4; j++)
                    {
                        Debug.Log("new " + newPos);
                        Debug.Log("old " + tetronimoCells[j] + tetronimoPosition);
                        //check if its part of current piece
                        if (tetronimoCells[j].x + tetronimoPosition.x == newPos.x && tetronimoCells[j].y + tetronimoPosition.y == newPos.y)
                        {
                            ground = false;
                            break;
                        }
                    }
                    //dice has hit another dice
                    if (ground)
                    {
                        CheckLines();
                        SpawnTetromino();
                    break;
                }
                }
            }
            if (!ground)
            {
                for (int j = 0; j < 4; j++)
                {
                    Vector3Int position = tetronimoCells[j] + tetronimoPosition;
                    board[position.x, position.y] = 0;
                }
                tetronimoPosition.y = tetronimoPosition.y + 1;
                Set();
            }
        Draw();
    }

    private void Draw()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                boardSprites[i, j].sprite = diceSprites[board[i, j]];
            }
        }
    }

    private void CheckLines()
    {
        HashSet<int> fullSet = new HashSet<int>();
        for (int j = 0; j < 4; j++)
        {
            bool full = true;
            for (int i = 0; i < 5; i++)
            {
                if (board[i, tetronimoCells[j].y] == 0)
                {
                    full = false;
                    break;
                }
            }
            if (full)
            {
                fullSet.Add(tetronimoCells[j].y);
            }
        }
    }

    private void SpawnTetromino()
    {
        int random = Random.Range(0, tetronimoes.Length);
        tetronimoPosition = spawnPosition;
        tetronimoCells = tetronimoes[random].positions;
        for (int i = 0; i < 4; i++)
        {
            tetronimoCells[i].z = Random.Range(1, 7);
        }
        Set();
    }

    public void Set()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3Int position = tetronimoCells[i] + tetronimoPosition;
            board[position.x, position.y] = position.z;
        }
    }

    private void DropEverything(int v, object y)
    {
        throw new NotImplementedException();
    }
}
