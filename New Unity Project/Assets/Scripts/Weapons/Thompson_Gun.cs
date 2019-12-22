using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Thompson_Gun : MonoBehaviour {

    public Sprite idlePistol;
    public Sprite shotPistol;
    public Sprite reloadPistol;
    public float pistolDamage;
    public float pistolRange;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;

    public Text ammoText;

    public GameObject bulletHole;

    public int ammoAmount;
    public int ammoClipSize;
    int ammoLeft;
    int ammoClipLeft;
    AudioSource source;

    bool isShot;
    bool isReload;
    bool isReloadingNow;

    public GameObject bloodSplat;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        ammoLeft = ammoAmount;
        ammoClipLeft = ammoClipSize;
    }

    void  Update()
    {
        ammoText.text = ammoClipLeft + " / " + ammoLeft;
        if (Input.GetButtonDown("Fire1") && isReloadingNow != true)
        {
            isShot = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && isReloadingNow != true)
        {
            isReload = true;
        }
    }
    void FixedUpdate()//execute in regular time line and we have the same time between
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;//object which was hit by ray (no initialized)
        if (isShot == true && ammoClipLeft > 0 && isReloadingNow != true)
        {
            isShot = false;
            ammoClipLeft--;
            source.PlayOneShot(shotSound);
            StartCoroutine("shot");// it runs Coroutine so it(the coroutine) will be executed in the same time as main frame
            if (Physics.Raycast(ray, out hit, pistolRange))//statement out means that arg is pass by reference. Different between ref and out is that out doesn't need initialized variables
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Instantiate(bloodSplat, hit.point, Quaternion.identity);
                    if (hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().patrolState ||
                        hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().alertState)
                    {
                        hit.collider.gameObject.SendMessage("hiddenShot", transform.parent.transform.position, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("Hidden shot!!!!" + transform.parent.transform.position);
                    }
                    hit.collider.gameObject.SendMessage("addDamage", pistolDamage, SendMessageOptions.DontRequireReceiver);//Send message to hit object to execute method in " ", third param means that we donot require execute methot if method do not exist(will be no error)
                }
                else
                {
                    Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.parent = hit.transform.gameObject.transform;//create new object in game
                }
            }
        }
        else if (isShot == true && ammoClipLeft <= 0 && isReloadingNow != true)
        {
            isShot = false;
            source.PlayOneShot(emptyGunSound);
        }
        if (isReload == true && isReloadingNow != true)
        {
            reload();
        }
    }

    void reload()
    {
        isReload = false;
        if (ammoLeft >= ammoClipSize)
        {
            StartCoroutine("reloadWeapon");
            ammoLeft -= ammoClipSize;
            ammoClipLeft = ammoClipSize;
        }
        else if (ammoLeft < ammoClipSize && ammoLeft > 0)
        {
            source.PlayOneShot(emptyGunSound);
        }
    }

    IEnumerator reloadWeapon()
    {
        GetComponent<SpriteRenderer>().sprite = reloadPistol;
        isReloadingNow = true;
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(2);
        isReloadingNow = false;
        GetComponent<SpriteRenderer>().sprite = idlePistol;
    }
    IEnumerator shot()//corutein korutyna
    {
        GetComponent<SpriteRenderer>().sprite = shotPistol;
        yield return new WaitForSeconds(0.1f);// -http://plukasiewicz.net/Artykuly/Yield_return It's just waiting
        GetComponent<SpriteRenderer>().sprite = idlePistol;
    }
    public void addAmmo(int value)
    {
        ammoLeft += value;
    }
}
