using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyIA 
{
    EnemyStatesEngine enemyStatesEngine; 
    private int patrolRange;

    public ChaseState(EnemyStatesEngine _enemyStatesEngine)
    {
        enemyStatesEngine = _enemyStatesEngine;
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
    }

    public void updateState()
    {
        watch();
        chase();
    }
    public void watch()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(enemyStatesEngine.transform.position, -enemyStatesEngine.transform.forward, out raycastHit, patrolRange))
        {
            if (raycastHit.transform.CompareTag("Player"))
            {
                enemyStatesEngine.updatelastKnownPlayerPosition(raycastHit.transform);
            }
            else
            {
                toAlertState();
            }
        }
    }
    public void chase()
    {
        if (enemyStatesEngine)
        enemyStatesEngine.navMashagent.destination = enemyStatesEngine.LastKnownPlayerPosition;
        if (enemyStatesEngine.navMashagent.remainingDistance <= enemyStatesEngine.navMashagent.stoppingDistance && !enemyStatesEngine.navMashagent.pathPending)// check if enemy reach point and if unity computing path
        {
            destinationPoint = (destinationPoint + 1) % points.Length;
        }
    }
}
