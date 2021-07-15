using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LightingManager))]

public class CarController : MonoBehaviour
{
    public InputManager im;
    public LightingManager lm;
    public UIManager uim;
    public List<WheelCollider> throttleWheels;
    public List<GameObject> steeringWheels;
    public List<GameObject> meshes;
    public List<GameObject> tailLights;
    public float strengthCoefficient = 10000f;
    public float maxTurn = 20f;
    public Transform centralMass; //CentralMass
    public Rigidbody rb;
    public float breakeStrength;

    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();

        if (centralMass)
        {
            rb.centerOfMass = centralMass.position;
        }
    }
    void Update()
    {
        if (im.l)
        {
            lm.ToggleHeadlinghts();
        }

        foreach (GameObject tl in tailLights)
        {
            tl.GetComponent<Renderer>().material.SetColor("_EmissionColor",im.brake ? new Color(0.5f, 0.111f, 0.111f) : Color.black);
        }

        uim.changeText(transform.InverseTransformVector(rb.velocity).z);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (WheelCollider wheel in throttleWheels)
        {            
            if (im.brake)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = breakeStrength * Time.deltaTime;
            }
            else
            {
                wheel.motorTorque = strengthCoefficient * Time.deltaTime * im.throttle;
                wheel.brakeTorque = 0f;
            }
                         
        }
        foreach (GameObject wheel in steeringWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;
            wheel.transform.localEulerAngles = new Vector3(0f, im.steer * maxTurn, 0f);
        }
        foreach(GameObject mesh in meshes)
        {
            mesh.transform.Rotate(rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0?1:-1)/(2*Mathf.PI*0.3f), 0f, 0f);//ternary operator - 1>0?
        }

    }
}
