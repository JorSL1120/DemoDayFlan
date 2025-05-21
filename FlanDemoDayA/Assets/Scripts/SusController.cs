using UnityEngine;

public class SusController : MonoBehaviour
{
    public float speed, rotSpeed;
    public Transform enemy;

    void Update()
    {
        Movement();
        Vector3 normEnemyPos = (enemy.position - transform.position).normalized;

        //float distToEnemy = Vector3.Magnitude(enemy.position - transform.position);
        float distToEnemy = Vector3.Distance(enemy.position, transform.position);

        float dotProduct = Vector3.Dot(normEnemyPos, transform.forward);
        float fovAngle = Camera.main.fieldOfView;
        float cosAngle = Mathf.Cos(Mathf.Deg2Rad * fovAngle/2f);

        /*if (cosAngle < dotProduct && distToEnemy < 5)
            SusFollowing.canMove = false;
        else
            SusFollowing.canMove = true;*/
    }

    void Movement()
    {
        float dt = Time.deltaTime;
        float rotAngle = rotSpeed * dt * HorizontalInput();
        transform.Rotate(0, rotAngle, 0, Space.Self);

        Vector3 translation = speed * dt * transform.forward * VerticalInput();
        transform.Translate(translation, Space.World);
    }

    float HorizontalInput()
    {
        if (Input.GetKey(KeyCode.D))
            return 1f;
        else if (Input.GetKey(KeyCode.A))
            return -1f;
        else
            return 0f;
    }

    float VerticalInput()
    {
        if (Input.GetKey(KeyCode.W))
            return 1f;
        else if (Input.GetKey(KeyCode.S))
            return -1f;
        else
            return 0f;
    }
}
