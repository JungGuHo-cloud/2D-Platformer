using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;
    public float speed = 3;
    public Collider2D frontBottomCollider;
    public Collider2D frontCollider;
    public CompositeCollider2D terrainCollider;

    Vector2 vx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vx = Vector2.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (frontCollider.IsTouching(terrainCollider) || !frontBottomCollider.IsTouching(terrainCollider))
        {
            vx = -vx;
            transform.localScale = new Vector2(-transform.localScale.x, 1);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(vx * Time.fixedDeltaTime);
    }

    public void Hit(int damage) 
    {
        hp -= damage;

        if( hp <= 0)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().angularVelocity = 720;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);

            Invoke(nameof(DestroyThis), 2.0f);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
