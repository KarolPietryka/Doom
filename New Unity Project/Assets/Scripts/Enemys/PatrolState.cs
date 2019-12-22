using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyAI
{
    EnemyStates enemy;
    int nextWapPoint = 0;

    bool testFlag;

    public PatrolState(EnemyStates _enemy)
    {
        enemy = _enemy;
    }
    public void updateActions()
    {
        watch();
        patrol();
    }
    void watch()
    {
        if (enemy.enemySppotted())
        {
            ToChaseState();
        }
    }
    void patrol()
    {
        enemy.navMeshAgent.SetDestination(enemy.wayPoints[nextWapPoint].position);//navMashAgent is used for movement destinaton is the point wher the enemy is ahead
        enemy.navMeshAgent.isStopped = false;
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)// check if enemy reach point and if unity computing path
        {
            nextWapPoint = (nextWapPoint + 1) % enemy.wayPoints.Length;
        }
    }
    public void onTriggerEnter(Collision enemy)
    {
        if (enemy.gameObject.CompareTag("Player"))//if obiect which step into collision witc object enemy(Collision type) is Player?
        {
            ToAlertState();
        }
    }

    public void ToPatrolState()
    {
    }

    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }
}
