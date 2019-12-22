using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public Sprite deadBody;
    public int maxHealth;
    float health;

    private EnemyStates enemyStates;
    private NavMeshAgent navMeshAgent;
    private SpriteRenderer spriteRenderer;
    private BoxCollider boxCollider;

    private void Start()
    {
        health = maxHealth;
        enemyStates = GetComponent<EnemyStates>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }
    public void addDamage(int _damage)
    {
        health -= _damage;
    }
    private void Update()
    {
        if (health <= 0)
        {
            enemyStates.enabled = false;
            navMeshAgent.enabled = false;
            GetComponent<Animator>().enabled = false;
            GetComponent<DynamicBillboardChange>().enabled = false;
            transform.Find("Vision").GetComponent<Vision>().enabled = false;
            spriteRenderer.sprite = deadBody;
            transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
            boxCollider.center = new Vector3(0, 0.12f, 0);
            boxCollider.size = new Vector3(2.12f, 0.47f, 0.05f);
        }
    }
}
