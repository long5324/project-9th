using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.InputSystem;
public class attack : MonoBehaviour
{
    [SerializeField] LayerMask threa_layer;
    [SerializeField] Vector2 size_box, distance;
    [SerializeField] float Speed_attack;
    bool can_attack=true;
    public bool is_hit { get; set; }
    movement enemy_movement;
    private void Start()
    {
        enemy_movement = GetComponent<movement>();
    }
    public void attack1(InputAction.CallbackContext context)
    {
        if (can_attack)
        {
            can_attack = false;
            is_hit = true;
            StartCoroutine(time_wait_attack());
        }
    }
    IEnumerator time_wait_attack()
    {
        yield return new WaitForSeconds(Speed_attack);
        can_attack=true;
    }
    bool Check_threat()
    {
        if (Physics2D.BoxCast(transform.position+ transform.right*distance.x*transform.localScale.x+transform.up*distance.y,size_box , 0f, Vector2.left, 0f, threa_layer))
          return true;
        else return false;

    }
    private void OnDrawGizmos()
    {
       Gizmos.DrawCube(transform.position + transform.right * distance.x * transform.localScale.x + transform.up * distance.y, size_box);
    }
    // Update is called once per frame
    void Update()
    {
        if(is_hit)
        {
            Check_threat();
        }
    }
}
