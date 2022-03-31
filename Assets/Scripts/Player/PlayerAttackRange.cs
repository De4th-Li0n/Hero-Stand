using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;

    [Header("CastSound")]
    [SerializeField] private AudioClip CastSound;

    private Animator anim;
    private PlayerMovement playerMovement;

    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(CastSound);
        anim.SetTrigger("AttackRange");
        cooldownTimer = 0;

        fireBalls[FindFireball()].transform.position = firePoint.position;
        fireBalls[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));        
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }    
}