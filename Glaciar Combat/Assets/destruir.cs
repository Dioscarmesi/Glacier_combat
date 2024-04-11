using UnityEngine;

public class Bala : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("lluvia"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
