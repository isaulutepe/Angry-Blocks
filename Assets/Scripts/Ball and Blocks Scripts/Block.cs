using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    private int count;

    public Text countText;

    private AudioSource bounceSource;


    private void Awake()
    {
        bounceSource = GameObject.Find("BounceSound").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (transform.position.y <= -10) //Bloklar a�a�� d��erse silinecek.
        {
            Destroy(gameObject);
        }
    }
    public void SetStartingCount(int count)
    {
        this.count = count;
        countText.text = count.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Ball" && count > 0) //Top temas ediyorsa.
        {
            count--;
            Camera.main.GetComponent<CameraTransition>().Shake(); //Top ar�ld�g�nda small titreme ba�lar.
            countText.text = count.ToString();
            bounceSource.Play();
            if (count == 0)
            {
                Destroy(gameObject); //Temas tamemen bitince yani �arpma s�f�ra inince blok nesnesi silinecek.
                Camera.main.GetComponent<CameraTransition>().MediumShake(); //Bloklar sili�nirken kamerada sars�lam hareketi ger�ekle�ecek.
                GameObject.Find("ExstraBallProgress").GetComponent<Progress>().IncreaseCurrentWitdh();
            }

        }
    }
}
