using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    bool moveMode;

    public float mouseSensitivity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveMode = !moveMode;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.position = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (moveMode)
        {
            rb.AddForce((Vector2)Input.mousePositionDelta * Time.deltaTime * mouseSensitivity);
            mouseSensitivity = 100;
        }
        else
        {
            rb.MovePosition(rb.position + (Vector2)Input.mousePositionDelta * Time.deltaTime * mouseSensitivity);
            mouseSensitivity = 1;
        }



    }
}
