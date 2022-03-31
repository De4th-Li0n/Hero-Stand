using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; //create variable for speed
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("JumpSound")]
    [SerializeField] private AudioClip JumpSound;


    private Rigidbody2D body;
    private Animator anim;    
    private CapsuleCollider2D CapsuleCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    PlayerPosition playerPosData;
   
    private void Awake() 
    {   
        //Grab reference
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();
        playerPosData = FindObjectOfType<PlayerPosition>();
        playerPosData.PlayerPosLoad();
    }

    // Update is called once per frame
    private void Update() 
    {
        horizontalInput = Input.GetAxis("Horizontal");
               
        //Flip player when moving left
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Check if space is press to jump
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
            Jump();

        //Set Animator parameters
        anim.SetBool("Run", horizontalInput != 0); //if not press then do idle if not do run
        anim.SetBool("Grounded", IsGrounded());

        //wall jump logic
        if (wallJumpCooldown > 0.2f)
        {          
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (OnWall() && !IsGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 5;

            // Check if space is press to jump
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();

                if(Input.GetKeyDown(KeyCode.Space))
                    SoundManager.instance.PlaySound(JumpSound);
            }                
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            body.gravityScale = 1;
            anim.SetTrigger("Jump");

        }
        else if (OnWall() && !IsGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localPosition.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localPosition.x) * 3, 6);
            }
            wallJumpCooldown = 0;            
        }               
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(CapsuleCollider.bounds.center, CapsuleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(CapsuleCollider.bounds.center, CapsuleCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && IsGrounded() && !OnWall();
    }
}
