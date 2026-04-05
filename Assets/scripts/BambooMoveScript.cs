using UnityEngine;

public class BambooMoveScript : MonoBehaviour
{
    static float _sharedMoveSpeed = 5f;

    public static float SharedMoveSpeed => _sharedMoveSpeed;

    public static void ResetSharedMoveSpeed(float value) => _sharedMoveSpeed = value;

    public static void AddSharedMoveSpeed(float delta) => _sharedMoveSpeed += delta;

    public float leftBound = -70;

    void Update()
    {
        transform.position += Vector3.left * (_sharedMoveSpeed * Time.deltaTime);
        if (transform.position.x < leftBound)
        {
            Debug.Log("Bamboo deleted");
            Destroy(gameObject);
        }
    }
}
