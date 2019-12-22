using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [HideInInspector] public float explosionRadius;
    [HideInInspector] public float damage;
    [HideInInspector] public LayerMask layerMask;
    [HideInInspector] public GameObject explosion;
    [HideInInspector] public AudioClip explosionSound;

    float rocketLife;
    float destroyAfterTime = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        rocketLife += Time.deltaTime;
        if (rocketLife > destroyAfterTime)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];//Tab of all object which collided witch object. We need only first object becouse explosion occure on first tougch
        Collider[] hitColliders = Physics.OverlapSphere(contact.point, explosionRadius, layerMask);//returns all coliders which are in sphera defined in args of funnction
        GameObject explosionIstantiate = Instantiate(explosion, contact.point, Quaternion.identity);
        explosionIstantiate.GetComponent<Explosion>().explosionSound = explosionSound;

        foreach (Collider col in hitColliders)
        {
            col.SendMessage("addDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
        Destroy(gameObject);
    }
}
