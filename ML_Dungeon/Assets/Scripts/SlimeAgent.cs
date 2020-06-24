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

        this.transform.localPosition= new Vector3(Random.value*6-3,
                                                  Random.value*6-3,
                                                  0f);
        target.transform.localPosition= new Vector3(Random.value*6-3,
                                                    Random.value*6-3,
                                                    0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(target.transform.localPosition);
        sensor.AddObservation(health.currentHealth);
        sensor.AddObservation(enemyhealth.currentHealth);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        slimeMovement.movement.x=vectorAction[0];
        slimeMovement.movement.y=vectorAction[1];
        combat.isAttacking=vectorAction[2]>0f;

        slimeMovement.Move();
        combat.Attack();
        enemyhealth.Heal();

        float distanceToTarget = Vector3.Distance(this.transform.localPosition,
                                                  target.transform.localPosition);

        if(enemyhealth.currentHealth<=0)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        if(health.currentHealth<=0)
        {
            SetReward(-0.01f);
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
