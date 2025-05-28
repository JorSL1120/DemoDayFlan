using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [Tooltip("Asigna aquí todos los enemigos manualmente")]
    public List<EnemyAI> enemies = new List<EnemyAI>();

    [Tooltip("Distancia máxima para detectar enemigos")]
    public float detectionDistance = 10f;

    [Tooltip("Ángulo del campo de visión en grados")]
    public float fieldOfView = 60f;

    void Update()
    {
        foreach (EnemyAI enemy in enemies)
        {
            if (enemy == null) continue;

            //Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            //float dot = Vector3.Dot(transform.forward, directionToEnemy);

            Vector3 flatDirectionToEnemy = (enemy.transform.position - transform.position);
            flatDirectionToEnemy.y = 0;
            flatDirectionToEnemy.Normalize();
            float dot = Vector3.Dot(transform.forward, flatDirectionToEnemy);
            float angleLimit = Mathf.Cos(Mathf.Deg2Rad * fieldOfView / 2f);

            if (dot >= angleLimit && distance <= detectionDistance)
            {
                // El enemigo está en el campo de visión
                enemy.SetCanMove(false);
            }
            else
            {
                // Fuera del campo o demasiado lejos
                enemy.SetCanMove(true);
            }
        }
    }

    // Gizmo para ver el campo de visión
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);

        Vector3 left = Quaternion.Euler(0, -fieldOfView / 2f, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, fieldOfView / 2f, 0) * transform.forward;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, left * detectionDistance);
        Gizmos.DrawRay(transform.position, right * detectionDistance);
    }
}
