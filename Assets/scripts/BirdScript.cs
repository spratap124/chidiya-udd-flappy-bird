using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;

    [Header("Sprite animation")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] flapSprites;
    [SerializeField] float flapFramesPerSecond = 10f;

    [Header("Game over")]
    [SerializeField] float gameOverNoseDownAngle = -75f;
    [SerializeField] float gameOverTiltSpeed = 220f;

    float _flapTimer;
    int _flapIndex;
    bool _flapInputEnabled = true;
    bool _gameOverFall;

    public void LockFlapInput() => _flapInputEnabled = false;

    public void BeginGameOverFall()
    {
        LockFlapInput();
        _gameOverFall = true;
    }

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        gameObject.name = "Bird";

        if (spriteRenderer != null && flapSprites != null && flapSprites.Length > 0)
            spriteRenderer.sprite = flapSprites[0];
    }

    void Update()
    {
        bool jump = false;

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            jump = true;

        if (!jump && Touchscreen.current != null)
        {
            foreach (var touch in Touchscreen.current.touches)
            {
                if (touch.press.wasPressedThisFrame)
                {
                    jump = true;
                    break;
                }
            }
        }

        if (_flapInputEnabled && jump)
            myRigidbody.linearVelocity = Vector2.up * flapStrength;

        AdvanceFlapAnimation();
    }

    void FixedUpdate()
    {
        if (!_gameOverFall || myRigidbody == null)
            return;

        float next = Mathf.MoveTowardsAngle(
            myRigidbody.rotation,
            gameOverNoseDownAngle,
            gameOverTiltSpeed * Time.fixedDeltaTime);
        myRigidbody.MoveRotation(next);
    }

    void AdvanceFlapAnimation()
    {
        if (spriteRenderer == null || flapSprites == null || flapSprites.Length == 0)
            return;

        float interval = 1f / Mathf.Max(0.01f, flapFramesPerSecond);
        _flapTimer += Time.deltaTime;
        if (_flapTimer >= interval)
        {
            _flapTimer -= interval;
            _flapIndex = (_flapIndex + 1) % flapSprites.Length;
        }

        spriteRenderer.sprite = flapSprites[_flapIndex];
    }
}
