using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class SlimeAgent : Agent
{
    Rigidbody rBody;
    Health health;

    void Start()
    {
        rBody=GetComponent<Rigidbody>();
        health=GetComponent<Health>();
    }

    public Transform Target;
    public Animator animator;

    public override void OnEpisodeBegin()
    {

    }

}
