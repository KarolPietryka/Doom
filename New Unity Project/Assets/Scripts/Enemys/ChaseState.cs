using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyAI {

    EnemyStates enemy;

    public ChaseState(EnemyStates _enemyStates)
    {
        enemy = _enemyStates;
    }

    public void updateActions()
    {
        Debug.Log("In CHASE STATE");
        watch();
        chase();
    }

    void watch()
    {
        if (!enemy.enemySppotted())
        {
            ToAlertState();
        }
    }
    void chase()
    {
        enemy.navMeshAgent.SetDestination(enemy.chaseTarget.position);
        enemy.navMeshAgent.isStopped = false;
        if (enemy.navMeshAgent.remainingDistance <= enemy.meleeAttackRange && enemy.onlyMelee == true)
        {
            enemy.navMeshAgent.isStopped = true;
            ToAttackState();
        }
        else if (enemy.navMeshAgent.remainingDistance <= enemy.shootAttackRange && enemy.onlyMelee == false)
        {
            enemy.navMeshAgent.isStopped = true;
            ToAttackState();
        }
    }
    public void onTriggerEnter(Collision enemy) { }

    public void ToPatrolState() { }

    public void ToAttackState()
    {
        Debug.Log("Atakuje gracza");
        enemy.navMeshAgent.isStopped = true;
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        Debug.Log("Zgubiłem gracza");
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState() { }
}
