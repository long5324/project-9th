using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class movement : MonoBehaviour
{
    [SerializeField] float speed_movement = 4;
    Rigidbody2D e_rigd;
    float Horizontal;
    void Start()
    {
        e_rigd = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        e_rigd.velocity = new Vector2(Horizontal*speed_movement, e_rigd.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Move(InputAction.CallbackContext context)
    {
        Horizontal = context.action.ReadValue<Vector2>().x;
    }
}
