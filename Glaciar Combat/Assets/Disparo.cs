using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
     public GameObject bulletPrefab; // Prefabricado de la bala
    public Transform bulletSpawnPoint; // Punto de inicio de la bala
    public float shootCooldown = 0.25f; // Tiempo de espera entre disparos
    private float lastShootTime; // Tiempo del �ltimo disparo

    void Update()
    {
        // Verifica si ha pasado suficiente tiempo desde el �ltimo disparo
        if (Time.time - lastShootTime >= shootCooldown)
        {
            // Si se presiona el bot�n de disparo ( barra espaciadora)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot(); // Llama a la funci�n de disparo
                lastShootTime = Time.time; // Actualiza el tiempo del �ltimo disparo
            }
        }
    }

    void Shoot()
    {
        // Instancia una nueva bala en el punto de inicio con una rotaci�n hacia arriba
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 10; // Velocidad hacia arriba
        Destroy(bullet, 5f); // Destruye la bala despu�s de 5 segundos
    }
}