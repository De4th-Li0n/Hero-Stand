using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 2;
    int currentHealth;    
    public Animator anim;

    [Header("DeathSound")]
    [SerializeField] private AudioClip DeathSound;

    [Header("HurtSound")]
    [SerializeField] private AudioClip HurtSound;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }    

    public void TakeDamage(int damage)
    {
        SoundManager.instance.PlaySound(HurtSound);
        currentHealth -= damage;
        anim.SetTrigger("IsHurt");
        if(currentHealth <= 0)
        {
            Die();
            SoundManager.instance.PlaySound(DeathSound);
        }
    }
    void Die()
    {        
        //Die animation
        anim.SetBool("IsDead", true);        
        GetComponent<Collider2D>().enabled = false;
        //Disable Enemy (1f is for delaying for animation to play)        
        Destroy(gameObject,1f);
    }    

}
