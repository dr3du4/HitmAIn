using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Komponent Rigidbody2D gracza
    private Rigidbody2D rb;

    // Wektor przechowujący kierunek ruchu
    private Vector2 movement;
    

    void Start()
    {
        // Pobierz Rigidbody2D przypisany do gracza
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Odczytaj wejście z klawiatury (WSAD) - Input.GetAxis zwraca wartość z przedziału [-1, 1]
        movement.x = Input.GetAxisRaw("Horizontal"); // A (-1) i D (+1)
        movement.y = Input.GetAxisRaw("Vertical");   // W (+1) i S (-1)
    }

    void FixedUpdate()
    {
        // Ruch gracza - przesuwamy Rigidbody2D w kierunku zgodnym z wejściem
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    // Funkcja wywoływana przy kolizji z obiektami z tagiem "Enemy" lub "Light"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Light"))
        {
            GameManager.Instance.ShowLoseScreen();
        }
        // Sprawdzamy, czy gracz wszedł w obszar "Reward"
        if (other.CompareTag("Reward"))
        {
            GameManager.Instance.ShowWinScreen();
        }
    }
}

