using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] private int speed;
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }
} 

