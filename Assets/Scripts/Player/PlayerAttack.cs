using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float nextAttacktime = 0f;
    public float AttackRate = 2f;
    public Animator anim;
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int AttackDamage = 1;

    [Header("MeleSound")]
    [SerializeField] private AudioClip MeleSound;


    private void Update()
    {   
        if(Time.time >= nextAttacktime)
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlayerAttackMele();
                nextAttacktime = Time.time + 0.5f / AttackRate;
            }

    }    
    private void PlayerAttackMele()
    {
        SoundManager.instance.PlaySound(MeleSound);
        //play attack animation
        anim.SetTrigger("Attack");
        //detect if enemy is in range
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
        //damage them
        foreach(Collider2D enemy in hitEnemys)
        {
            enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}
