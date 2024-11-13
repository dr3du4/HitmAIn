using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Pozycje dwóch punktów, między którymi porusza się przeciwnik
        public Transform pointA;
        public Transform pointB;
    
        // Prędkość poruszania się przeciwnika
        public float speed = 2.0f;
    
        // Prędkość obracania przeciwnika
        public float rotationSpeed = 90.0f;
    
        // Flaga wskazująca, do którego punktu zmierza przeciwnik
        private bool movingToPointB = true;
    
        void Update()
        {
            // Wyznacz cel podróży
            Vector3 targetPosition = movingToPointB ? pointB.position : pointA.position;
    
            // Porusz przeciwnikiem w kierunku celu
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    
            // Sprawdź, czy przeciwnik dotarł do punktu
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Przeciwnik dotarł do celu, więc przełącz kierunek
                movingToPointB = !movingToPointB;
    
                // Obróć przeciwnika o 180 stopni
                StartCoroutine(RotateEnemy());
            }
        }
    
        // Korutyna do obracania przeciwnika o 180 stopni
        IEnumerator RotateEnemy()
        {
            float rotationAmount = 0f;
            while (rotationAmount < 180f)
            {
                float rotationStep = rotationSpeed * Time.deltaTime * 180f;
                rotationStep = Mathf.Min(rotationStep, 180f - rotationAmount);
                transform.Rotate(0f, 0f, rotationStep);
                rotationAmount += rotationStep;
                yield return null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
}
