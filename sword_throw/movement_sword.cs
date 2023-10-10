using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class movement_sword : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rig_sword;
    public bool can_move { get;set; } 
    private void Start()
    { 
        can_move = true;
        rig_sword = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        if (!can_move)
        {
            rig_sword.velocity = Vector2.zero;
            return;
        }
        rig_sword.velocity = new Vector2(speed, 0);
    }
}
