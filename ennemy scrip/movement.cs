using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;

public class movement : MonoBehaviour
{
    [SerializeField][Header("physic check")] LayerMask groundCheck;
    [SerializeField] Vector2 sizeBoxcast;
    [SerializeField] float castDistance;

    [SerializeField][Header("movement")] float speedMovement = 4;
    [SerializeField] float taget_speed_move;
    [SerializeField] float speedMoveIfJump;
    [SerializeField] float time_after_accleration = 1;
    [SerializeField] float time_end_accleration = 0.3f;
    
    bool start_accleration = false;
    float step_cound;
    float step_cound_end;

    [SerializeField][Header("jump")] float timeWaitJump = 0.3f;
    [SerializeField] public float forceJump = 10;
 
    public Rigidbody2D eRigidbody { get; set; }
    public float Horizontal { get; set; }

    bool faceLook = true;
    public bool canJump { get;set; }
    public bool canMove { get;set; }

    attack enemyAttack;

    void Start()
    {
        canMove = true;
        canJump = true;
        enemyAttack = GetComponent<attack>();
        eRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayerController();
      /*  HandleJumpGravity();*/
        if (Horizontal > 0 && !faceLook)
            Flip();
        else if (Horizontal < 0 && faceLook)
            Flip();;
       
    }
    
    private void Update()
    {
        cound_time_accleration();
       
    }
    public void MoveInput(InputAction.CallbackContext context)
    {
        Horizontal = context.action.ReadValue<Vector2>().x;
        
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<float>() == 1 && GroundCheck() && canJump)
        {
            eRigidbody.velocity = new Vector2(eRigidbody.velocity.x, forceJump);
            StartCoroutine(WaitJump());
        }
    }

    private void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        faceLook = !faceLook;
    }

    public bool GroundCheck()
    {
        return Physics2D.BoxCast(transform.position, sizeBoxcast, 0f, Vector2.down, castDistance, groundCheck);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position - transform.up * castDistance, sizeBoxcast);
    }

    private IEnumerator WaitJump()
    {
        yield return new WaitForSeconds(timeWaitJump);
        canJump = true;
    }
   

    bool start_cound;
    void MovePlayerController()
    {
        if (!canMove)
        {
            eRigidbody.velocity = new Vector2(0, eRigidbody.velocity.y);
            return;
        }
           
         if (math.abs(eRigidbody.velocity.y) > 0.1)
        {
            eRigidbody.velocity = new Vector2(Horizontal * speedMoveIfJump, eRigidbody.velocity.y);
        }
        else
        {
            if (!start_accleration)
            {
                eRigidbody.velocity = new Vector2(Horizontal * speedMovement, eRigidbody.velocity.y);
                if (Horizontal != 0)
                    start_cound = true;
                else
                {
                    start_cound = false;
                    step_cound = 0;
                }

            }
            else
            {
                eRigidbody.velocity = new Vector2(Horizontal * taget_speed_move, eRigidbody.velocity.y);
                start_cound = false;
                step_cound = 0;
            }
        }
    }
    void cound_time_accleration()
    {
        if (start_cound)
        {
            if (step_cound < time_after_accleration)
            {
                step_cound += Time.deltaTime;
            }
            else
            {
                start_accleration = true;
            }
        }
        if (start_accleration)
        {
            if (Horizontal == 0)
            {
                step_cound_end += Time.deltaTime;
                if (step_cound_end > time_end_accleration)
                {
                    step_cound_end = 0;
                    start_accleration = false;
                }
            }
            else if (Horizontal != 0 && step_cound_end < time_end_accleration)
            {
                step_cound_end = 0;
            }

        }
    }
    public float get_target_speed_move()
    {
        return taget_speed_move;
    }
}
