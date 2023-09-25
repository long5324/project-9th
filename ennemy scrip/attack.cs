using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.InputSystem;
public class attack : MonoBehaviour
{

    [SerializeField] LayerMask threa_layer;
    [SerializeField] Vector2 size_box, distance;
    [SerializeField] float Speed_attack;
    bool is_click,can_attack;

    public bool is_hit  { get; set; }
    movement enemy_movement;
    private void Start()
    {
        is_hit = false;
        can_attack = true;
        enemy_movement = GetComponent<movement>();
    }
    public void attack1(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<float>() !=1)
        {
            is_click=false;
        }
        else
        {
            is_click = true;
        }
    }
    bool Check_threat()
    {
        if (Physics2D.BoxCast(transform.position+ transform.right*distance.x*transform.localScale.x+transform.up*distance.y,size_box , 0f, Vector2.left, 0f, threa_layer))
          return true;
        else return false;

    }
    private void OnDrawGizmos()
    {
       Gizmos.color = UnityEngine.Color.yellow;
       Gizmos.DrawWireCube(transform.position + transform.right * distance.x * transform.localScale.x + transform.up * distance.y, size_box);
    }
    // Update is called once per frame
    void Update()
    {
        if (is_click && can_attack)
        {
            can_attack = false;
            is_hit = true;
        }
        
        if (is_hit)
        {
            Check_threat();
        }
    }
    public void wait_time_hellper()
    {
        
        StartCoroutine(wait_attack());
    }
    private IEnumerator wait_attack()
    {
        yield return new WaitForSeconds(Speed_attack);
        can_attack = true;
        Debug.Log("pl");
    }
}
