using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private int Size;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Size = 1;
    }

    // Update is called once per frame
    void Update()
    {

        this.Allign();

    }

    private void FixedUpdate()
    {
        if (rb)
        {
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * 10, 0));
            if (Input.GetAxis("Jump") > 0)
                this.Jump();
        }
    }
    private void Jump() 
    {
        if (rb)
            if (Mathf.Abs(rb.velocity.y) < 0.05f)
                rb.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
    }

    private void Allign()
    {
        Vector2 newScale = transform.localScale;
        if (Input.GetAxis("Horizontal") > 0)
            newScale.x = Size;
        else if (Input.GetAxis("Horizontal") < 0)
            newScale.x = -Size;
        newScale.y = Size;
        transform.localScale = newScale;

    }
}
