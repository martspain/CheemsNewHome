using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed = 10f;
    public float jumpForce = 100f;
    public Canvas pausa;
    public AudioClip JumpSFX;
    public AudioClip HitSFX;
    public AudioClip PowerUpSFX;
    public AudioClip CoinGottenSFX;
    public AudioClip DestroySFX;
    public GameObject deathScreen;
    public GameObject chest;

    private AudioSource sonido;
    private Animator animatior;
    private float horizontalInput;
    private bool isDEAD;
    private bool facingRight;
    private bool facingLeft;
    private Rigidbody2D rb;
    private Vector2 velocidad;
    private SpriteRenderer render;
    private int gemas = 0;
    private bool hasKey;
    void Start()
    {
        sonido = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        velocidad = new Vector2();
        render = GetComponent<SpriteRenderer>();
        facingRight = true;
        animatior = GetComponent<Animator>();
        isDEAD = false;
        if (deathScreen)
            deathScreen.SetActive(false);
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

        else if (other.CompareTag("Pass"))
        {
            hasKey = true;
            Destroy(other.gameObject);
            if (CoinGottenSFX)
                sonido.PlayOneShot(CoinGottenSFX);
        }
            

        else if (other.CompareTag("Enemy"))
        {

            if (gemas >= 4)
            {
                Destroy(other.gameObject);
                if (sonido)
                    sonido.PlayOneShot(DestroySFX);

            }
            else
            {
                kill();
            }
        }
        else if (other.CompareTag("AbsoluteDeath"))
            kill();
        else if (other.CompareTag("LevelDoor"))
        {
            if (hasKey)
            {
                if(chest)
                    chest.GetComponent<Animator>().SetBool("isOpen", true);
                if (SceneManager.GetSceneByBuildIndex(1) == SceneManager.GetActiveScene())
                    SceneManager.LoadScene(2);
                else if (SceneManager.GetSceneByBuildIndex(2) == SceneManager.GetActiveScene())
                    SceneManager.LoadScene(3);
            }
        }
    }

    private void getGema(Collider2D other)
    {
        if(CoinGottenSFX)
            sonido.PlayOneShot(CoinGottenSFX);
        Destroy(other.gameObject);
        if (++gemas >= 4)
        {
            if(PowerUpSFX)
                sonido.PlayOneShot(PowerUpSFX);
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
            animatior.SetBool("isJumping", false);
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            animatior.SetBool("isJumping", false);
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.transform.parent = null;
        }
    }

    private void Jump()
    {
        if (!isDEAD && rb)
        {
            if(Mathf.Abs(rb.velocity.y) < 0.05f)
            {
                if (JumpSFX)
                    sonido.PlayOneShot(JumpSFX);
                if (animatior)
                    animatior.SetBool("isJumping", true);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
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
        if(HitSFX)
            sonido.PlayOneShot(HitSFX);
        isDEAD = true;
        if (rb)
            Destroy(rb);
        animatior.SetBool("isDamaged", true);
        if(deathScreen)
            deathScreen.SetActive(true);
    } 

}   