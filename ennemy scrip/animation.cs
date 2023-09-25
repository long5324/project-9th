using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class animation : MonoBehaviour
{
    [SerializeField] Animator sword_effect, run_effect, jump_effect;
    [SerializeField] float time_can_combo=0.5f;
    
    attack enemy_attack;
    movement movement_enemy;
    Animator animator_enemy;
    public float step_wait { get; set; }
    float attack_nb;
   public bool start_load { get; set; }
    private void Start()
    {
        step_wait = time_can_combo;
        enemy_attack = GetComponentInParent<attack>();
        movement_enemy = GetComponentInParent<movement>();
        animator_enemy = GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        // effct run
        set_run_effect();
        //effect jump
        set_jum_effect();

        animator_enemy.SetBool("attack", enemy_attack.is_hit);
        sword_effect.SetBool("attack1", animator_enemy.GetBool("attack1"));
        sword_effect.SetBool("attack2", animator_enemy.GetBool("attack2"));
        sword_effect.SetBool("attack3", animator_enemy.GetBool("attack3"));

        if (animator_enemy.GetBool("buttun_jump") && animator_enemy.GetBool("attack"))
        {
            if((movement_enemy.e_rigd.velocity.y < 2 && movement_enemy.e_rigd.velocity.y > 0)||( movement_enemy.e_rigd.velocity.y < -5 && movement_enemy.e_rigd.velocity.y > -7))
            animator_enemy.SetBool("attack4", true);
            attack_nb = 4;
        }

        if (animator_enemy.GetBool("attack") && attack_nb ==0&&! animator_enemy.GetBool("buttun_jump"))
        {
            attack_nb = 1;
            animator_enemy.SetBool("attack1", true);
        }
        animator_enemy.SetFloat("velocityy", movement_enemy.e_rigd.velocity.y);
        animator_enemy.SetFloat("velocityx", Math.Abs(movement_enemy.e_rigd.velocity.x));
        if (movement_enemy.groundCheck()&&animator_enemy.GetBool("buttun_jump")==true&& movement_enemy.e_rigd.velocity.y <0.1 )
        {
            animator_enemy.SetBool("buttun_jump", false);
        }
        
        if (start_load)
        {
            step_wait -= Time.deltaTime;
        }
        if (step_wait > 0 && step_wait < time_can_combo && enemy_attack.is_hit == true)
        {
            
            
            if (attack_nb == 1)
            {
                animator_enemy.SetBool("attack2", true);
                start_load = false;
                step_wait = time_can_combo;
                attack_nb = 2;
            }
            else if (attack_nb == 2)
            {
                animator_enemy.SetBool("attack3", true);
                start_load = false;
                step_wait = time_can_combo;
                attack_nb = 3;
            }
            else if (attack_nb == 3)
            {
                animator_enemy.SetBool("attack1", true);
                start_load = false;
                step_wait = time_can_combo;
                attack_nb = 0;
            }
            else if(attack_nb == 4&& step_wait > time_can_combo-0.1)
            {
                animator_enemy.SetBool("attack5", true);
                start_load = false;
                step_wait = time_can_combo;
            }
        }
        else if (step_wait <= 0)
        {
            attack_nb = 0;
            step_wait = time_can_combo;
            start_load = false;
            set_all_attack_false();
        }
    }
    private void Update()
    {
        // attack effect

        
        // attack combo

       
        // set jump
        if (math.abs(movement_enemy.e_rigd.velocity.y) > 0.1) animator_enemy.SetBool("buttun_jump", true);
    }
    public void set_all_attack_false()
    {
        animator_enemy.SetBool("attack2", false);
        animator_enemy.SetBool("attack1", false);
        animator_enemy.SetBool("attack3", false);
        animator_enemy.SetBool("attack4", false);
        animator_enemy.SetBool("attack5", false);
    }
    
    void set_jum_effect()
    {
        jump_effect.SetFloat("velocity y", movement_enemy.e_rigd.velocity.y);
    }
    void set_run_effect()
    {
        run_effect.SetFloat("run", math.abs(movement_enemy.e_rigd.velocity.x));
    }

}
