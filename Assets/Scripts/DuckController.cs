using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class DuckController : MonoBehaviour
{
    public float yDuckScale;
    public float duckSpeed;
    public float overheadOffset;
    public float overheadRadius;

    [Tooltip("What layers are overhead objects on")]
    public LayerMask overheadLayers;

    [SerializeField]
    private bool obstacleOverhead;

    private float _normalYScale;

    private StarterAssetsInputs _input;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();

        // if Y Duck Scale has not been set in the editor then set it to 0.5 here
        if (yDuckScale == 0.0f)
        {
            yDuckScale = 0.5f;
        }

        if (duckSpeed == 0.0f)
        {
            duckSpeed = 5.0f;
        }

        _normalYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        OverheadCheck();
        Duck();
    }

    private void Duck()
    {
        float newYScale;

        // The following if statement ensures we only set the local y scale if we have to i.e. if we are ducking and 
        // already at the y duck scale then we do nothing, likewise, if we are not ducking and we are at the normal
        // scale we do nothing

        if  (_input.duck && (transform.localScale.y != yDuckScale)) // if ducking and not yet at the duck scale
        {
            // Lerp towards the yDuckScale
            newYScale = Mathf.Lerp(transform.localScale.y, yDuckScale, duckSpeed * Time.deltaTime);

            // if we are *almost* at the yDuckScale then set the new scale to the yDuckScale
            if ((newYScale - yDuckScale) < 0.05f)
            {
                newYScale = yDuckScale;
            }

            // Update the scale
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.x);
        }
        else if (!_input.duck && (transform.localScale.y != _normalYScale) && !obstacleOverhead)  // if not ducking and not yet at the normal scale and no obstacles overhead
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //bool somethingOverhead = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

            // Lerp towards the _normalScale
            newYScale = Mathf.Lerp(transform.localScale.y, _normalYScale, duckSpeed * Time.deltaTime);

            // if we are *almost* at the _normalScale then set the new scale to the _normalScale
            if ((_normalYScale - newYScale) < 0.05f)
            {
                newYScale = _normalYScale;
            }

            // Update the scale
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.x);
        }
        
    }

    private void OverheadCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + overheadOffset, transform.position.z);
        obstacleOverhead = Physics.CheckSphere(spherePosition, overheadRadius, overheadLayers, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (obstacleOverhead) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + overheadOffset, transform.position.z), overheadRadius);
    }
}
