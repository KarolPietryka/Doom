using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;
    public bool canMeleeAttack;
    public bool canShoot;
    public float meleeDamage;
    public float shootDamage;

    public void pistolDamage(int damage)
    {
        health -= damage;
    }
}
