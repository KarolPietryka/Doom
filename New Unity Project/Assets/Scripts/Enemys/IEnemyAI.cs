using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAI {

    void updateActions();

    void onTriggerEnter(Collision _enemy);

    void ToPatrolState();

    void ToAttackState();

    void ToAlertState();

    void ToChaseState();
}
