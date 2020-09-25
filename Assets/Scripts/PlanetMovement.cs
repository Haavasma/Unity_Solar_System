using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    [SerializeField] float radius; 
    [SerializeField] float torque;
    [SerializeField] float alpha;
    [SerializeField] float orbitSpeed;
    [SerializeField] bool moon;
    
    [SerializeField] GameObject centerObject;
    
    private Vector3 center;
    Rigidbody rb;

    private bool inOrbit = true;

    // Start is called before the first frame update
    void Start()
    {
        center = centerObject.transform.position;
        rb = GetComponent<Rigidbody> ();
        transform.position=new Vector3(center.x + radius*Mathf.Cos(alpha), center.y, center.z + radius * Mathf.Sin(alpha));
        alpha+=orbitSpeed*0.01f;
        rb.AddForce(new Vector3(center.x + radius*Mathf.Cos(alpha) - transform.position.x, center.y - transform.position.y, center.z + radius * Mathf.Sin(alpha) - transform.position.z).normalized*400f*orbitSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        center = centerObject.transform.position;
    }

    void FixedUpdate() 
    {
            Vector3 sunPlanetVector = transform.position - center;
            if(inOrbit){
                if(moon){
                    alpha+=orbitSpeed*0.01f;
                    rb.MovePosition(new Vector3(center.x + radius*Mathf.Cos(alpha), center.y, center.z + radius * Mathf.Sin(alpha)));
                }else if(sunPlanetVector.magnitude > radius) {
                    rb.AddForce((-sunPlanetVector.normalized) * 10f * orbitSpeed);
                }
                transform.Rotate(0.0f, torque, 0.0f, Space.Self);
            } else {
                rb.AddForce((-sunPlanetVector.normalized) * 100f 
                            * ((centerObject.transform.localScale.x) * (transform.localScale.x))/(sunPlanetVector.magnitude * sunPlanetVector.magnitude));
            }
    }

    void OnCollisionEnter(Collision collision)
    {
        inOrbit = false;
    }
}
