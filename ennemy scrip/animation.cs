using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class animation : MonoBehaviour
{
    [SerializeField] GameObject object_throw_sword;
    [SerializeField]GameObject object_sword_effect;
    [SerializeField] float timeCanCombo = 0.5f;
    [SerializeField] float timeActiveEffectRun;
    [SerializeField] GameObject effectRun;
    [SerializeField] GameObject jumpEffect, fallEffect ;

    info_player if_player;

    attack enemyAttack;
    movement movementEnemy;
    Animator animatorEnemy;
    Animator swordEffect_animator;


    Vector2 position_sword_effect_defaul;
    float attackNb;
    bool checkOnEffect = false;

    private void Start()
    { 
        if_player= GetComponentInParent<info_player>();
        attackNb = 0;
        position_sword_effect_defaul =object_sword_effect.transform.localPosition;
        stepWait = timeCanCombo;
        enemyAttack = GetComponentInParent<attack>();
        movementEnemy = GetComponentInParent<movement>();
        animatorEnemy = GetComponent<Animator>();
        swordEffect_animator = object_sword_effect.GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        // Set run effect
        SetRunEffect();

        // Set jump effect
        SetJumpEffect();

        // Set fall effect
        FallEffect();

        // attack combo  effect

        // Set jump
        SetJump();

        // Set velocity animation
       set_velocity_animation();


        if(!if_player.has_wapon)
        return;
        AttackCombo();
        animatorEnemy.SetBool("attack", enemyAttack.IsHit);
    }
    
    private void Update()
    {
        // Set jump
        if (Mathf.Abs(movementEnemy.eRigidbody.velocity.y) > 0.1f)
        {
            animatorEnemy.SetBool("buttun_jump", true);
        }
        if (startLoad)
        {
            stepWait -= Time.deltaTime;
        }

    }
    private void FixedUpdate()
    {
        HandleJumpGravity();
    }
    void set_velocity_animation()
    {
        animatorEnemy.SetFloat("velocityy", movementEnemy.eRigidbody.velocity.y);
        animatorEnemy.SetFloat("velocityx", Mathf.Abs(movementEnemy.eRigidbody.velocity.x));
    }
    public void SetAllAttackFalse()
    {
       
        if (attackNb == 1)
            animatorEnemy.SetBool("attack1", false);
        else if (attackNb == 2)
            animatorEnemy.SetBool("attack2", false);
        else if (attackNb == 3)
            animatorEnemy.SetBool("attack3", false);
        else if (attackNb == 4)
        {
            animatorEnemy.SetBool("attack4", false);
            animatorEnemy.SetBool("attack5", false);
        }
    }
    private void end_throw_sword_animation()
    {
            animatorEnemy.SetBool("throw", false);
    }
    public void Start_counddow_attack()
    {
        startLoad = true;
    }
    public void set_attack_effect_false()
    {
        swordEffect_animator.SetBool("attack1", false);
        swordEffect_animator.SetBool("attack2", false);
        swordEffect_animator.SetBool("attack3", false);
        swordEffect_animator.SetBool("attack4", false);
        swordEffect_animator.SetBool("attack5", false);


    }

    #region attack combo effect
   

    public void attack_combo_effect()
    {
        if (attackNb==1)
        {
            swordEffect_animator.SetBool("attack1", true);
        }
        else if (attackNb == 2)
        {
            swordEffect_animator.SetBool("attack2", true);
        }
        else if (attackNb == 3)
        {
            swordEffect_animator.SetBool("attack3", true);
        }
    }

    public void set_effect_air_attack_1_true()
    {
        swordEffect_animator.SetBool("attack4", true);

    }
    public void set_effect_air_attack_2_true()
    {
         swordEffect_animator.SetBool("attack5", true);

    }

    #endregion

    void on_throw_sword()
    {
        object_throw_sword.SetActive(true) ;
        object_throw_sword.transform.position = transform.position;
        animatorEnemy.SetBool("has_weapon", false);
    }
    bool startLoad;
    #region attack combo
    private void AttackCombo()
    {

        if (enemyAttack.is_throw_sword)
        {
            animatorEnemy.SetBool("throw", true);
            enemyAttack.is_throw_sword = false;
            return;
        }

        if (animatorEnemy.GetBool("buttun_jump")&& enemyAttack.check_air_attack())
        {
            if(attackNb!=4)
            SetAllAttackFalse();
            return;
        }
        if (!enemyAttack.check_air_attack())
        {
            if (animatorEnemy.GetBool("attack"))
            {
                if (attackNb != 4 && attackNb != 5)
                {
                    animatorEnemy.SetBool("attack4", true);
                    attackNb = 4;
                }
            }

            return;

        }
        else if (movementEnemy.GroundCheck() && attackNb == 4){ attackNb = 0;return; }
        if (animatorEnemy.GetBool("attack") && attackNb == 0)
            {
                attackNb = 1;
                animatorEnemy.SetBool("attack1", true);
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
        }
        else if (stepWait < 0)
        {

            attackNb = 0;
            stepWait = timeCanCombo;
            startLoad = false;
        }
    }
    #endregion




    #region jjump and faill effect;

    private float stepWait;
    

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
    bool can_active_faill_effect=false;

    private void FallEffect()
    {
        if (!enemyAttack.check_air_attack() && !can_active_faill_effect&& !movementEnemy.GroundCheck())
        {
            can_active_faill_effect = true;
        }
        else if (can_active_faill_effect && movementEnemy.GroundCheck() )
        {
            SpawnEffect(fallEffect, transform.position);
            can_active_faill_effect = false;
        }
    }
    #endregion


    #region run effect
    private void SetRunEffect()
    {
        if (animatorEnemy.GetBool("buttun_jump"))
        {
            effectRun.SetActive(false);
            return;
        }
        if (  Mathf.Abs(movementEnemy.eRigidbody.velocity.x)+0.01f>movementEnemy.get_target_speed_move() )
        {
            effectRun.SetActive(true);
        }
        else
        {
            effectRun.SetActive(false);
        }
        
    }
    #endregion


    private void SetJump()
    {
        if (movementEnemy.GroundCheck() && animatorEnemy.GetBool("buttun_jump") == true && movementEnemy.eRigidbody.velocity.y < 0.1f)
        {
            animatorEnemy.SetBool("buttun_jump", false);
        }
    }

    private void attack_air_hellper(){
     movementEnemy.eRigidbody.velocity = new Vector2(movementEnemy.eRigidbody.velocity.x, 0);
     movementEnemy.eRigidbody.gravityScale = 0.1f;
    }

    void HandleJumpGravity()
    {
        if (!animatorEnemy.GetCurrentAnimatorStateInfo(0).IsName("attack 4"))
        {
            movementEnemy.eRigidbody.gravityScale = movementEnemy.eRigidbody.velocity.y < -0.01f ? 1.5f : 1.3f;
            object_sword_effect.transform.localPosition = position_sword_effect_defaul;
        }
        else
        {
            object_sword_effect.transform.localPosition = new Vector2(0.9f, -0.5f);
        }
    }
    public  void end_air_attack()
    {
        movementEnemy.eRigidbody.gravityScale = 1.5f;
    }
    private void SpawnEffect(GameObject a,  Vector2 pos)
    {
        Instantiate(a, pos, Quaternion.identity, null);
    }
    private void set_true_move()
    {
          movementEnemy.canMove = true;
    }
    private void set_false_move()
    {
         movementEnemy.canMove = false;
    }
}
