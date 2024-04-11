using UnityEngine;

public class GeneradorItems : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 10f;

    private void Start()
    {
        // Comienza a generar items en intervalos regulares
        InvokeRepeating("GenerarItem", spawnInterval, spawnInterval);
    }

    private void GenerarItem()
    {
        // Selecciona aleatoriamente uno de los puntos de generación
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Genera el item en el punto de generación seleccionado
        Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
    }
}