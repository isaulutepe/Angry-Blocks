using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{

    private Transform cameraContainer;

    private float rotateSemiAmount = 4; //DDairel harektte yar��ap i�in
    private float shakeAmout; //Kameran�n titreme hareketi i�in.

    private Vector3 startingLocalPos;

    private void Start()
    {
        cameraContainer = GameObject.Find("CameraContainer").transform;
    }
    private void Update()
    {
        //Titreme methotlar� shakeAmount de�erini artt�racak ve bu kodlar �al��acak ard�ndan tekrar shakeAmount de�eri d���r�ld���nden titreme duracak.
        if (shakeAmout > 0.01f) //Titreme hareketi varsa konumda olu�acak de�i�iklik.
        {
            Vector3 localPosition = startingLocalPos;
            localPosition.x += shakeAmout * Random.Range(3, 5);
            localPosition.y += shakeAmout * Random.Range(3, 5);
            localPosition.z = -10f;
            transform.position = localPosition;
            shakeAmout = 0.9f * shakeAmout;//Titreme miktar�n� her karede biraz azalt�r.Titreme efektinin zamanla azalmas�n� sa�lamak i�in.
        }
    }
    public void Shake() //titreme miktar�n� art�r�r, ancak belirli bir �st s�n�r olan 0.1f'yi ge�mez.
    {
        shakeAmout = Mathf.Min(0.1f, shakeAmout + 0.01f);
    }
    public void MediumShake() //titreme miktar�n� art�r�r, ancak bu sefer �st s�n�r 0.15f olarak belirlenmi�tir. 
    {
        shakeAmout = Mathf.Min(0.15f, shakeAmout + 0.015f);
    }
}
