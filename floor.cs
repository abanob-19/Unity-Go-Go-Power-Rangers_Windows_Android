using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class floor : MonoBehaviour
{
    public MeshRenderer plane;
    public GameObject player;
    public GameObject redOrb;
    public GameObject greenOrb;
    public GameObject blueOrb;
    public GameObject obstacle;
    public Camera mainCamera;
    public Light mainLight;



    // Start is called before the first frame update

    void Start()
    {
        generateItems(player.transform.position.z - 80, plane.bounds.min.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= plane.transform.position.z)
        {
            plane = GetComponent<MeshRenderer>();
            if (plane != null)
            {
                float lastPoint = plane.bounds.min.z;
                float firstPoint = plane.bounds.max.z;
                float zSize = firstPoint - lastPoint;
                if (player.transform.position.z - lastPoint <= zSize / 2)
                {
                    float newZ = transform.position.z - (zSize / 2)+15;
                    //Debug.Log(transform.position.z + " " + newZ);
                    transform.position = new Vector3(transform.position.x, transform.position.y,newZ);
                    // Debug.Log(lastPoint+" "+ plane.bounds.min.z);
                    generateItems(lastPoint-80, plane.bounds.min.z);
                    deleteItems(player.transform.position.z + 23);

                }
            }
        }
    }
    void generateItems(float first,float last)
    {
        string[] items = {"green","blue","red","obstacle","empty"};
        for (float i = first; i >= last; i -= 80)
        {
            int obstacles = 0;
            for (float lane = 20; lane >= -20; lane -= 20)
            {
                int r = Random.Range(0, 5);
                if (items[r] == "green")
                {
                    Instantiate(greenOrb, new Vector3(lane, 5.5f, i), Quaternion.identity);
                }
                else if (items[r] == "blue")
                {
                    Instantiate(blueOrb, new Vector3(lane, 5.5f, i), Quaternion.identity);
                }
                else if (items[r] == "red")
                {
                    Instantiate(redOrb, new Vector3(lane, 5.5f, i), Quaternion.identity);
                }
                else if (items[r] == "obstacle")
                {
                    if (obstacles < 2)
                    {
                        Instantiate(obstacle, new Vector3(lane, 0.0f, i), obstacle.transform.rotation);
                        obstacles++;
                    }

                }


            }
        }
    }
    void deleteItems(float pos)
    {
        string[] tagsToDestroy = { "redOrb", "greenOrb", "blueOrb", "obstacle" };
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (string tag in tagsToDestroy)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objectsWithTag)
            {

                if (obj.transform.position.z > pos)
                {
                    Object.DestroyImmediate(obj, true);
                }
            }
        }
    }
}
