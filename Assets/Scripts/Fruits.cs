using UnityEngine;

public class Fruits : MonoBehaviour
{
    public float timeAdd = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.Instance.AddTime(timeAdd);
            GetComponent<Animator>().SetTrigger("Eaten");
            //Invoke("DestroyThis", 0.3f);
            Invoke(nameof(DestroyThis), 0.3f);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
