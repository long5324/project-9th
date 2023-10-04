using System.Collections;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.InputSystem;

public class attack : MonoBehaviour
{
    [SerializeField] LayerMask threatLayer, maplayer;
    [SerializeField] Vector2 boxSize, attackDistance;
    [SerializeField] float attackSpeed, attackairDistance;

    bool isClick;
    public bool IsHit { get; private set; }
    public bool CanAttack { get; private set; }
    movement enemyMovement;

    private void Start()
    {
        IsHit = false;
        CanAttack = true;
        enemyMovement = GetComponent<movement>();
    }

    void Update()
    {
        if (isClick && CanAttack)
        {
            if (enemyMovement.GroundCheck())
            {
                CanAttack = false;
                IsHit = true;
            }
        }

        if (IsHit)
        {
            CheckThreat();
        }

        Debug.Log(check_air_attack());
    }

    public void Attack1(InputAction.CallbackContext context)
    {
        isClick = context.action.ReadValue<float>() == 1;
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
    bool check_air_attack()
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
        StartCoroutine(WaitAttack());
    }

    private IEnumerator WaitAttack()
    {
        yield return new WaitForSecondsRealtime(attackSpeed);
        CanAttack = true;
    }
}
