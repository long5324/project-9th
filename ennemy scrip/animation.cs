using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class animation : MonoBehaviour
{
    movement movement_enemy;
    Animator animator_enemy;
    private void Start()
    {
        movement_enemy = GetComponentInParent<movement>();
        animator_enemy = GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        animator_enemy.SetFloat("velocityy", movement_enemy.e_rigd.velocity.y);
        animator_enemy.SetFloat("velocityx", Math.Abs(movement_enemy.e_rigd.velocity.x));
        if (movement_enemy.groundCheck()&&animator_enemy.GetBool("buttun_jump")==true&& movement_enemy.e_rigd.velocity.y <0.1 )
        {
            animator_enemy.SetBool("buttun_jump", false);
        }
    }
    public void Jum_animator(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue <float>()== 1)
        {
            animator_enemy.SetBool("buttun_jump", true);
        }
    }
}
