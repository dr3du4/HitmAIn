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
    
    void Start()
    {
        startPos = transform.position;
        currentDistance = Vector3.Distance(startPos, target.position);
    }
    
    public override void OnEpisodeBegin()
    {
        transform.position = startPos;
        currentDistance = Vector3.Distance(transform.position, target.position);
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
        Debug.Log(other.tag);
        if (other.tag == "Goal")
        {
            AddReward(1f);
            EndEpisode();
        }
        else if (other.tag == "Wall")
        {
            AddReward(-1f);
            EndEpisode();
        }
        
    }
}
