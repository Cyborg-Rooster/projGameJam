using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    float distance = -10f, height = 0f, damping = 5.0f;
    public float maxX, maxY, minX, minY;

    public float shakeDuration, shakeMagnetude, shakeMaxX, shakeMinX;

    /*public void ShakeObject()
    {
        StartCoroutine(Shake.ShakeObject(shakeDuration, shakeMagnetude, shakeMinX, shakeMaxX, transform));
    }*/

    void Update()
    {
        // get the position of the target (AKA player)
        Vector3 wantedPosition = target.TransformPoint(0, height, distance);

        // check if it's inside the boundaries on the X position
        wantedPosition.x = (wantedPosition.x < minX) ? minX : wantedPosition.x;
        wantedPosition.x = (wantedPosition.x > maxX) ? maxX : wantedPosition.x;

        // check if it's inside the boundaries on the Y position
        wantedPosition.y = (wantedPosition.y < minY) ? minY : wantedPosition.y;
        wantedPosition.y = (wantedPosition.y > maxY) ? maxY : wantedPosition.y;

        // set the camera to go to the wanted position in a certain amount of time
        transform.position = Vector3.MoveTowards(transform.position, wantedPosition, (Time.deltaTime * damping));
    }
}

