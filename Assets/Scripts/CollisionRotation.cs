using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRotation : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.instance.collided = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        GameManager.instance.collided = false;
    }
}
