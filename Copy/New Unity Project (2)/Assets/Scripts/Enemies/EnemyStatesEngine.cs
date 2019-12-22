using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyStatesEngine : MonoBehaviour {

    [HideInInspector] public IEnemyIA enemyIA;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public AttackState attackState;

    public int patrolRange;
    public Vector3 LastKnownPlayerPosition{ get; set;}

    public Transform[] points;
    [HideInInspector] public int destinationPoint = 0;
    [HideInInspector] public NavMeshAgent navMashagent;

    void Start ()
    {
        enemyIA = new PatrolState(this);

        navMashagent = GetComponent<NavMeshAgent>();
        navMashagent.autoBraking = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        enemyIA.updateState();
    }

    public void updatelastKnownPlayerPosition(Transform _playertransform)
    {
         LastKnownPlayerPosition = _playertransform.position;
    }
}
