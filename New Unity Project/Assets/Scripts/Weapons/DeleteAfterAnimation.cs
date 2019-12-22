using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterAnimation : MonoBehaviour {

    public float deley = 0f;

    private void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + deley);
    }
}
