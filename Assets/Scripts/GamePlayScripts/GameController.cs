using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private ShotCountText shotCountText;

    public Text ballsCountText;

    public GameObject[] block;

    public List<GameObject> levels;

    private GameObject level1;
    private GameObject level2;

    private Vector2 level1Pos;
    private Vector2 level2Pos;

    public int shotCount;
    public int score;
    public int ballsCount;

    private GameObject ballContainer;
    public GameObject gameOver; //GameOver paneli

    private bool firstShot;

    private void Awake()
    {
        shotCountText = GameObject.Find("ShotCountText").GetComponent<ShotCountText>();
        ballsCountText = GameObject.Find("BallCountText").GetComponent<Text>();
        ballContainer = GameObject.Find("BallsContainer");
    }
    private void Start()
    {
        gameOver.SetActive(false);
        PlayerPrefs.DeleteKey("Level");

        ballsCount = PlayerPrefs.GetInt("BallsCount", 5);
        ballsCountText.text = ballsCount.ToString();

        Physics2D.gravity = new Vector2(0, -17);

        SpawnLevel();
    }
    private void Update()
    {
        if (ballContainer.transform.childCount == 0 && shotCount == 4) //Aktif top kalmad�ysa ve 3 at�� yap�ld�ysa oyunu bitir.
        {
            gameOver.SetActive(true);
        }
        if (shotCount > 2)
        {
            firstShot = false;
        }
        else
        {
            firstShot = true;
        }

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
        if (PlayerPrefs.GetInt("Level", 0) == 0)
            SpawnNewLevel(0, 17, 3, 5);

        if (PlayerPrefs.GetInt("Level") == 1)
            SpawnNewLevel(1, 18, 3, 5);

        if (PlayerPrefs.GetInt("Level") == 2)
            SpawnNewLevel(2, 19, 3, 6);

        if (PlayerPrefs.GetInt("Level") == 3)
            SpawnNewLevel(5, 20, 4, 7);

        if (PlayerPrefs.GetInt("Level") == 4)
            SpawnNewLevel(12, 28, 5, 8);

        if (PlayerPrefs.GetInt("Level") == 5)
            SpawnNewLevel(14, 29, 7, 10);

        if (PlayerPrefs.GetInt("Level") == 6)
            SpawnNewLevel(15, 30, 6, 12);

        if (PlayerPrefs.GetInt("Level") == 7)
            SpawnNewLevel(16, 31, 9, 15);
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
            RemoveBalls();
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            SpawnLevel();

            //Top say�s�nda de�i�me oldugunda kaydedilmesi i�in, ekstra top ile devam edilebilmesi i�in.
            if (ballsCount >= PlayerPrefs.GetInt("BallsCount", 5))
                PlayerPrefs.SetInt("BallsCount", ballsCount);
            
            if (firstShot)
            {
                score += 5;
            }
            else
            {
                score += 3;
            }
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
