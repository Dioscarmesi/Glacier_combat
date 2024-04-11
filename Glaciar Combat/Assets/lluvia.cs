
using UnityEngine;

public class ObjetoCaido : MonoBehaviour
{
    public GameObject objetoPrefab;
    public Transform[] posicionesIniciales;
    public float alturaCaida = 10f;
    public float tiempoEsperaInicialMin = 3f;
    public float tiempoEsperaInicialMax = 5f;
    public float velocidadMinima = 5f;
    public float velocidadMaxima = 10f;

    private void Start()
    {
        for (int i = 0; i < posicionesIniciales.Length; i++)
        {
            StartCoroutine(GenerarObjetos(posicionesIniciales[i]));
        }
    }

    private System.Collections.IEnumerator GenerarObjetos(Transform posicionInicial)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(tiempoEsperaInicialMin, tiempoEsperaInicialMax));

            GameObject nuevoObjeto = Instantiate(objetoPrefab, posicionInicial.position, Quaternion.identity);
            Rigidbody rb = nuevoObjeto.GetComponent<Rigidbody>();

            rb.useGravity = false;

            yield return new WaitForSeconds(Random.Range(0.5f, 2f));

            // Verificar si el objeto aún existe antes de intentar acceder a su Rigidbody
            if (nuevoObjeto != null && rb != null)
            {
                float velocidadAleatoria = Random.Range(velocidadMinima, velocidadMaxima);
                rb.velocity = Vector3.down * velocidadAleatoria;

                rb.useGravity = true;

                // Espera hasta que el objeto toque el suelo
                yield return new WaitUntil(() => HaTocadoElSuelo(nuevoObjeto));

                // Verificar si el objeto aún existe antes de intentar destruirlo
                if (nuevoObjeto != null)
                {
                    // Si ha tocado el suelo, espera un breve tiempo antes de destruir el objeto
                    yield return new WaitForSeconds(1f);
                    Destroy(nuevoObjeto);
                }
            }
        }
    }

    private bool HaTocadoElSuelo(GameObject objeto)
    {
        // Verifica si el objeto ha tocado el suelo (objeto con etiqueta "ground")
        return !objeto.GetComponent<Collider>().isTrigger && objeto.transform.position.y < alturaCaida;
    }
}
