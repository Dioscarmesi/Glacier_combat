using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SampleScene"); // Cargar la escena de juego
    }

    public void Options()
    {
        SceneManager.LoadScene("Options"); // Cargar la escena de opciones
    }

    public void Quit()
    {
        UnityEngine.Application.Quit(); // Salir de la aplicación
    }
}
