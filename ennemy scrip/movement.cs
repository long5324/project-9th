using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
public class movement : MonoBehaviour
{
    [SerializeField] float speed_movement = 4, focre_jum = 10;
    Rigidbody2D e_rigd;
    float Horizontal;
    bool facelook=true;
    void Start()
    {
        
        e_rigd = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Horizontal > 0 && !facelook)
            flip();
        else if (Horizontal < 0 && facelook)
            flip();
    }
    void FixedUpdate()
    {
        e_rigd.velocity = new Vector2(Horizontal*speed_movement, e_rigd.velocity.y);
    }
    // Update is called once per frame
    public void Move(InputAction.CallbackContext context)
    {
        Horizontal = context.action.ReadValue<Vector2>().x;
    }
    public void Jum(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<bool>())
        {
            e_rigd.velocity = new Vector2(e_rigd.velocity.x,10f);
        }
    }
    private void flip()
    {
        Vector2 Scale =  transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
        facelook = !facelook;
    }
}
