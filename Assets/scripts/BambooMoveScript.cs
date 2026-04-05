using UnityEngine;

public class BambooMoveScript : MonoBehaviour
{   
    public float moveSpeed = 5;
    public float leftBound = -70;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * (moveSpeed * Time.deltaTime);
        if (transform.position.x < leftBound)
        {
            Debug.Log("Bamboo deleted");
            Destroy(gameObject);
        }
    }
}
