using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class VisionCone : MonoBehaviour {
    [SerializeField] private Transform parent;

    [SerializeField] [Range(0f, 360f)] private float angleInDegrees;

    [SerializeField] [Range(0f, 360f)] private float angleOffset;

    [SerializeField] private float radius;

    [SerializeField] [Range(0f, 3f)] private float raycastsPerDegree;

    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask detectionMask;

    private List<Vector2> raycastHitpoints = new();
    private LayerMask interactionMask;
    

    private void Update()
    {
        interactionMask = obstacleMask | detectionMask;
        raycastHitpoints.Clear();
        int totalRaycastCount = Mathf.FloorToInt(raycastsPerDegree * angleInDegrees);
        float angleStep = angleInDegrees / totalRaycastCount;
        Vector3 origin = transform.position;
        float startAngle = -angleInDegrees / 2f + angleOffset;

        for (int i = 0; i <= totalRaycastCount; i++)
        {
            float currentAngle = startAngle + angleStep * i;
            Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * transform.up;
            Vector3 endPoint = origin + direction * radius;

            RaycastHit2D raycastHit = Physics2D.Linecast(origin, endPoint, interactionMask);
            if (raycastHit.collider != null)
            {
                raycastHitpoints.Add(raycastHit.point);
                if (IsInLayerMask(raycastHit.collider.gameObject, detectionMask))
                {
                    Debug.Log("Player detected at " + raycastHit.point);
                }
            }
            else
            {
                raycastHitpoints.Add(endPoint);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        Vector3 centerDirection = Quaternion.Euler(0, 0, angleOffset) * transform.up;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + centerDirection * radius);

        Gizmos.color = Color.yellow;
        if (!Application.isPlaying)
        {
            //Gizmos.DrawWireSphere(origin, radius);
            Gizmos.DrawLine(origin, origin + Quaternion.Euler(0, 0, -angleInDegrees / 2f) * centerDirection * radius);
            Gizmos.DrawLine(origin, origin + Quaternion.Euler(0, 0, angleInDegrees / 2f) * centerDirection * radius);
        }
        
        foreach (Vector2 endPoint in raycastHitpoints)
        {
            Gizmos.DrawLine(origin, endPoint);
        }
    }
    
    private bool IsInLayerMask(GameObject obj, LayerMask mask) {
        return (mask.value & (1 << obj.layer)) > 0;
    }
}