using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private string playerName;

    // Llamado cuando se completa una partida
    public void OnGameFinished()
    {
        // Obtener el nombre de usuario (asumimos que ya lo tienes almacenado)
        playerName = "Juan"; // Ejemplo

        // Calcular la puntuación (aquí puedes usar tu propia lógica)
        score = 100; // Ejemplo

        // Mostrar la puntuación en pantalla
        scoreText.text = $"Score: {score}";

        // Enviar la puntuación al servidor
        StartCoroutine(SendScoreToServer());
    }

    private IEnumerator SendScoreToServer()
    {
        string serverUrl = "http://localhost/prueba/index.php"; // Cambia por la URL de tu servidor
        string urlWithParams = $"{serverUrl}?username={playerName}&score={score}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlWithParams))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Puntuación enviada al servidor correctamente");
            }
            else
            {
                Debug.LogError($"Error al enviar la puntuación: {webRequest.error}");
            }
        }
    }
}
