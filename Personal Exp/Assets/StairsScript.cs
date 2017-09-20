using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsScript : MonoBehaviour {

    private Renderer render;
    public Camera cam;
    private float range = 11;
    public bool doorInSight = false;
    public bool trigger2 = false;
    public bool wallMove = false;
    public bool allowWallMove = true;
    public bool lightDim = false;
    public GameObject startingStairs;
    public GameObject newStairs;
    public GameObject rightWall;
    public GameObject leftWall;
    public GameObject barrier;
    public Light pointLight;

    void Start () {
        render = gameObject.GetComponent<Renderer>();
        newStairs.SetActive(false);
        barrier.SetActive(false);
    }
	
	void Update () {
        RayCastCheck();
        triggerChange();
        moveWalls();
        pointLight.range = range;
        if (lightDim == true)
        {
            range -= (0.5f * Time.deltaTime);
            Debug.Log(range);
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "trigger2")
        {
            trigger2 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "trigger2")
        {
            trigger2 = false;
        }
    }

    void moveWalls()
    {
        if (wallMove == true)
        {
            rightWall.transform.Rotate(new Vector3(0, 0.5f, 0) * Time.deltaTime, Space.World);
            leftWall.transform.Rotate(new Vector3(0, -0.5f, 0) * Time.deltaTime, Space.World);
            if (leftWall.transform.rotation.eulerAngles.y <= 350)
            {
                allowWallMove = false;
                wallMove = false;
            }
        }
    }

    void triggerChange()
    {
        if (doorInSight == true && trigger2 == true)
        {
            startingStairs.SetActive(false);
            newStairs.SetActive(true);
        }
    }

    void dimLight()
    {
        lightDim = true;
    }

    private void RayCastCheck()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Collider objectHit = hit.collider;
            if(objectHit.tag == "trigger")
            {
                Debug.Log("It worked?");
                doorInSight = true;
            }
            if (objectHit.tag == "wallTrigger")
            {
                if (allowWallMove == true)
                {
                    dimLight();
                    barrier.SetActive(true);
                    wallMove = true;
                }
            }
        }
    }
}
