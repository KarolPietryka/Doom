using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Pistol : MonoBehaviour {

    public int pistolDamage;
    public Sprite staticPistol;
    public Sprite reloadPistol;
    public Sprite shotPistol;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;
    private AudioSource source;
    private bool isShooting = false;
    private bool activatingReloading = false;
    private bool isReloading = false;

    public Text Ammo;
    public int ammoClipCapacity;
    public int ammoAmount;
    public int ammoLeftInClip;

    public int pistolRange;

    private Ray bulletRay;
    private RaycastHit raycastHit;
    public GameObject bulletHole;

    void Awake ()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        Ammo.text = ammoLeftInClip + " / " + ammoAmount;
        if (Input.GetButtonDown("Fire1") && isReloading == false)
        {
            isShooting = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && isShooting == false && isReloading == false)
        {
            activatingReloading = true;
        }
    }

    void FixedUpdate()
    {
        if (isReloading == false && isShooting == true)
        {
            isShooting = false;
            StartCoroutine("shot");
        }
        else if (activatingReloading == true && isShooting == false)
        {
            activatingReloading = false;
            StartCoroutine("reload");
        }	
	}

    IEnumerator shot()
    {
        if (ammoLeftInClip > 0)
        {
            ammoLeftInClip--;

            bulletRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(bulletRay, out raycastHit, pistolRange))
            {
                //hit.collider.gameObject.SendMessage("pistolHit", pistolDamage, SendMessageOptions.DontRequireReceiver);
                Instantiate(bulletHole, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal)).transform.parent = raycastHit.transform.gameObject.transform;// FromToRotation(Vector3.up, raycastHit.normal) z jakiej do jakiej płaszczyzny. obiekt bulletHole lezy płasko na ziemi domyslnie(w preFabs jest)
            }

            GetComponent<SpriteRenderer>().sprite = shotPistol;
            source.PlayOneShot(shotSound);
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().sprite = staticPistol;
        }
        else
        {
            source.PlayOneShot(emptyGunSound);
        }
    }
    IEnumerator reload()
    {
        isReloading = true;

        if (ammoAmount > 0)
        {
            GetComponent<SpriteRenderer>().sprite = reloadPistol;
            source.PlayOneShot(reloadSound);
            yield return new WaitForSeconds(2.0f);
            GetComponent<SpriteRenderer>().sprite = staticPistol;

            ammoAmount -= ammoClipCapacity;
            ammoLeftInClip = ammoClipCapacity;
        }

        isReloading = false;
    }
}
