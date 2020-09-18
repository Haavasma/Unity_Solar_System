using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    [SerializeField] float radius; 

    [SerializeField] float tilt;

    [SerializeField] float torque;
    [SerializeField] float alpha;
    [SerializeField] float orbitSpeed;
    [SerializeField] Vector3 center;
    Rigidbody rb;

    private bool inOrbit = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody> ();
        transform.position=new Vector3(center.x + radius*Mathf.Cos(alpha), center.y, center.z + radius * Mathf.Sin(alpha));
        alpha+=orbitSpeed*0.01f;
        rb.AddForce(new Vector3(center.x + radius*Mathf.Cos(alpha) - transform.position.x, center.y - transform.position.y, center.z + radius * Mathf.Sin(alpha) - transform.position.z).normalized*400f*orbitSpeed);
        rb.AddTorque(this.transform.up * torque*100.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() 
    {
        Vector3 sunPlanetVector = transform.position - transform.parent.gameObject.transform.position;
        if(inOrbit){
            if(sunPlanetVector.magnitude > radius) {
                rb.AddForce((-sunPlanetVector.normalized) * 10f * orbitSpeed);
            }
        } else {
            rb.AddForce((-sunPlanetVector.normalized) * 700f 
                        * ((transform.parent.gameObject.transform.localScale.x) * (transform.localScale.x))/(sunPlanetVector.magnitude * sunPlanetVector.magnitude));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        inOrbit = false;
    }
}
