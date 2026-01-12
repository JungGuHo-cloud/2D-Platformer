using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 7;
    public float jumpSpeed = 15;
    public Collider2D bottomCollider;
    public CompositeCollider2D terrainCollider;

    float vx = 0;
    float prevVx = 0;
    bool isGrounded;
    Vector2 originPosition;
    float lastShoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originPosition = transform.position;
    }

    public void Restart()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<BoxCollider2D>().enabled = true;

        transform.eulerAngles = Vector3.zero;
        transform.position = originPosition;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {
        // 가속도가 필요한 경우 GetAxis사용
        vx = Input.GetAxisRaw("Horizontal");

        if (vx < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (vx > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (bottomCollider.IsTouching(terrainCollider))
        {
            if (!isGrounded)
            {
                // 착지
                if (vx == 0)
                {
                    GetComponent<Animator>().SetTrigger("Idle");
                }
                else
                {
                    GetComponent<Animator>().SetTrigger("Run");
                }
            }
            else
            {
                // 걷는 중
                if (prevVx != vx)
                {
                    if (vx == 0)
                    {
                        GetComponent<Animator>().SetTrigger("Idle");
                    }
                    else
                    {
                        GetComponent<Animator>().SetTrigger("Run");
                    }
                }
            }
            
            isGrounded = true;
        }
        else
        {
            if (isGrounded) 
            {
                GetComponent<Animator>().SetTrigger("Jump");
            }

            isGrounded = false;
        }



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * speed;
        }

        prevVx = vx;

        // 0.5초마다 총알 발사
        if(Input.GetButtonDown("Fire1") && lastShoot + 0.5f < Time.time)
        {
            Vector2 bulletVt = Vector2.zero;

            if(GetComponent<SpriteRenderer>().flipX)
            {
                bulletVt = new Vector2(-10, 0);
            }
            else
            {
                bulletVt = new Vector2(10, 0);
            }

            GameObject bullet = ObjectPool.Instance.GetBullet();
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().velocity = bulletVt;
            lastShoot = Time.time;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * vx * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    void Die() 
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().angularVelocity = 720;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        GetComponent<BoxCollider2D>().enabled = false;

        GameManager.Instance.Die();
    }
}
