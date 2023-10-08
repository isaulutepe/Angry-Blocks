using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    private GameController gc;
    public float power = 2f; //F�rlatma g�n�
    private int dots = 15; //Top say�s�

    private Vector2 startPosition;

    private bool shoot, aiming;

    private GameObject Dots;
    private List<GameObject> projectilesPack;

    private Rigidbody2D ballBody;

    public GameObject ballPrefab;
    public GameObject ballContainer;

    private void Awake()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        Dots = GameObject.Find("Dots"); //An game obje i�inde 15 adet dot nesnesi var.
    }
    private void Start()
    {
        projectilesPack = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        //Bu i�lemde, Dots adl� bir GameObject�in t�m alt nesnelerinin (children) bir listesini olu�turuyorsunuz. Bu listeyi projectilesPack adl� bir de�i�kene at�yorsunuz. Dost nesnesinin transform �zelli�ini transform nesnesine d�n��t�y�oruz sonra da bunu bir transorm listesi yap�yoruz ard�ndan bu listedeki b�t�n elemanlar� tekrar bir gameonjeye �eviriyoruz.

        //Bu i�lem alt nesnelerin transformlar�n� de�il, do�rudan GameObject�lerini kullanabilmemizi sa�lar.

        HideDost(); //Toplar� gizle.
    }
    private void Update()
    {
        ballBody = ballPrefab.GetComponent<Rigidbody2D>();

        if (gc.shotCount <= 3)
        {
            Aim();
            Rotate();
        }
    }

    private void Aim()
    {
        if (shoot)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            if (!aiming)
            {
                aiming = true;
                startPosition = Input.mousePosition; //Mouse poziyonunu ba�lang�� pozisyonu kabul et.
                gc.CheckShotCount();
            }
            else
            {
                PathCalculation();
            }
        }
        else if (aiming && !shoot)
        {
            aiming = false;
            HideDost();
            StartCoroutine(Shoot());
            if (gc.shotCount == 1)
                Camera.main.GetComponent<CameraTransition>().RotateCameraToSide(); //Kameray� yana �evir.
        }
    }
    Vector2 ShootForce(Vector2 force)
    {
        return (new Vector2(startPosition.x, startPosition.y) - new Vector2(force.x, force.y)) * power;
    }
    Vector2 DotPath(Vector2 startP, Vector2 startVel, float t)
    //Ba�lang�� posizyonu, ba�lang�� h�z�,birde zaman parametreleri al�r.
    {
        return startP + startVel * t + 0.5f * Physics2D.gravity * t * t;
        //Bu kodlar, bir cismin 2 boyutlu bir y�r�nge �zerindeki konumunu hesaplamak i�in kullan�l�yor
    }
    void PathCalculation()
    {
        Vector2 vel = ShootForce(Input.mousePosition) * Time.fixedDeltaTime / ballBody.mass;
        //Topun hareketini ayarl�yoruz. fixidDeltaTime topun sabit h�zla gitmesini sa�layacak. ballBody.mass de�eride topun ivmesini kontrol etmek i�in.

        for (int i = 0; i < projectilesPack.Count; i++)
        {
            projectilesPack[i].GetComponent<Renderer>().enabled = true; //Toplar�n g�r�n�rl���n� aktif ettik.

            float t = i / 15f;
            Vector3 point = DotPath(transform.position, vel, t); //Her bir nokta i�in konumunu hesaplar.
            point.z = 1; // z de hareket sabit olmal�.
            projectilesPack[i].transform.position = point; //Her noktan�n trasformlar�n� de�i�tir.
        }
    }
    void ShowDots()
    {
        for (int i = 0; i < projectilesPack.Count; i++)
        {
            projectilesPack[i].GetComponent<Renderer>().enabled = true;
        }
    }
    void HideDost()
    {
        for (int i = 0; i < projectilesPack.Count; i++)
        {
            projectilesPack[i].GetComponent<Renderer>().enabled = false;
        }
    }
    void Rotate() //Connon nesnesinin d�nmesi i�in.
    {
        var direction = GameObject.Find("dot (1)").transform.position - transform.position;
        //Cannon nesnesi ile birinci top aras�ndaki mesafeyi �l�t�k. 
        var angle = Math.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Burada a�� hesaplama i�lemi yap�l�r.
        //direction.y ve direction.x de�erleri bir noktadan ba�ka bir noktaya giden bir do�ruyu temsil ediyo.
        //Math.Atan2 --> bu �n vect�r�n�n x eksni ile yapt��� a��y� Ranyan olarak hesaplar.
        //Mathf.Rad2Deg--> bu da randyan olan de�eri a�� cinsine d�n��t�r�r.

        transform.rotation = Quaternion.AngleAxis((float)angle, Vector3.forward);
    }
    IEnumerator Shoot()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.07f);
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball.name = "Ball";
            ball.transform.SetParent(ballContainer.transform); //Olu�an toplar� bu nesne alt�nda toplar.
            ballBody = ball.GetComponent<Rigidbody2D>();
            ballBody.AddForce(ShootForce(Input.mousePosition));
        }
        gc.shotCount++;
    }
}
