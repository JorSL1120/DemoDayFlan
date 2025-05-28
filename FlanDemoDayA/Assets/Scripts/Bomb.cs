using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float stunDuration = 2f;
    public float knockbackForce = 8f;
    public GameObject explosionEffect;

    void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        if (explosionEffect)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hitColliders)
        {
            EnemyStun enemy = hit.GetComponent<EnemyStun>();
            if (enemy != null)
            {
                enemy.Stun(stunDuration, transform.position, knockbackForce);
            }
        }

        Destroy(gameObject);
    }
}
