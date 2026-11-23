using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController playerController;
    private bool isAttacking;

    // 공격 설정 변수 추가
    [SerializeField] private Transform attackPoint;

    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackRange = 1.0f;

    [SerializeField] private float attackDuration = 0.8f;
    [SerializeField] private float attackMovemont = 0.3f;
    [SerializeField] private string attackAnimationName = "Attack";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse  = Mouse.current;

        if(mouse == null || isAttacking) return;
        if (playerController.IsNormalState() == false) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            StartCoroutine(AttackAnimation());
        }
    }

    private IEnumerator AttackAnimation()
    {
        isAttacking = true;

        playerController.ChangeState(PlayerState.Attack);

        int attackState = Animator.StringToHash(attackAnimationName);

        if (animator.HasState(0,attackState))
        {
            animator.CrossFade("Attack01", 0.1f);
        }
        else
        {
            Debug.LogWarning(attackAnimationName + "애니메이션 상태가 없습니다.");
        }

        yield return new WaitForSeconds(attackMovemont);

        HitEnemy();

        yield return new WaitForSeconds(Mathf.Max(0f, attackDuration - attackMovemont));

        animator.CrossFade("Blend Tree", 0.1f);
        playerController.ChangeState(PlayerState.Normal);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()     // 플레이어를 선택하면 빨간구체가 나타난다.
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void HitEnemy()
    {
        Collider[] targets = Physics.OverlapSphere(attackPoint.position, attackRange);

        foreach (Collider target in targets)
        {
            EnemyHealth enemy = target.GetComponentInParent<EnemyHealth>();

            if (enemy == null) continue;

            enemy.TakeDamage(attackDamage);
            //break;        break면 1명만 데미지가 들어간다.
        }
    }
}
