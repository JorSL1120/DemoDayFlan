using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Tooltip("Transform del jugador")]
    public Transform player;

    [Tooltip("Velocidad de movimiento")]
    public float speed = 5f;

    [Tooltip("Distancia máxima para perseguir al jugador")]
    public float detectionRange = 10f;

    private bool canMove = true;

    void Update()
    {
        if (player == null || !canMove)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange)
            return; // No persigue si está fuera del rango

        // Obtener la posición del jugador a nivel del fantasma (misma Y)
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

        // Rotar hacia el jugador (horizontalmente)
        transform.LookAt(targetPosition);

        // Calcular dirección sin afectar Y
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Mover solo en plano XZ
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
