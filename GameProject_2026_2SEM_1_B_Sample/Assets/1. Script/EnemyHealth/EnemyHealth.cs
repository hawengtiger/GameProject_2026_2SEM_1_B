using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;        // 초기 최대 체력

    [SerializeField] private int currentHealth;      // 현재 남아 있는 체력

    [SerializeField] private GameObject dropItemPrefab;
    [SerializeField] private Vector3 dropOffeset = new Vector3 (0f, 0.5f, 0f);

    private bool isDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)      // 외부에서 피해를 전달 받는 함수 : 적이 자기 스스로 공격을 검사하지 않고 공격하는 쪽에서 함수 호출
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log(name + "남은 체력 : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        isDead = true;

        if (dropItemPrefab != null)
        {
            Instantiate(dropItemPrefab, transform.position + dropOffeset, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
