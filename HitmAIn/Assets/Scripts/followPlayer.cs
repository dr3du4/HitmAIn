using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class followPlayer : MonoBehaviour
{


    // Obiekt gracza, za którym kamera będzie podążać
    public Transform player;

    // Wektor, który określa przesunięcie kamery względem gracza
    public Vector3 offset;

    private void Start()
    {
        
    }

    void Update()
    {
        transform.position = player.position + offset;
    }
}