using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform target;
    public float chaseSpeed;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {


        Vector2 direction = (target.position - transform.position).normalized;
        float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime * chaseSpeed);
        rb.SetRotation(Angle);

    }


}
