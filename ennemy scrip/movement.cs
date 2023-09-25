using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
public class movement : MonoBehaviour
{

    [SerializeField] LayerMask ground_check;
    [SerializeField] Vector2 size_boxcast;
    [SerializeField] float speed_movement = 4, focre_jum = 10, casDistance, time_wait_jum = 0.3f;
   
    public  Rigidbody2D e_rigd { get; set; }
    public float Horizontal { get; set; }
    bool facelook=true,can_jum=true;
    float speed_move_if_attack;
    attack enemy_attack;
    void Start()
    {
        enemy_attack = GetComponent<attack>();
        speed_move_if_attack = speed_movement*0f;
        e_rigd = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

        if (enemy_attack.is_hit) { e_rigd.velocity = new Vector2(Horizontal * speed_move_if_attack, e_rigd.velocity.y); }
          else  e_rigd.velocity = new Vector2(Horizontal * speed_movement, e_rigd.velocity.y);
        if (Horizontal > 0 && !facelook)
            flip();
        else if (Horizontal < 0 && facelook)
            flip();
    }
    private void FixedUpdate()
    {
        jum_gravity();
    }
    // Update is called once per frame
    public void Move(InputAction.CallbackContext context)
    {
            Horizontal = context.action.ReadValue<Vector2>().x;
        
    }
    public void Jum(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<float>()==1&& groundCheck() == true&& can_jum == true)
        {
            e_rigd.velocity = new Vector2(e_rigd.velocity.x,focre_jum);
            can_jum = false;
            StartCoroutine(waitjum());
        }
    }
    private void flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facelook = !facelook;
    }
    public bool groundCheck()
    {

        if (Physics2D.BoxCast(transform.position, size_boxcast, 0f, Vector2.down, casDistance, ground_check))
        return true;
        else return false;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position-transform.up*casDistance,size_boxcast);
    }
    private IEnumerator waitjum() {
       yield return new WaitForSeconds(time_wait_jum);
       
        can_jum = true;
    }
    void jum_gravity()
    {
        if (e_rigd.velocity.y < -0.01)
        {
            e_rigd.gravityScale = 1.5f;
        }
        else {
            e_rigd.gravityScale = 1.3f;   

                }
    }
}
