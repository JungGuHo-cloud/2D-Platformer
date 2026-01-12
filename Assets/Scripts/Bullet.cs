using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(10, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Terrain")
        {
            gameObject.SetActive(false);
        }
        else if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hit(1);
            gameObject.SetActive(false);
        }
    }
}
