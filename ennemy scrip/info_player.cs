using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info_player : MonoBehaviour
{
    public bool has_wapon { get; private set; }
    attack player_attack;

    // Start is called before the first frame update
    void Start()
    {
        player_attack = GetComponent<attack>();
        has_wapon = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(has_wapon)
        {
            player_attack.enabled = true;
        }
        else
        {
            player_attack.enabled=false;
        }
    }
}
