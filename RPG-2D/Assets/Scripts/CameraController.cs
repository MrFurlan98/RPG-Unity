using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Tilemap map;

    private Vector3 bottomLeft;
    private Vector3 topRight;

    private float halfHeight;
    private float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        //target = PlayerController.instance.transform;
        target = FindObjectOfType<PlayerController>().transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeft = map.localBounds.min + new Vector3(halfWidth,halfHeight,0);
        topRight = map.localBounds.max - new Vector3(halfWidth, halfHeight, 0);

        PlayerController.instance.SetBounds(map.localBounds.min, map.localBounds.max);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeft.x, topRight.x),
                                         Mathf.Clamp(transform.position.y, bottomLeft.y, topRight.y),
                                         transform.position.z);
    }
}
