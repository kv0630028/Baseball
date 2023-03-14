using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeZoneComponent : MonoBehaviour
{
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + new Vector3(0, 0.25f));   
    }

}
