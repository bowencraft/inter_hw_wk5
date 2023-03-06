using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject PlayerObject;
    public float CameraSpeed = 0.5f;
    public int CameraOffset = 3;

    public GameObject door;
    public GameObject key;

    private bool hasKey = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPos = transform.position;

        newPos.x = Mathf.Lerp(newPos.x, PlayerObject.transform.position.x, CameraSpeed * Time.deltaTime);
        newPos.y = Mathf.Lerp(newPos.y, PlayerObject.transform.position.y + CameraOffset, CameraSpeed * Time.deltaTime);

        transform.position = newPos;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("door") && hasKey)
        {
            Destroy(door);
        }

        if (collision.gameObject.name.Equals("key"))
        {
            hasKey = true;
            // play sound
            Destroy(key);
        }

    }
}