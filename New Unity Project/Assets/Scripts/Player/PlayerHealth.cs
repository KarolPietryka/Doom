using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int maxHealth;
    public AudioClip playerPainSound;
    public AudioClip playerDangerSound;
    AudioSource source;
    [SerializeField] private float health;
    public flashScreen flesh;

    public int maxArmor;
    [SerializeField] private float armor;

    private bool isGameOver = false;
    private bool isInDanger = false;

	void Start ()
    {
        health = maxHealth;
        armor = 0;
        source = GetComponent<AudioSource>();
	}

    private void Update()
    {
        armor = Mathf.Clamp(armor, 0, maxArmor);
        health = Mathf.Clamp(health, -Mathf.Infinity, maxHealth);

        if (health <= 0 && isGameOver == false)
        {
            isGameOver = true;
            GameManager.Instance.playerDeath();
        }
        else if (health > 0 && health <= 10 && isInDanger == false)
        {
            isInDanger = true;
            source.PlayOneShot(playerDangerSound);
        }
        else if (health > 10 && isInDanger == true)
        {
            isInDanger = false;
        }
    }
    public void addDamage(float _damage)
    {
        if (armor > 0 && armor >= _damage)
        {
            armor -= _damage;
        }
        else if (armor > 0 && armor < _damage)
        {
            Debug.Log("damage : " + _damage);
            Debug.Log("armor : " + armor);
            Debug.Log("hp : " + health);
            _damage -= armor;
            armor = 0;
            health -= _damage;
        }
        else
        {
            health -= _damage;
        }

        source.PlayOneShot(playerPainSound);
        flesh.tookDamage();
    }
    public void addHealth(float value)
    {
        health += value;
    }
    public void addArmor(float value)
    {
        armor += value;
    }
}
