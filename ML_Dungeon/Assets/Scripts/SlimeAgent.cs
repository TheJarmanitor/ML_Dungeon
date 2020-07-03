using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class SlimeAgent : Agent
{
    SlimeMovement slimeMovement;
    Combat combat;
    HealthSystem health;
    HealthSystem enemyhealth;

    public GameObject target;

    public float distanceToTarget;
    float healthRecorder;
    float distanceRecorder;

    bool danger=false;



    public override void Initialize()
    {
        slimeMovement=GetComponent<SlimeMovement>();
        combat=GetComponent<Combat>();
        health=GetComponent<HealthSystem>();
        enemyhealth=target.GetComponent<HealthSystem>();
    }

    public override void OnEpisodeBegin()
    {
        health.ResetHealth();
        enemyhealth.ResetHealth();
        healthRecorder=enemyhealth.maxHealth;
        distanceRecorder=Mathf.Infinity;
        combat.isAttacking=false;

        this.transform.localPosition= new Vector3(Random.value*6-3f,
                                                  Random.value*6-3f,
                                                  0f);
        target.transform.localPosition= new Vector3(Random.value*6-3f,
                                                    Random.value*6-3f,
                                                    0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(target.transform.localPosition);
        sensor.AddObservation(danger ? 1f: 0f);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        slimeMovement.movement.x=vectorAction[0];
        slimeMovement.movement.y=vectorAction[1];
        combat.isAttacking=vectorAction[2]>0f;
        if(health.currentHealth<=30)
            danger=true;
        else
            danger=false;

        slimeMovement.Move();
        combat.Attack();
        distanceToTarget = (target.transform.position - combat.attackPoint.transform.position).sqrMagnitude;
        if(!danger)
        {
            if(combat.isAttacking)
            {
                if(enemyhealth.currentHealth<healthRecorder)
                {
                    SetReward(0.1f);
                    healthRecorder=enemyhealth.currentHealth;
                }
            }
            else
            {
                if(distanceToTarget<distanceRecorder)
                {
                    SetReward(0.01f);
                    distanceRecorder=distanceToTarget;
                }
                else
                {
                    SetReward(-0.001f);
                }
            }
        }
        else
        {
            if(combat.isAttacking)
            {
                SetReward(-0.005f);
            }
            else
            {
                if(distanceToTarget>distanceRecorder)
                {
                    SetReward(0.02f);
                    distanceRecorder=distanceToTarget;
                }
                else
                {
                    SetReward(-0.001f);
                }
                // if(distanceToTarget>=15f)
                // {
                //     SetReward(0.25f);
                //     EndEpisode();
                // }
            }

        }
        if(enemyhealth.currentHealth<=0f)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
        actionsOut[2]=Input.GetKey("space")? 1f:0f;
    }
}
