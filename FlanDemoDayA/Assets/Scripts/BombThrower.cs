using UnityEngine;
using System.Collections;

public class BombThrower : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float upwardForce = 5f;
    public float cooldown = 5f;

    private bool canThrow = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canThrow)
        {
            ThrowBomb();
        }
    }

    void ThrowBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();

        // Lanzamiento parabólico
        Vector3 direction = (Camera.main.transform.forward + Vector3.up * 0.5f).normalized;
        rb.AddForce(direction * throwForce + Vector3.up * upwardForce, ForceMode.Impulse);

        canThrow = false;
        Invoke(nameof(ResetThrow), cooldown);
    }

    void ResetThrow()
    {
        canThrow = true;
    }
}
