using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform playerCameraParent;
    public float lookSpeed = 2.0f;

    public float lookXLimit = 60.0f;

    public float speed = 5.0f;

    public float zoomSpeed = 2.0f;
    private GameObject planets;

    private Transform camera;
    private Transform selectedPlanet;

    private int selectIndex=0;

    private int childCount;

    private float scrollValue;

    Vector2 rotation = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        camera = playerCameraParent.GetChild(0).transform;
        planets = GameObject.FindWithTag("Planets");
        childCount = planets.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {

            if(Input.GetKeyDown(KeyCode.Q)){
                selectIndex += childCount  - 1;
                selectIndex %= childCount;
            } else if(Input.GetKeyDown(KeyCode.E)){
                selectIndex+=1;
                selectIndex%=childCount;
            }
            scrollValue += Input.mouseScrollDelta.y;

            planets = GameObject.FindWithTag("Planets");
            selectedPlanet = planets.transform.GetChild(selectIndex).gameObject.transform;
            float step =  100f * Time.deltaTime;
            Debug.Log(selectedPlanet.position);
            playerCameraParent.position = Vector3.MoveTowards(playerCameraParent.position, selectedPlanet.position, step*speed);
            camera.localPosition = Vector3.MoveTowards(camera.localPosition, new Vector3(0.0f, 0.0f, selectedPlanet.localScale.x*(3.0f - (scrollValue*0.5f))), 
                                                            step*zoomSpeed);
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            //playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            playerCameraParent.eulerAngles = new Vector2(-rotation.x, rotation.y);
    }
}
