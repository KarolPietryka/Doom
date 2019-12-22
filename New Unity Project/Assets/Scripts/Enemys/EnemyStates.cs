using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour {

    public Transform[] wayPoints; //collecting all points that create way
    public int patrolRange;
    public int meleeAttackRange;
    public Transform vision;
    public float stayAlertTime;
    public int shootAttackRange;

    public GameObject missile;
    public float missileDamage;
    public float missileSpeed;
    public bool onlyMelee = false;
    public float meleeDamage;
    public float attackDelay;

    public LayerMask raycastMaska;

    [HideInInspector] public AlertState alertState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public IEnemyAI currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public Vector3 lastKnownPosition;

    public float viewAngle;
    public bool lookAtPlayer = false;

    void Awake()
    {
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start ()
    {
        currentState = patrolState;
	}
	void Update ()
    {
        currentState.updateActions();
	}
    void OnTriggerEnter(Collision _otherObj)
    {
        currentState.onTriggerEnter(_otherObj);
    }
    void hiddenShot(Vector3 playerShotPosition)
    {
        Debug.Log("Ktos strzelil");
        lastKnownPosition = playerShotPosition;
        currentState = alertState;
    }
    public bool enemySppotted()
    {
        Vector3 directionToPlayer = GameObject.FindWithTag("Player").transform.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, vision.forward);//calculate angle beetween vistion(from enemy's eyes) and vector from enemy to player

        if (angle < viewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(vision.transform.position, directionToPlayer, out hit, patrolRange, raycastMaska))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    chaseTarget = hit.transform;
                    lastKnownPosition = hit.transform.position;
                    lookAtPlayer = true;
                    return true;
                }
            }
        }
        lookAtPlayer = false;
        return false;
    }
}
