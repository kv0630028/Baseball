using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSinMoveTest : MonoBehaviour
{
    public Transform oriPos;
    private Vector3 retVector;

    public float speed = 0.01f;
    public float degree = 0;
    public float distance = 0.000001f;

    void Start()
    {
        retVector = oriPos.transform.localPosition; 
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            degree += speed;
            float radian = (degree * Mathf.PI / 180) * 5f; //라디안값

            retVector.x +=  Mathf.Cos(radian);
            retVector.z +=  Mathf.Sin(radian);

            transform.position = retVector;
        }
    }
}
