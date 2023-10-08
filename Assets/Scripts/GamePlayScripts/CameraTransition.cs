using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{

    private Transform cameraContainer;

    private float rotateSemiAmount = 4; //DDairel harektte yarýçap için
    private float shakeAmout; //Kameranýn titreme hareketi için.

    private Vector3 startingLocalPos;

    private void Start()
    {
        cameraContainer = GameObject.Find("CameraContainer").transform;
    }
    private void Update()
    {
        //Titreme methotlarý shakeAmount deðerini arttýracak ve bu kodlar çalýþacak ardýndan tekrar shakeAmount deðeri düþürüldüðünden titreme duracak.
        if (shakeAmout > 0.01f) //Titreme hareketi varsa konumda oluþacak deðiþiklik.
        {
            Vector3 localPosition = startingLocalPos;
            localPosition.x += shakeAmout * Random.Range(3, 5);
            localPosition.y += shakeAmout * Random.Range(3, 5);
            localPosition.z = -10f;
            transform.position = localPosition;
            shakeAmout = 0.9f * shakeAmout;//Titreme miktarýný her karede biraz azaltýr.Titreme efektinin zamanla azalmasýný saðlamak için.
        }
    }
    public void Shake() //titreme miktarýný artýrýr, ancak belirli bir üst sýnýr olan 0.1f'yi geçmez.
    {
        shakeAmout = Mathf.Min(0.1f, shakeAmout + 0.01f);
    }
    public void MediumShake() //titreme miktarýný artýrýr, ancak bu sefer üst sýnýr 0.15f olarak belirlenmiþtir. 
    {
        shakeAmout = Mathf.Min(0.15f, shakeAmout + 0.015f);
    }

    public void RotateCameraToSide()
    {
        StartCoroutine(RotateCameraToSideRoutine());
    }
    public void RotateCameraToFront()
    {
        StartCoroutine(RotateCameraToFrontRoutine());
    }
    IEnumerator RotateCameraToSideRoutine()
    {
        int frames = 20; //Kameranýn belirli bir açýya dönmesi için gerekli zaman.
        float increment = rotateSemiAmount / (float)frames; //Açýsal dönme miktarýnýn eþit aralýklarla gerçekleþmesini saðlak için.
        for (int i = 0; i < frames; i++)//Kamera Döndürme iþlemi gerçekleþtirilir.
        {
            cameraContainer.RotateAround(Vector3.zero, Vector3.up, increment);
            yield return null;
        }
        yield break;
    }
    IEnumerator RotateCameraToFrontRoutine()
    {
        int frames = 60;
        float increment = rotateSemiAmount / (float)frames;
        for (int i = 0; i < frames; i++)
        {
            cameraContainer.RotateAround(Vector3.zero, Vector3.up, -increment);
            yield return null;
        }
        cameraContainer.localEulerAngles = new Vector3(0, 0, 0);
        yield break;
    }

}
