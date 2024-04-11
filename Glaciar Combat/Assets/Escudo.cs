using UnityEngine;

public class Escudo : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vida player = other.GetComponent<Vida>();
            if (player != null)
            {
                // Aplicar efecto de escudo al jugador
                player.ActivateShield();
            }

            // Destruir el objeto de escudo
            Destroy(gameObject);
        }
    }
}