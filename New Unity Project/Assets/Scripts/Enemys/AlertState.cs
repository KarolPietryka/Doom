using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyAI
{
    EnemyStates enemy;
    float timer = 0;
    

    public AlertState(EnemyStates _enemyStates)
    {
        enemy = _enemyStates;
    }
    public void updateActions()
    {
        Debug.Log("IN ALERT STATE");
        search();
        watch();
        
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            enemy.navMeshAgent.isStopped = true;
            lookAround();
        }
    }
    void search()
    {
        enemy.navMeshAgent.destination = enemy.lastKnownPosition;
        enemy.navMeshAgent.isStopped = false;
    }
    void watch()
    {
        if (enemy.enemySppotted())
        {
            enemy.navMeshAgent.destination = enemy.lastKnownPosition;
            ToChaseState();
        }  
    }
    void lookAround()
    {
        timer += Time.deltaTime;//time sinc last "ramka" of game
        if (timer >= enemy.stayAlertTime)
        {
            timer = 0;
            enemy.navMeshAgent.isStopped = false;
            ToPatrolState();
        }
    }
    public void onTriggerEnter(Collision enemy) { }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void ToAttackState() { }

    public void ToAlertState() { }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }
}
