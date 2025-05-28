using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    public string nombreEscena;

    public void Reiniciar()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
