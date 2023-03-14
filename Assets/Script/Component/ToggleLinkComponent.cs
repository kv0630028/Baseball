using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLinkComponent : MonoBehaviour
{
    public GameObject linkObject;

    private void Start()
    {
        linkObject.SetActive(GetComponent<Toggle>().isOn);
    }
    public void OnToggleChange(bool value)
    {
        linkObject.SetActive(value);
    }
}
