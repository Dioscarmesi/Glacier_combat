using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int lives = 3;
    private bool hasShield = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("lluvia"))
        {
            if (!hasShield) // Solo resta vidas si el jugador no tiene el escudo
            {
                lives--;
                if (lives <= 0)
                {
                    // Lógica para manejar la muerte del jugador
                    Destroy(gameObject);
                }
            }

            Destroy(collision.gameObject);
           
        }
    }

    public void ActivateShield()
    {
        hasShield = true;
    }

    public void DeactivateShield()
    {
        hasShield = false;
    }
}

