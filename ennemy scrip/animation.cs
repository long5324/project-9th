using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class animation : MonoBehaviour
{
    attack enemy_attack;
    movement movement_enemy;
    Animator animator_enemy;
    float time_wait_attack = 0.5f;
    public float step_wait { get; set; }
    float attack_nb;
   public bool start_load { get; set; }
    private void Start()
    {
        step_wait = time_wait_attack;
        enemy_attack = GetComponentInParent<attack>();
        movement_enemy = GetComponentInParent<movement>();
        animator_enemy = GetComponent<Animator>();
    }
    private void LateUpdate()
    {
         animator_enemy.SetBool("attack", enemy_attack.is_hit);
        if (animator_enemy.GetBool("attack") && attack_nb ==0)
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
    }
    private void Update()
    {
        if (start_load)
        {
            step_wait -= Time.deltaTime;
        }
        if(step_wait > 0&& step_wait<time_wait_attack && enemy_attack.is_hit == true)
        {
            if (attack_nb==1)
            {
                animator_enemy.SetBool("attack2", true);
                start_load = false;
                step_wait = time_wait_attack;
                attack_nb = 2;
            }
            else if(attack_nb==2)
            {
                animator_enemy.SetBool("attack3", true);
                start_load = false;
                step_wait = time_wait_attack;
                attack_nb = 3;            }
            else if(attack_nb==3)
            {
                animator_enemy.SetBool("attack1", true);
                start_load = false;
                step_wait = time_wait_attack;
                attack_nb = 0;
            }
        }
        else if(step_wait <= 0)
        {
            attack_nb = 0;
            step_wait = time_wait_attack;
            start_load = false;
            set_all_attack_false();
        }
    }
    public void set_all_attack_false()
    {
        animator_enemy.SetBool("attack2", false);
        animator_enemy.SetBool("attack1", false);
        animator_enemy.SetBool("attack3", false);
    }
    public void Jum_animator(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue <float>()== 1)
        {
            animator_enemy.SetBool("buttun_jump", true);
        }
    }
    

}
