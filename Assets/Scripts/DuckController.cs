using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class DuckController : MonoBehaviour
{
    // The Y scale to use when the character is ducking (how "short" the character becomes)
    public float yDuckScale;
    // How quickly the character transitions between standing and ducking
    public float duckSpeed;
    // How far above the character to check for obstacles
    public float overheadOffset;
    // The radius of the sphere used to check for overhead obstacles
    public float overheadRadius;

    [Tooltip("What layers are overhead objects on")]
    public LayerMask overheadLayers;

    // True if there is an obstacle above the character 
    [SerializeField]
    private bool obstacleOverhead;

    // The original Y scale of the character (standing height)
    private float _normalYScale;

    // Reference to the input handler that stores player input states (like duck, jump, etc.)
    private StarterAssetsInputs _input;

    // Start is called before the first frame update
    void Start()
    {
        // Get the StarterAssetsInputs component attached to this GameObject
        _input = GetComponent<StarterAssetsInputs>();

        // If Y Duck Scale has not been set in the editor, set it to 0.5 here
        if (yDuckScale == 0.0f)
        {
            yDuckScale = 0.5f;
        }

        // If Duck Speed has not been set in the editor, set it to 5.0 here
        if (duckSpeed == 0.0f)
        {
            duckSpeed = 5.0f;
        }

        // Store the original Y scale of the character
        _normalYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there is an obstacle above the character
        OverheadCheck();
        // Handle ducking and standing logic
        Duck();
    }

    // Handles the logic for ducking and standing up
    private void Duck()
    {
        float newYScale;

        // Only change the Y scale if needed:
        // - If ducking and not already at duck scale, lerp towards duck scale
        // - If not ducking, not at normal scale, and nothing overhead, lerp towards normal scale

        if  (_input.duck && (transform.localScale.y != yDuckScale)) // If ducking and not yet at the duck scale
        {
            // Smoothly move the Y scale towards the duck scale
            newYScale = Mathf.Lerp(transform.localScale.y, yDuckScale, duckSpeed * Time.deltaTime);

            // If almost at the duck scale, snap to the exact value
            if ((newYScale - yDuckScale) < 0.05f)
            {
                newYScale = yDuckScale;
            }

            // Update the character's scale (only Y changes)
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.x);
        }
        else if (!_input.duck && (transform.localScale.y != _normalYScale) && !obstacleOverhead)  // If not ducking, not at normal scale, and no obstacles overhead
        {
            // Smoothly move the Y scale back to the normal (standing) scale
            newYScale = Mathf.Lerp(transform.localScale.y, _normalYScale, duckSpeed * Time.deltaTime);

            // If almost at the normal scale, snap to the exact value
            if ((_normalYScale - newYScale) < 0.05f)
            {
                newYScale = _normalYScale;
            }

            // Update the character's scale (only Y changes)
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.x);
        }
        
    }

    // Checks if there is an obstacle above the character using a physics sphere
    private void OverheadCheck()
    {
        // Set the position of the sphere above the character
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + overheadOffset, transform.position.z);
        // Check if the sphere overlaps with any objects on the specified layers
        obstacleOverhead = Physics.CheckSphere(spherePosition, overheadRadius, overheadLayers, QueryTriggerInteraction.Ignore);
    }

    // Draws a colored sphere in the editor to show where the overhead check happens
    private void OnDrawGizmosSelected()
    {
        // Green if something is overhead, red if not
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (obstacleOverhead) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // Draw the sphere at the overhead check position with the correct radius
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + overheadOffset, transform.position.z), overheadRadius);
    }
}
