using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] public float jumpForce = 3f;                              
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;                       
    [SerializeField] private LayerMask groundLayer;                          
    [SerializeField] private Transform groundCheck;                           

    const float groundRadius = .2f; 
    public bool grounded;            
    private Rigidbody2D player;
    private bool lookingRight = true;  
    private Vector3 velocity = Vector3.zero;
    UnityEvent OnLandEvent;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 8f, player.velocity.y);
        
        player.velocity = Vector3.SmoothDamp(player.velocity, targetVelocity, ref velocity, movementSmoothing);

        
        if (move > 0 && !lookingRight)
        {
            Flip();
        }
        
        else if (move < 0 && lookingRight)
        {
            Flip();
        }
        
        if (grounded && jump)
        {
            
            grounded = false;
            player.AddForce(new Vector2(0f, jumpForce),ForceMode2D.Impulse);
        }
    }


    private void Flip()
    {
        
        lookingRight = !lookingRight;

        
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
