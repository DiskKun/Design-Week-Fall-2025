using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    //bool moveMode;

    public float mouseSensitivity;
    public GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    moveMode = !moveMode;
        //}
        if (Input.GetKeyDown(KeyCode.R))
        {
            gm.ResetGame();
        }
    }

    private void FixedUpdate()
    {
        //if (moveMode)
        //{
            rb.AddForce((Vector2)Input.mousePositionDelta * Time.deltaTime * mouseSensitivity);
            mouseSensitivity = 100;
        //}
        //else
        //{
        //    rb.MovePosition(rb.position + (Vector2)Input.mousePositionDelta * Time.deltaTime * mouseSensitivity);
        //    mouseSensitivity = 1;
        //}



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            GameManager.lives -= 1;
            gm.Respawn();
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            collision.gameObject.SetActive(false);
            GameManager.score += 1;
        }
    }
}
