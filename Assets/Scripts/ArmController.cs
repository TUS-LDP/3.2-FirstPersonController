using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ArmController : MonoBehaviour
{
    private StarterAssetsInputs _inputs;

    [SerializeField]
    private float raiseSpeed;

    [SerializeField]
    private GameObject arm;

    [SerializeField]
    float targetRaisedAngle;

    [SerializeField]
    float targetLoweredAngle;

    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckArm();
    }

    private void CheckArm()
    {
        if (_inputs.raiseArm)
        {
            RaiseArm();
        }
        else
        {
            LowerArm();
        }
    }

    private void RaiseArm()
    {
        arm.transform.localRotation = Quaternion.Lerp(arm.transform.localRotation, Quaternion.Euler(targetRaisedAngle, 0, 0), raiseSpeed * Time.deltaTime);
    }

    private void LowerArm()
    {
        arm.transform.localRotation = Quaternion.Lerp(arm.transform.localRotation, Quaternion.Euler(targetLoweredAngle, 0, 0), raiseSpeed * Time.deltaTime);
    }
}
