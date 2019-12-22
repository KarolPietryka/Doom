using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketLauncher : MonoBehaviour {

    public GameObject rocket;
    public GameObject explosion;
    public GameObject spawnPoint;

    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;
    public AudioClip explosionSound;

    public float rocketForce;
    public float explosionRadius;
    public float explosionDamage;
    public LayerMask explosionLayerMask;

    public Text ammoText;
    public int rocketsAmount;
    private int rocketsLeft;

    AudioSource audioSource;

    bool isReloading;
    bool isCharged = true;
    bool isShot;
    int rocketInChamber;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rocketsLeft = rocketsAmount;
    }
    void Update()
    {
        rocketInChamber = isCharged ? 1 : 0;
        ammoText.text = rocketInChamber + " / " + rocketsLeft;

        if (Input.GetButtonDown("Fire1") && isCharged && isReloading != true)
        {
            isCharged = false;
            audioSource.PlayOneShot(shotSound);
            GameObject rocketInstantiated = Instantiate(rocket, spawnPoint.transform.position, Quaternion.identity);
            rocketInstantiated.GetComponent<Rocket>().damage = explosionDamage;
            rocketInstantiated.GetComponent<Rocket>().explosionRadius = explosionRadius;
            rocketInstantiated.GetComponent<Rocket>().explosionSound = explosionSound;
            rocketInstantiated.GetComponent<Rocket>().layerMask = explosionLayerMask;
            rocketInstantiated.GetComponent<Rocket>().explosion = explosion;

            Rigidbody rocketRigidbody = rocketInstantiated.GetComponent<Rigidbody>();
            rocketRigidbody.AddForce(Camera.main.transform.forward * rocketForce, ForceMode.Impulse);//ForceMode.Impuls means bigger mas needs bigger force
            reload();
        }
        else if (Input.GetButtonDown("Fire1") && !isCharged && isReloading != true)
        {
            reload();
        }
    }
    void reload()
    {
        if (rocketsLeft <= 0)
        {
            audioSource.PlayOneShot(emptyGunSound);
        }
        else
        {
            StartCoroutine("reloadWeapon");
            rocketsLeft--;
            isCharged = true;
        }
    }
    IEnumerator reloadWeapon()
    {
        isReloading = true;
        audioSource.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(2.5f);
        isReloading = false;
    }


}
