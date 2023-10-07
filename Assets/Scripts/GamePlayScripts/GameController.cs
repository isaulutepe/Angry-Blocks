using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] block;

    public List<GameObject> levels; //Bölümleri tutacak olan liste
    private GameObject level1;
    private GameObject level2;

    private Vector2 level1Pos;
    private Vector2 level2Pos;


    private void Start()
    {
        PlayerPrefs.DeleteAll();
        Physics2D.gravity = new Vector2(0, -17);
        SpawnLevel();
    }
    private void Update()
    {
        CheckBlocks();
    }
    void SpawnNewLevel(int numberLevel1, int numberLevel2, int min, int max)
    {
        level1Pos = new Vector2(3.5f, -1);
        level2Pos = new Vector2(3.5f, -6f);

        level1 = levels[numberLevel1];
        level2 = levels[numberLevel2];

        Instantiate(level1, level1Pos, Quaternion.identity);
        Instantiate(level2, level2Pos, Quaternion.identity);

        SetBlocksCount(min, max);

    }
    void SpawnLevel()
    {
        if (PlayerPrefs.GetInt("Level", 0) == 0) //Ýlk seviye
        {
            SpawnNewLevel(0, 17, 3, 5);
        }
        if (PlayerPrefs.GetInt("Level") == 1)
        {
            SpawnNewLevel(0, 18, 3, 5);
        }
        if (PlayerPrefs.GetInt("Level") == 2)
        {
            SpawnNewLevel(1, 13, 9, 15);
        }
        if (PlayerPrefs.GetInt("Level") == 3)
        {
            SpawnNewLevel(3, 5, 8, 9);
        }
        if (PlayerPrefs.GetInt("Level") == 4)
        {
            SpawnNewLevel(8, 17, 7, 16);
        }
        if (PlayerPrefs.GetInt("Level") == 5)
        {
            SpawnNewLevel(9, 24, 13, 15);
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
}
