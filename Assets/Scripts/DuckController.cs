using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class DuckController : MonoBehaviour
{
    public float yDuckScale;
    public float duckSpeed;

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
        else if (!_input.duck && (transform.localScale.y != _normalYScale))  // if not ducking and not yet at the normal scale
        {
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
}
