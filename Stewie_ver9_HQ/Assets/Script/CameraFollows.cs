using UnityEngine;
using System.Collections;

public class CameraFollows : MonoBehaviour
{

    public Transform target;
    public Transform target_2;
    private Vector3 offsetPosition = new Vector3(0, 2, -12);
    private Space offsetPositionSpace = Space.Self;
    private bool lookAt = true;
    public float degreesPerSecond = 90f;

    private void Update()
    {
        if (target == target_2)
        {
            offsetPosition = new Vector3(0, 15, -40);
        }
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            Quaternion targetRotation = Quaternion.LookRotation(DirectionXZ());
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, degreesPerSecond * Time.deltaTime);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }

    Vector3 DirectionXZ()
    {
        Vector3 direction = target.position - transform.position;
        //direction.y = 0; // Ignore Y
        return direction;
    }

}