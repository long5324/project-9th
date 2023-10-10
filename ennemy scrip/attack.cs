using System.Collections;
using Unity.VisualScripting;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.InputSystem;

public class attack : MonoBehaviour
{
    [SerializeField] LayerMask threatLayer, maplayer;
    [SerializeField] Vector2 boxSize, attackDistance;
    [SerializeField] float attackSpeed, attackairDistance;

    info_player if_player;
    bool isClick;
    public bool IsHit { get; private  set; }
    public bool CanAttack { get; private set; }
    public bool is_throw_sword;
    movement enemyMovement;

    private void Start()
    {
        if_player = GetComponent<info_player>();
        IsHit = false;
        CanAttack = true;
        enemyMovement = GetComponent<movement>();
    }

    void Update()
    {
        if (isClick && CanAttack)
        {
            if (enemyMovement.GroundCheck()|| !check_air_attack())
            {
                CanAttack = false;
                IsHit = true;
            }
           
        }
        if (IsHit)
        {
            CheckThreat();
        }

    }

    public void Attack1(InputAction.CallbackContext context)
    {
        isClick = context.action.ReadValue<float>() == 1;
    }
    public void Throw_sword_input(InputAction.CallbackContext context)
    {
        if (if_player.has_wapon&&context.action.ReadValue<float>()==1) {
                   is_throw_sword = true;
        }
    }
    bool CheckThreat()
    {
        if (Physics2D.OverlapBox(
            transform.position + transform.right * attackDistance.x * transform.localScale.x + transform.up * attackDistance.y,
            boxSize, 0f, threatLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireCube(
            transform.position + transform.right * attackDistance.x * transform.localScale.x + transform.up * attackDistance.y,
            boxSize);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position+Vector2.down*attackairDistance);
    }
    public  bool check_air_attack()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down,attackairDistance,maplayer) )
        {
            return true;
        }
        return false;
    }

   
    public void WaitTimeHelper()
    {
        IsHit = false;
        StartCoroutine(WaitAttack(attackSpeed));
    }
    public void WaitTimeHelper_air_attack()
    {
        IsHit = false;
        CanAttack = true;
    }
    private IEnumerator WaitAttack(float a)
    {
        yield return new WaitForSecondsRealtime(a);
        CanAttack = true;

    }
}
