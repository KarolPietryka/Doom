using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DynamicBillboardChange : MonoBehaviour {

    [SerializeField] Sprite[] sprites;
    [SerializeField] string[] animStates = new string[10] { "Backward", "BackAndLeft", "Left", "FrontAndLeft", "Forward", "IdleFront", "IdleBack", "IdleBack_Left", "IdleFront_Left", "IdleLeft"};
    [SerializeField] bool isAnimated;

    Animator animator;
    SpriteRenderer spriteRenderer;

    bool duringFlippedAnimation;

    NavMeshAgent navMeshAgent;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
       getAngle();
    }

    void getAngle()
    {
        Vector3 playerDir = transform.position - Camera.main.transform.position;//Camera.main.transform.forward;
        playerDir.y = 0;
        Vector3 playerRight = Quaternion.Euler(0, 90, 0) * playerDir;//Camera.main.transform.right;
        playerRight.y = 0;
        Vector3 enemyDir = transform.Find("Vision").forward;
        enemyDir.y = 0;

        float dotProduct = Vector3.Dot(playerDir, enemyDir);

        float angleBetweenPlayerAndEnemy = Vector3.Angle(playerDir, enemyDir);
        float angleBetweenPlayerLeftSideAndEnemy = Vector3.Angle(playerRight, enemyDir);

        if (angleBetweenPlayerAndEnemy <= 22.5f && angleBetweenPlayerAndEnemy >= 0)//back
        {
            changeOnIdleOrInMoveSprite(6, 0);
            undoSpriteFlip();
        }
        else if (angleBetweenPlayerAndEnemy < 67.5f && angleBetweenPlayerAndEnemy > 22.5f)//back with gentel flip to left
        {
            changeSprite(angleBetweenPlayerLeftSideAndEnemy, 7, 1);
        }
        else if (angleBetweenPlayerAndEnemy <= 112.5f && angleBetweenPlayerAndEnemy >= 67.5f)//left
        {
            changeSprite(angleBetweenPlayerLeftSideAndEnemy, 9, 2);
        }
        else if (angleBetweenPlayerAndEnemy < 157.5f && angleBetweenPlayerAndEnemy > 112.5f)//front with gentel flip to left
        {
            changeSprite(angleBetweenPlayerLeftSideAndEnemy, 8, 3);
        }
        else if (angleBetweenPlayerAndEnemy <= 180 && angleBetweenPlayerAndEnemy >= 157.5f)//front
        {
            changeOnIdleOrInMoveSprite(5, 4);
            undoSpriteFlip();
        }  
    }

    void changeAnimation(int index)
    {
        if (isAnimated)
        {
            animator.Play(animStates[index]);
        }
        else
        {
            spriteRenderer.sprite = sprites[index];
        }
    }
    void flipSprite()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void undoSpriteFlip()
    {
        if (duringFlippedAnimation == true)
        {
            flipSprite();
        }
        duringFlippedAnimation = false;
    }
    void changeOnIdleOrInMoveSprite(int idleSpriteIndex, int inMoveSpriteIndex)
    {
        if (navMeshAgent.isStopped == true)
        {
            changeAnimation(idleSpriteIndex);
        }
        else
        {
            changeAnimation(inMoveSpriteIndex);
        }
    }
    void changeSprite(float _angleBetweenPlayerLeftSideAndEnemy, int idleSpriteIndex, int inMoveSpriteIndex)
    {
        if (_angleBetweenPlayerLeftSideAndEnemy >= 90)
        {
            changeOnIdleOrInMoveSprite(idleSpriteIndex, inMoveSpriteIndex);
            undoSpriteFlip();
        }
        else
        {
            if (duringFlippedAnimation == false)
            {
                flipSprite();
            }
            changeOnIdleOrInMoveSprite(idleSpriteIndex, inMoveSpriteIndex);
            duringFlippedAnimation = true;
        }
    }
}