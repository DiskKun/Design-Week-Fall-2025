using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;


    //bool moveMode;


    public float mouseSensitivity;
    public GameManager gm;
    

    public TextMeshProUGUI mpostext;

    Vector2 mouseDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
        mouseDirection = Vector2.zero;
        mouseSensitivity = 100;
        animator = GetComponent<Animator>();

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
            mouseDirection = Vector2.zero;
        }
        mouseDirection = mouseDirection + (Vector2)Input.mousePositionDelta * Time.deltaTime * mouseSensitivity;
        mpostext.text = "X: " + mouseDirection.x + " Y: " + mouseDirection.y;

        bool isMoving = rb.linearVelocity!= Vector2.zero;
        string playerDirection = "";

        if (isMoving)
        {
            animator.enabled = true;

            if (Mathf.Abs(rb.linearVelocity.x) > Mathf.Abs(rb.linearVelocity.y))
            {
                playerDirection = rb.linearVelocity.x < 0 ? "Left" : "Right";
            }
            else
            {
                playerDirection = rb.linearVelocity.y < 0 ? "Down" : "Up";
            }

            animator.Play("Walk_" + playerDirection);
        }
        else
        {
            animator.Play("Walk_" + playerDirection, 0, 0f);
            animator.enabled = false;
        }



    }

    private void FixedUpdate()
    {

        rb.AddForce((Vector2)Input.mousePositionDelta * Time.deltaTime * mouseSensitivity);
        //rb.AddForce(mouseDirection * Time.deltaTime);




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            gm.SetLife(GameManager.lives - 1);
            collision.gameObject.GetComponent<Animator>().SetTrigger("Eat");
            gameObject.SetActive(false);
            //gm.Respawn();
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            collision.gameObject.SetActive(false);
            gm.SetScore(GameManager.score + 1);
        }
    }
}
