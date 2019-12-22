using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool slide, rotate;
    public float speed;
    public KeyCode openningKey;
    public Vector3 endPosition;
    Vector3 startPosition;
    GameObject doors;
    bool isOpen = false;
    Animator animator;

    private void Awake()
    {
        doors = transform.Find("Doors").gameObject;
        startPosition = doors.transform.position;
        animator = doors.GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(openningKey))
            {
                if (slide)
                {
                    StartCoroutine(slideDoors());
                }
                else if (rotate)
                {
                    if (!isOpen)
                    {
                        isOpen = !isOpen;
                        animator.SetBool("isOpened", true);

                    }
                    else
                    {
                        isOpen = !isOpen;
                        animator.SetBool("isOpened", false);
                    }
                }
            }
        }
    }

    IEnumerator slideDoors()
    {
        Vector3 current = doors.transform.position;
        Vector3 destination = isOpen ? startPosition : endPosition;
        isOpen = !isOpen;
        float time = 0f;
        while (time < 1)
        {
            time += Time.deltaTime * speed;
            doors.transform.position = Vector3.Lerp(current, destination, time);
            yield return null;
        }       
    }
}
