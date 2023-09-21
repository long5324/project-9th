using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        e_rigd = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        e_rigd.velocity = new Vector2(Horizontal * speed_movement, e_rigd.velocity.y);
        if (Horizontal > 0 && !facelook)
            flip();
        else if (Horizontal < 0 && facelook)
            flip();
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
        Vector2 Scale =  transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
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
}
