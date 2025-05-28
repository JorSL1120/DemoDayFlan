using UnityEngine;
using System.Collections;

public class EnemyStun : MonoBehaviour
{
    private bool isStunned = false;
    private Rigidbody rb;
    private UnityEngine.AI.NavMeshAgent agent; // Si usas NavMesh para mover enemigos

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void Stun(float duration, Vector3 explosionOrigin, float knockbackForce)
    {
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(duration));

            // Aplica fuerza de empuje desde la bomba
            if (rb != null)
            {
                Vector3 direction = (transform.position - explosionOrigin).normalized;
                rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
            }
        }
    }

    IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;

        if (agent != null)
            agent.enabled = false; // Desactiva movimiento por IA

        Debug.Log($"{gameObject.name} aturdido y empujado");

        yield return new WaitForSeconds(duration);

        if (agent != null)
            agent.enabled = true;

        isStunned = false;
        Debug.Log($"{gameObject.name} ya no está aturdido");
    }
}
