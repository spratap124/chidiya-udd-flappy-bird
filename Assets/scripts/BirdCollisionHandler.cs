using System;
using UnityEngine;

public class BirdCollisionHandler : MonoBehaviour
{
    [SerializeField] string obstacleTag = "Obstacle";
    [SerializeField] Camera gameplayCamera;
    [Tooltip("How far below the camera bottom edge the bird must be before the game freezes.")]
    [SerializeField] float belowScreenPadding = 0.30f;

    public static event Action OnGameOver;

    bool _ended;
    bool _frozen;

    Camera Cam => gameplayCamera != null ? gameplayCamera : Camera.main;

    void Update()
    {
        if (_frozen || Cam == null || !Cam.orthographic)
            return;

        float cameraBottom = Cam.transform.position.y - Cam.orthographicSize;
        float offScreenLine = cameraBottom - belowScreenPadding;

        if (!IsBirdFullyBelow(offScreenLine))
            return;

        if (!_ended)
        {
            _ended = true;
            GetComponent<BirdScript>()?.BeginGameOverFall();
            Debug.Log("Game Over");
            OnGameOver?.Invoke();
        }

        _frozen = true;
        Time.timeScale = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_ended || !collision.gameObject.CompareTag(obstacleTag))
            return;

        _ended = true;
        GetComponent<BirdScript>()?.BeginGameOverFall();

        Debug.Log("Game Over");
        OnGameOver?.Invoke();
    }

    bool IsBirdFullyBelow(float worldY)
    {
        var col = GetComponent<Collider2D>();
        if (col != null)
            return col.bounds.max.y < worldY;
        return transform.position.y < worldY;
    }
}
