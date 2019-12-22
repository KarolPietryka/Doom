using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Vision : MonoBehaviour {

    Vector3 destination;
    float sightExtension = 1.001f;

    void Update ()
    {
       
        EnemyStates enemyStates = transform.parent.GetComponent<EnemyStates>();
        if (enemyStates.lookAtPlayer == true)
        {
            destination = transform.parent.GetComponent<EnemyStates>().lastKnownPosition;
        }
        else
        {
            destination = enemyStates.navMeshAgent.destination;
            destination *= sightExtension;
        }
        transform.LookAt(destination);
        
	}
}
