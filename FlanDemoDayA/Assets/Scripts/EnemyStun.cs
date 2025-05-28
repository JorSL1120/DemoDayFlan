using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyStun : MonoBehaviour
{
    [Header("Configuración de Stun")]
    public float customStunDuration = 3f; // Puedes ajustar este valor desde el Inspector

    [Header("Knockback")]
    public float knockbackForce = 5f; // Opcional, puedes configurar knockback por enemigo

    private bool isStunned = false;
    private Rigidbody rb;
    private NavMeshAgent agent;

    // Permite que otros scripts (como EnemyAI) vean si está stuneado
    public bool IsStunned => isStunned;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Este método es llamado por la bomba al explotar
    public void Stun(Vector3 explosionOrigin, float knockback)
    {
        if (isStunned) return;

        // Aplicar fuerza de empuje (knockback)
        if (rb != null)
        {
            Vector3 direction = (transform.position - explosionOrigin).normalized;
            rb.AddForce(direction * knockback, ForceMode.Impulse);
        }

        StartCoroutine(StunCoroutine(customStunDuration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;

        // Detener movimiento de IA
        if (agent != null)
            agent.enabled = false;

        // Esperar el tiempo de stun
        yield return new WaitForSeconds(duration);

        // Restaurar movimiento
        if (agent != null)
            agent.enabled = true;

        isStunned = false;
    }
}
