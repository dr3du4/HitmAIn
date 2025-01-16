using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;

public class playerAI : Agent
{
    [SerializeField] private Transform target;
    private Vector3 startPos;
    private float currentDistance;
    
    public List<GameObject> gates;
    private int currentIndex;
    private int steps=0;
    
    void Start()
    {
        startPos = transform.position;
        currentIndex = 0;
        target = gates[currentIndex].transform;
        currentDistance = Vector3.Distance(startPos, gates[currentIndex].transform.position);
        
        
    }
    
    public override void OnEpisodeBegin()
    {
        transform.position = startPos;
        currentIndex = 0;
       
        target = gates[currentIndex].transform;
        currentDistance = Vector3.Distance(startPos, gates[currentIndex].transform.position);
        foreach (GameObject g in gates)
        {
            g.SetActive(true);
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(target.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    { 
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        float moveSpeed = 0.5f;
        transform.position += new Vector3(moveX, moveY, 0) * Time.deltaTime * moveSpeed;
        float Distance = Vector2.Distance(transform.position, target.position);
        float diff=currentDistance-Distance;
        AddReward(diff);
       
        currentDistance = Distance;
      
        
       
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> actions = actionsOut.ContinuousActions;
        actions[0]=Input.GetAxis("Horizontal");
        actions[1]=Input.GetAxis("Vertical");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Goal")
        {
            Debug.Log(other.tag);
            AddReward(1000f);
            EndEpisode();
        }
        else if (other.tag == "Wall")
        {
            Debug.Log(other.tag);
            AddReward(-1000f);
            EndEpisode();
        }
        else if (other.tag == "Gate" && gates[currentIndex] == other.gameObject)
        {
            Debug.Log($"Triggered Gate: {other.name}, Current Index: {currentIndex}");
            AddReward(500f);
            other.gameObject.SetActive(false);

            currentIndex++;
            if (currentIndex < gates.Count)
            {
                target = gates[currentIndex].transform;
            }
            else
            {
                Debug.LogWarning("No more gates available!");
            }
        }
        
    }
}
