using UnityEngine;
using Unity.Cinemachine;

public class Monster : MonoBehaviour
{
    public Transform target;
    public float chaseSpeed;
    public GameManager gm;
    public AudioSource audio_death;

    AudioSource audio_monster;
    Rigidbody2D rb;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio_monster = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterEat"))
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            rb.MovePosition(rb.position + direction * Time.fixedDeltaTime * chaseSpeed);
            rb.SetRotation(Angle);
        }
        

    }

    public void RespawnAfterDeath()
    {
        audio_monster.Play();
        gm.Respawn();
    }

    public void DeathSFX()
    {
        audio_death.Play();
        audio_monster.Stop();
        gm.SetCameraTarget(gameObject);
        gm.SetCameraSize(3, 0.2f);

    }


}
