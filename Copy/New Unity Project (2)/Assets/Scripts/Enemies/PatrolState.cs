using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyIA
{
    EnemyStatesEngine enemyStatesEngine;

    private Transform[] points;
    private int destinationPoint;
    private int patrolRange;

    public PatrolState(EnemyStatesEngine _enemyStatesEngine)
    {
        enemyStatesEngine = _enemyStatesEngine;

        points = _enemyStatesEngine.points;
        destinationPoint = _enemyStatesEngine.destinationPoint;
        patrolRange = _enemyStatesEngine.patrolRange;
    }
    public void toPatrolState()
    {
    }

    public void toAttackState()
    {
    }

    public void toAlertState()
    {
    }

    public void toChaseState()
    {
        enemyStatesEngine.enemyIA = enemyStatesEngine.chaseState;
    }

    public void updateState()
    {
        patrol();
        watch();
    }
    public void patrol()
    {
        enemyStatesEngine.navMashagent.destination = enemyStatesEngine.points[destinationPoint].position;
        if (enemyStatesEngine.navMashagent.remainingDistance <= enemyStatesEngine.navMashagent.stoppingDistance && !enemyStatesEngine.navMashagent.pathPending)// check if enemy reach point and if unity computing path
        {
            destinationPoint = (destinationPoint + 1) % points.Length;
        }
    }
    public void watch()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(enemyStatesEngine.transform.position, -enemyStatesEngine.transform.forward, out raycastHit, patrolRange)) 
        {
            if (raycastHit.transform.CompareTag("Player"))
            {
                enemyStatesEngine.updatelastKnownPlayerPosition(raycastHit.transform);
                toChaseState();
            }
        }

    }
}

