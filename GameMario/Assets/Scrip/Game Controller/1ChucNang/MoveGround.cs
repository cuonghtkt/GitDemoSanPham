using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    private Vector3 pointB = Vector3.left;
    public Transform targettransform;
    public float reverseSpeed = 30f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        pointB = targettransform.position;
        Vector3 pointA = transform.position;

        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, reverseSpeed));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, reverseSpeed));
        }
    }

    IEnumerator MoveObject(Transform thistransform, Vector3 a, Vector3 b, float reverseSpeed)
    {
        float i = 0;
        float rate = 1.0f / reverseSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thistransform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
