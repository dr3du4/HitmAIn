using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VisionCone : MonoBehaviour {
    [SerializeField] private Transform parent;

    [SerializeField] [Range(0f, 360f)] private float angleInDegrees;

    [SerializeField] [Range(0f, 360f)] private float angleOffset;

    [SerializeField] private float radius;

    [SerializeField] [Range(0f, 3f)] private float raycastsPerDegree;

    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask detectionMask;

    [SerializeField] private Material material;
    

    private List<Vector2> raycastHitpoints = new();
    private LayerMask interactionMask;

    private MeshFilter meshFilter;

    private Vector3[] vertices;
    private int[] triangles;


    private void Start()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        
        int totalRaycastCount = Mathf.FloorToInt(raycastsPerDegree * angleInDegrees);
        vertices = new Vector3[totalRaycastCount + 1];
        triangles = new int[3 * totalRaycastCount];

        GetComponent<MeshRenderer>().material = material;
    }

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

        #region meshGeneration

        Mesh mesh = new Mesh();

        if (vertices.Length != totalRaycastCount + 1)
        {
            vertices = new Vector3[totalRaycastCount + 1];
            triangles = new int[totalRaycastCount * 3];
        }

        vertices[0] = Vector3.zero;

        // I could do this in the loop above, but I'm too lazy for now
        for (int i = 1; i <= totalRaycastCount; i++)
        {
            vertices[i] = transform.InverseTransformPoint(raycastHitpoints[i - 1]);
        }

        for (int i = 1; i < totalRaycastCount; i++)
        {
            triangles[3 * i - 3] = 0;
            triangles[3 * i - 2] = i + 1;
            triangles[3 * i - 1] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        meshFilter.mesh = mesh;

        #endregion
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
        
        for (int i = 1; i < raycastHitpoints.Count; i++)
        {
            Gizmos.DrawLine(origin, raycastHitpoints[i]);
        }
        
        Gizmos.color = Color.green;
        if (raycastHitpoints.Count != 0)
        {
            Gizmos.DrawLine(origin, raycastHitpoints[0]);
        }
    }
    
    private bool IsInLayerMask(GameObject obj, LayerMask mask) {
        return (mask.value & (1 << obj.layer)) > 0;
    }
}