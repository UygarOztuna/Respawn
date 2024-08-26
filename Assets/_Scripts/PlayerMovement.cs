using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private float moveInput;
    private Rigidbody2D rb;
    private SpriteRenderer playerRenderer;
    private PlayerMovement playerScript;
    //private bool isGrounded;
    [SerializeField] private float gravityScale;
    [SerializeField] private float fallGravityScale;
    [SerializeField] private bool facingRight = true;
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float radius;
    private Color deathColor = Color.gray;

    [SerializeField] private Sprite[] sprites;
    private int i = 0;
    private void Awake()
    {
        
    }

    void Start()
    {
        CameraFollow.Instance.players.Add(this.gameObject);
        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerScript = GetComponent<PlayerMovement>();
        CameraFollow.Instance.smoothSpeed = 1;
    }

    void Update()
    {
        Move();
        Jump();
        Flip();
        ResetTheLevel();
        ChangeSprite();
    }

    void Move()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(feetPos.position, radius, layerMask);

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if(rb.velocity.y >= 0)
        {
            rb.gravityScale = gravityScale;
        }
        else if(rb.velocity.y < 0 )
        {
            rb.gravityScale = fallGravityScale;
        }
    }

    private void FlipWithRenderer()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Flip()
    {
        if(moveInput < 0 && facingRight)
        {
            FlipWithRenderer();
        }
        else if(moveInput > 0 && !facingRight)
        {
            FlipWithRenderer();
        }
    }

    private void ResetTheLevel()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void ChangeSprite()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(i == 0)
            {
                i++;
            }
            else if(i > 0)
            {
                i--;
            }
            
            playerRenderer.sprite = sprites[i];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dead"))
        {
            CameraFollow.Instance.smoothSpeed = 0.125f;
            rb.velocity = new Vector2(0, 0);
            GameManager.Instance.lives--;
            playerScript.enabled = false;
            playerRenderer.color = deathColor;
            rb.mass = 10000;
            rb.bodyType = RigidbodyType2D.Kinematic;
            Spawner.Instance.Spawn();
            
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            GameManager.Instance.isWin = true;
        }
    }


}
