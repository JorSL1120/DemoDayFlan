using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Tooltip("Transform del jugador")]
    public Transform player;

    [Tooltip("Velocidad de movimiento")]
    public float speed = 5f;

    [Tooltip("Distancia m�xima para perseguir al jugador")]
    //public float detectionRange = 10f;

    private bool canMove = true;
    private EnemyStun stun;  // Sistema del Stun

    void Awake()
    {
        //Obtener el componente de stun si existe
        stun = GetComponent<EnemyStun>();
    }

    void Update()
    {
        Debug.Log("CanMove: " + canMove);

        if (player == null || !canMove)
            return;

        //Si el fantasma est� stuneado, no se mueve
        if (stun != null && stun.IsStunned)
            return;

        /*float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange)
            return; // No persigue si est� fuera del rango
        else if (distanceToPlayer <= detectionRange && !canMove)
            return;*/

        // Obtener la posici�n del jugador a nivel del fantasma (misma Y)
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

        // Rotar hacia el jugador (horizontalmente)
        transform.LookAt(targetPosition);

        // Calcular direcci�n sin afectar Y
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Mover solo en plano XZ
        transform.position += direction * (speed * Time.deltaTime);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public bool GetCanMove()
    {
        return canMove;
    }
}
