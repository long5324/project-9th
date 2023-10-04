using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class animation : MonoBehaviour
{
    [SerializeField] Animator swordEffect;
    [SerializeField] float timeCanCombo = 0.5f;
    [SerializeField] float timeActiveEffectRun;
    [SerializeField] GameObject effectRun;
    [SerializeField] GameObject jumpEffect, fallEffect;

    attack enemyAttack;
    movement movementEnemy;
    Animator animatorEnemy;

    float attackNb;
    bool checkOnEffect = false, checkOnEffectFall = false;

    private void Start()
    {
        attackNb = 0;
        stepWait = timeCanCombo;
        enemyAttack = GetComponentInParent<attack>();
        movementEnemy = GetComponentInParent<movement>();
        animatorEnemy = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        // Set run effect
        SetRunEffect();

        // Set jump effect
        SetJumpEffect();

        // Set fall effect
        FallEffect();

        // Set jump
        SetJump();

        // Set velocity animation
        animatorEnemy.SetFloat("velocityy", movementEnemy.eRigidbody.velocity.y);
        animatorEnemy.SetFloat("velocityx", Mathf.Abs(movementEnemy.eRigidbody.velocity.x));
        animatorEnemy.SetBool("attack", enemyAttack.IsHit);
        swordEffect.SetBool("attack1", animatorEnemy.GetBool("attack1"));
        swordEffect.SetBool("attack2", animatorEnemy.GetBool("attack2"));
        swordEffect.SetBool("attack3", animatorEnemy.GetBool("attack3"));

        AttackCombo();
    }

    private void Update()
    {
        // Set jump
        if (Mathf.Abs(movementEnemy.eRigidbody.velocity.y) > 0.1f)
        {
            animatorEnemy.SetBool("buttun_jump", true);
        }
    }

    public void SetAllAttackFalse()
    {
        startLoad = true;
        animatorEnemy.SetBool("attack2", false);
        animatorEnemy.SetBool("attack1", false);
        animatorEnemy.SetBool("attack3", false);
        animatorEnemy.SetBool("attack4", false);
        animatorEnemy.SetBool("attack5", false);
    }

    private void AttackCombo()
    {
        if (animatorEnemy.GetBool("attack") && attackNb == 0 && !animatorEnemy.GetBool("buttun_jump"))
        {
            attackNb = 1;
            animatorEnemy.SetBool("attack1", true);
        }
        else if (startLoad)
        {
            stepWait -= Time.deltaTime;
        }

        if (stepWait > 0 && stepWait < timeCanCombo && enemyAttack.IsHit)
        {
            if (attackNb == 1)
            {
                animatorEnemy.SetBool("attack2", true);
                startLoad = false;
                stepWait = timeCanCombo;
                attackNb = 2;
            }
            else if (attackNb == 2)
            {
                animatorEnemy.SetBool("attack3", true);
                startLoad = false;
                stepWait = timeCanCombo;
                attackNb = 3;
            }
            else if (attackNb == 3)
            {
                animatorEnemy.SetBool("attack1", true);
                startLoad = false;
                stepWait = timeCanCombo;
                attackNb = 0;
            }
            else if (attackNb == 4 && stepWait > timeCanCombo - 0.1f)
            {
                animatorEnemy.SetBool("attack5", true);
                startLoad = false;
                stepWait = timeCanCombo;
            }
        }
        else if (stepWait <= 0)
        {
            attackNb = 0;
            stepWait = timeCanCombo;
            startLoad = false;
            SetAllAttackFalse();
        }
    }

    private float stepWait;
    private bool startLoad;

    private void SetJumpEffect()
    {
        if (!checkOnEffect && animatorEnemy.GetFloat("velocityy") == movementEnemy.forceJump)
        {
            checkOnEffect = true;
            SpawnEffect(jumpEffect, gameObject.transform.position -  new Vector3(0f,0.1f));
        }
        else if (animatorEnemy.GetFloat("velocityy") != movementEnemy.forceJump)
        {
            checkOnEffect = false;
        }
    }

    private void FallEffect()
    {
        if (!checkOnEffectFall && movementEnemy.GroundCheck() && animatorEnemy.GetFloat("velocityy") < -10f)
        {
            checkOnEffectFall = true;
            SpawnEffect(fallEffect, gameObject.transform.position);
        }
        else if (!movementEnemy.GroundCheck())
        {
            checkOnEffectFall = false;
        }
    }

    private void SetRunEffect()
    {
        if (animatorEnemy.GetFloat("velocityx") == movementEnemy.get_target_speed_move())
        {
            effectRun.SetActive(true);
        }
        else
        {
            effectRun.SetActive(false);
        }
        
    }

    private void SetJump()
    {
        if (movementEnemy.GroundCheck() && animatorEnemy.GetBool("buttun_jump") == true && movementEnemy.eRigidbody.velocity.y < 0.1f)
        {
            animatorEnemy.SetBool("buttun_jump", false);
        }
    }

    private void SpawnEffect(GameObject a,  Vector2 pos)
    {
        Instantiate(a, pos, Quaternion.identity, null);
    }
}
