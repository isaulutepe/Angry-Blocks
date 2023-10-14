using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public RectTransform extraBallInner;

    private GameController gameController;

    private float curretWidth, addWidth, totalWidth;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    private void Start()
    {
        extraBallInner.sizeDelta = new Vector2(50, 100); //Ba�lang�� de�erleri. x,y olarak
        curretWidth = 50;
        totalWidth = 1375;
    }
    private void Update()
    {
        if (curretWidth >= totalWidth) //Bar tamamen doldu�unda
        {
            gameController.ballsCount++; //Top say�s�n� bir artt�r.
            gameController.ballsCountText.text = gameController.ballsCount.ToString(); //Text olarak yaz.
            curretWidth = 50; //Tekrar bo� bar haline d�n.
        }
        if (curretWidth >= addWidth)
        {
            addWidth += 5;
            extraBallInner.sizeDelta = new Vector2(addWidth, 100);
        }
        else
        {
            addWidth = curretWidth;
        }
    }
    public void IncreaseCurrentWitdh()
    {
        int addRandom = Random.Range(80, 120);
        curretWidth = addRandom + 50 + curretWidth % 576f;
    }
}
