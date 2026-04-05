using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.name = "Chidiya";
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Check for jump input
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

        // Apply jump force if input is detected
        if (jump)
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
    }
}
 