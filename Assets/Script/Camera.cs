using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Target;
    public float CameraZ = -10;
    
    void FixedUpdate()
    {
        Vector3 TargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y, CameraZ);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * 2f);
    }
}
