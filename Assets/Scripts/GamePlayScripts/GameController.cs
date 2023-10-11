using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private ShotCountText shotCountText;
    public Text ballsCountText;

    public GameObject[] block;

    public List<GameObject> levels; //B�l�mleri tutacak olan liste
    private GameObject level1;
    private GameObject level2;

    private Vector2 level1Pos;
    private Vector2 level2Pos;

    public int shotCount;
    public int ballsCount;

    private void Awake()
    {
        shotCountText = GameObject.Find("ShotCountText").GetComponent<ShotCountText>();
        ballsCountText = GameObject.Find("BallCountText").GetComponent<Text>();
    }
    private void Start()
    {
        PlayerPrefs.DeleteKey("Level");
        ballsCount = PlayerPrefs.GetInt("BallsCount", 5);
        ballsCountText.text = ballsCount.ToString();
        Physics2D.gravity = new Vector2(0, -17);
        SpawnLevel();
    }
    private void Update()
    {
        CheckBlocks();
    }
    void SpawnNewLevel(int numberLevel1, int numberLevel2, int min, int max)
    {

        shotCount = 1;

        level1Pos = new Vector2(2.4f, -1);
        level2Pos = new Vector2(2.4f, -6f);

        level1 = levels[numberLevel1];
        level2 = levels[numberLevel2];

        Instantiate(level1, level1Pos, Quaternion.identity);
        Instantiate(level2, level2Pos, Quaternion.identity);

        SetBlocksCount(min, max);

    }
    void SpawnLevel()
    {

        if (PlayerPrefs.GetInt("Level", 0) == 0) //�lk seviye
        {
            SpawnNewLevel(0, 17, 3, 5);
        }
        if (PlayerPrefs.GetInt("Level") == 1)
        {
            SpawnNewLevel(0, 18, 3, 5);
        }
        if (PlayerPrefs.GetInt("Level") == 2)
        {
            SpawnNewLevel(0, 18, 3, 5);
        }
        if (PlayerPrefs.GetInt("Level") == 3)
        {
            SpawnNewLevel(3, 5, 4, 6);
        }
        if (PlayerPrefs.GetInt("Level") == 4)
        {
            SpawnNewLevel(8, 6, 3, 5);
        }
        if (PlayerPrefs.GetInt("Level") == 5)
        {
            SpawnNewLevel(9, 13, 13, 15);
        }
        if (PlayerPrefs.GetInt("Level") == 6)
        {
            SpawnNewLevel(3, 42, 13, 25);
        }
        if (PlayerPrefs.GetInt("Level") == 7)
        {
            SpawnNewLevel(5, 35, 32, 35);
        }
        if (PlayerPrefs.GetInt("Level") == 8)
        {
            SpawnNewLevel(8, 13, 21, 17);
        }
        if (PlayerPrefs.GetInt("Level") == 9)
        {
            SpawnNewLevel(21, 43, 31, 25);
        }
        if (PlayerPrefs.GetInt("Level") == 10)
        {
            SpawnNewLevel(0, 41, 26, 43);
        }
    }
    void SetBlocksCount(int min, int max) //Nu iki de�er aras�nda random bir de�er belirle.
    {
        block = GameObject.FindGameObjectsWithTag("Block");
        for (int i = 0; i < block.Length; i++)
        {
            int count = UnityEngine.Random.Range(min, max);
            block[i].GetComponent<Block>().SetStartingCount(count); //Daha sonra bu de�eri Block scriptindeki Methoda parametre olarak g�nder.
        }
    }
    public void CheckBlocks()
    {
        block = GameObject.FindGameObjectsWithTag("Block");
        if (block.Length < 1)
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            RemoveBalls();
            SpawnLevel();
        }
    }
    void RemoveBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < balls.Length; i++)
        {
            Destroy(balls[i]);
        }
    }
    public void CheckShotCount()
    {
        if (shotCount == 1)
        {
            shotCountText.SetTopText("SHOT");
            shotCountText.SetBottomText("1/3");
            shotCountText.Flash();
        }
        if (shotCount == 2)
        {
            shotCountText.SetTopText("SHOT");
            shotCountText.SetBottomText("2/3");
            shotCountText.Flash();
        }
        if (shotCount == 3)
        {
            shotCountText.SetTopText("FINAL");
            shotCountText.SetBottomText("SHOT");
            shotCountText.Flash();
        }
    }
}
