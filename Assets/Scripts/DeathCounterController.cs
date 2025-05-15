using TMPro;
using UnityEngine;

public class DeathCounterController : MonoBehaviour
{
    void Start()
    {
        // Obtener y mostrar el contador de muertes cuando se muere el jugador
        GetComponent<TextMeshProUGUI>().text = GameManager.Instance.GetDeathCounter().ToString();
    }
}
