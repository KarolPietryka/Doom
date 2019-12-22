using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyAI
{
    EnemyStates enemy;
    float timer;

    public AttackState(EnemyStates _enemy)
    {
        enemy = _enemy;
    }
    public void updateActions()
    {
        enemy.navMeshAgent.isStopped = true;
        Debug.Log("In ATTACK STATE");
        timer += Time.deltaTime;
        float distance = Vector3.Distance(enemy.chaseTarget.position, enemy.transform.position);
        if (distance > enemy.meleeAttackRange && enemy.onlyMelee == true ||
            distance > enemy.shootAttackRange && enemy.onlyMelee == false)
        {
            enemy.navMeshAgent.isStopped = false;
            ToChaseState();
        }
        watch();
        if (distance < enemy.shootAttackRange && distance > enemy.meleeAttackRange && enemy.onlyMelee == false && timer >= enemy.attackDelay)
        {
            distanceAttack();
            timer = 0;
        }
        if (distance <= enemy.meleeAttackRange && timer >= enemy.attackDelay)
        {
            meleeAttack();
            timer = 0;
        }
    }

    public void onTriggerEnter(Collision enemy) { }

    public void ToPatrolState() { }

    public void ToAttackState() { }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    void watch()
    {
        if (!enemy.enemySppotted())
        {
            ToAlertState();
        }
    }

    void distanceAttack()
    {
        GameObject missile = GameObject.Instantiate(enemy.missile, enemy.transform.position, Quaternion.identity);
        missile.GetComponent<Missile>().speed = enemy.missileSpeed;
        missile.GetComponent<Missile>().damage = enemy.missileDamage;
    }
    void meleeAttack()
    {
        enemy.chaseTarget.SendMessage("addDamage", enemy.meleeDamage, SendMessageOptions.DontRequireReceiver);
    }
}
