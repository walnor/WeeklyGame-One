using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody2D m_RigidBody;
    Animator animator;
    SpriteRenderer spRender;
    Vector2 velocity;

    [SerializeField] float speed = 5.0f;
    [SerializeField] float JumpPower = 10.0f;
    [SerializeField] float m_MaxSpeed = 20.0f;

    [SerializeField] Transform m_contact = null;
    [SerializeField] LayerMask m_groundMask;

    AudioSource m_jumpSound;

    bool m_onGround = false;

    bool m_buttonMove = false;

    [SerializeField] [Range(0.1f, 10.0f)] float m_fallMultiplyer = 1.0f;
    [SerializeField] [Range(0.1f, 10.0f)] float m_JumpMultiplyer = 1.0f;

    [SerializeField] GameController m_GC;
    

    // Use this for initialization
    void Start () {

        m_RigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spRender = GetComponent<SpriteRenderer>();

        m_jumpSound = GetComponent<AudioSource>();

        m_jumpSound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        m_onGround = Physics2D.OverlapCircle(m_contact.transform.position, 0.5f, m_groundMask);

        if (velocity.x > 0.01)
        {
            spRender.flipX = false;
        }
        else
        if(velocity.x < -0.01)
        {
            spRender.flipX = true;
        }

        if (Input.GetButtonDown("Jump") && m_onGround)
        {
            Jump();
            animator.SetBool("Jump", true);
        }
        else
        {
            if(animator.GetBool("OnGround"))
            animator.SetBool("Jump", false);
        }

        animator.SetBool("OnGround", m_onGround);
        animator.SetFloat("FallSpeed", m_RigidBody.velocity.y);

    }

    public void Jump()
    {
        if (!m_onGround)
            return;
        
        m_RigidBody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        animator.SetBool("Jump", true);
        m_jumpSound.Play();
    }

    public void moveLeft()
    {
        velocity = Vector2.zero;

        velocity.x -= speed;
        m_buttonMove = true;
    }

    public void moveRight()
    {
        velocity = Vector2.zero;

        velocity.x += speed;
        m_buttonMove = true;

    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (!m_buttonMove)
        {
            velocity = Vector2.zero;
            velocity.x = horizontal * speed;
        }

        if (m_RigidBody.velocity.y < 0.0f)
        {
            m_RigidBody.velocity += (Vector2.up * Physics2D.gravity.y) * (m_fallMultiplyer - 1.0f) * Time.deltaTime;
        }
        else
        if (m_RigidBody.velocity.y > 0.0f)
        {
            m_RigidBody.velocity += (Vector2.up * Physics2D.gravity.y) * (m_JumpMultiplyer - 1.0f) * Time.deltaTime;
        }


        if(Mathf.Abs(m_RigidBody.velocity.x) < m_MaxSpeed)
            m_RigidBody.AddForce(velocity, ForceMode2D.Impulse);

        animator.SetFloat("Walk Velocity", Mathf.Abs(m_RigidBody.velocity.x) * 0.1f);
        m_buttonMove = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeathBox")
        {
            gameObject.SetActive(false);
            m_GC.GameEnd();
        }
    }
}
