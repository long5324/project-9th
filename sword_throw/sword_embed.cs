using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class sword_embed : MonoBehaviour
{
    GameObject embed_effect;
    bool on_embed_sword;
    Animator sword_animator;
    movement_sword movement;
    void Start()
    {
        sword_animator= GetComponent<Animator>();
        movement = GetComponent<movement_sword>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("map"))
        {
            on_embed_sword = true;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if(on_embed_sword) {
            sword_animator.SetBool("embed", true);
            movement.can_move = false;
        }
    }
    void active_effect()
    {
        embed_effect.SetActive(true);
    }
}
