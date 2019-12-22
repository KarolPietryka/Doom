using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBilboardChange : MonoBehaviour {


    [SerializeField] Sprite[] sprites;
    [SerializeField] AnimationClip[] anims;
    [SerializeField] bool isAnimated;

    Animator animator;
    SpriteRenderer spriteRenderer;
    float angle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        angle = getAngle();
 
        if ((angle <= 22.5f && angle >= 0) || (angle >= 337.5f && angle <= 360f))
        {
            changeSprite(0);
        }
        else if (angle < 67.5f && angle > 22.5f)
        {
            changeSprite(1);
        }
        else if (angle <= 112.5f && angle >= 67.5f)
        {
            changeSprite(2);
        }
        else if (angle < 157.5  && angle > 112.5f)
        {
            changeSprite(3);
        }
        else if (angle <= 202.5f  && angle >= 157.5)
        {
            changeSprite(4);
        }
        else if (angle < 247.5f  && angle > 202.5f)
        {
            changeSprite(5);
        }
        else if (angle <= 292.5f  && angle >= 247.5f)
        {
            changeSprite(6);
        }
        else if (angle < 247.5f  && angle > 202.5f)
        {
            changeSprite(7);
        }
    }

    void changeSprite(int index)
    {
        if (isAnimated)
        {
            animator.Play(anims[index].name);
        }
        else
        {
            spriteRenderer.sprite = sprites[index];
        }
    }

    float getAngle()
    {
        Vector3 direction = Camera.main.transform.position - this.transform.position;
        float angleTemp = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        angleTemp += 90f;
        return angleTemp;
    }
}
