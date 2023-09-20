using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class animation : MonoBehaviour
{
    bool is_run;
    Animator animator_enemy;
    private void Start()
    {
        animator_enemy = GetComponent<Animator>();
    }
    private void Update()
    {
        if(is_run)
        animator_enemy.SetTrigger("isRun");
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<Vector2>().x != 0)
        is_run = true;
        else
        is_run = false;
    }
}
