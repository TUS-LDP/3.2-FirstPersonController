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
        Vector3 newRotation;

        newRotation = Vector3.Lerp(arm.transform.eulerAngles, new Vector3(-90, 0, 0), raiseSpeed * Time.deltaTime);

        arm.transform.eulerAngles = newRotation;
    }

    private void LowerArm()
    {
        Vector3 newRotation;

        newRotation = Vector3.Lerp(arm.transform.eulerAngles, Vector3.zero, raiseSpeed * Time.deltaTime);

        arm.transform.eulerAngles = newRotation;
    }
}
