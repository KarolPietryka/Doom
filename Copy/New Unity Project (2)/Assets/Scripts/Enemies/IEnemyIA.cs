using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyIA {

    void updateState();

    void toPatrolState();

    void toAttackState();

    void toAlertState();

    void toChaseState();
}
