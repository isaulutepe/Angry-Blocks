using System.Collections;
using System.Collections.Generic;
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
        PlayerPrefs.DeleteAll();

        ballsCount = PlayerPrefs.GetInt("BallsCount", 5);
        ballsCountText.text = ballsCount.ToString();

        Physics2D.gravity = new Vector2(0, -17);

        SpawnLevel();
    }
    private void Update()
    {
        if (ballContainer.transform.childCount == 0 && shotCount == 4) //Aktif top kalmadýysa ve 3 atýþ yapýldýysa oyunu bitir.
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

        if (PlayerPrefs.GetInt("Level") == 8)
            SpawnNewLevel(18, 6, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 9)
            SpawnNewLevel(20, 8, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 10)
            SpawnNewLevel(31, 16, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 11)
            SpawnNewLevel(28, 3, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 12)
            SpawnNewLevel(24, 12, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 13)
            SpawnNewLevel(33, 32, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 14)
            SpawnNewLevel(44, 15, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 15)
            SpawnNewLevel(19, 18, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 16)
            SpawnNewLevel(28, 27, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 17)
            SpawnNewLevel(24, 25, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 18)
            SpawnNewLevel(37, 33, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 19)
            SpawnNewLevel(25, 4, 9, 15);

        if (PlayerPrefs.GetInt("Level") == 20)
            SpawnNewLevel(33, 35, 9, 15);

        int random1, random2, rndmin, rndmax;
        random1 = Random.Range(0, 44);
        random2 = Random.Range(0, 44);
        rndmin = Random.Range(1, 9);
        rndmax = Random.Range(4, 18);

        if (random1 == random2) //Ayný sayi üretilirse random 2 deðiþecek.
        {
            random2 = Random.Range(0, 44);
        }
        if (PlayerPrefs.GetInt("Level") > 20) //Level 20 den büyükse artýk random bölüm oluþturacak. Oyun son bulmasýn diye.
        {
            SpawnNewLevel(random1, random2, rndmin, rndmax);
        }

    }
    void SetBlocksCount(int min, int max) //Nu iki deðer arasýnda random bir deðer belirle.
    {
        block = GameObject.FindGameObjectsWithTag("Block");

        for (int i = 0; i < block.Length; i++)
        {
            int count = Random.Range(min, max);
            block[i].GetComponent<Block>().SetStartingCount(count); //Daha sonra bu deðeri Block scriptindeki Methoda parametre olarak gönder.
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

            //Top sayýsýnda deðiþme oldugunda kaydedilmesi için, ekstra top ile devam edilebilmesi için.
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
