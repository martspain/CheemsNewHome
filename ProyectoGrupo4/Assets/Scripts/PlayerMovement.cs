using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed = 10f;
    public float jumpForce = 40;
    public Canvas pausa;
    public AudioClip JumpSFX;
    public AudioClip HitSFX;
    public AudioClip PowerUpSFX;
    public AudioClip CoinGottenSFX;
    public AudioClip DestroySFX;

    private AudioSource audio;
    private Animator animatior;
    private float horizontalInput;
    private bool isDEAD;
    private bool facingRight;
    private bool facingLeft;
    private Rigidbody2D rb;
    private Vector2 velocidad;
    private bool canJump;
    private SpriteRenderer render;
    private int gemas = 0;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        velocidad = new Vector2();
        canJump = false;
        render = GetComponent<SpriteRenderer>();
        facingRight = true;
        animatior = GetComponent<Animator>();
        isDEAD = false;
    }

    // Update is called once per frame
    void Update()
    {
        run(Input.GetAxis("Horizontal") * horizontalSpeed);
        if (Input.GetButtonDown("Jump"))
            Jump();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
            getGema(other);

        else if (other.CompareTag("Enemy"))
        {

            if (gemas >= 4)
            {
                Destroy(other.gameObject);
                audio.PlayOneShot(DestroySFX);

            }
            else
            {
                kill();
            }
        }
        else if (other.CompareTag("AbsoluteDeath"))
            kill();
    }

    private void getGema(Collider2D other)
    {
        audio.PlayOneShot(CoinGottenSFX);
        Destroy(other.gameObject);
        if (++gemas >= 4)
        {
            audio.PlayOneShot(PowerUpSFX);
            gameObject.transform.localScale = new Vector2(1,1);
        }
    }

    private void run(float horizontalInput)
    {
        if (!isDEAD && rb)
        {
            velocidad = rb.velocity;
            velocidad.x = horizontalInput;
            rb.velocity = velocidad;
        }

        animatior.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalInput));

        if (!facingRight && horizontalInput > 0)
            Flip();
        else if (facingRight && horizontalInput < 0)    
            Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
            animatior.SetBool("isJumping", false);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            canJump = false;
    }

    private void Jump()
    {
        if (canJump && !isDEAD && rb)
        {
            audio.PlayOneShot(JumpSFX);
            if (animatior)
                animatior.SetBool("isJumping", true);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            canJump = false;
        }

    }

    private void Flip()
    {
        if (render)
        {

            render.flipX = facingRight;
            facingRight = !facingRight;
        }
    }

    private void kill()
    {
        audio.PlayOneShot(HitSFX);
        isDEAD = true;
        if (rb)
            Destroy(rb);
        animatior.SetBool("isDamaged", true);
    } 
}   