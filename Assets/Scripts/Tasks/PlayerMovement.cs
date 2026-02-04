using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, y, 0).normalized;
        transform.position += move * speed * Time.deltaTime;
    }
}


