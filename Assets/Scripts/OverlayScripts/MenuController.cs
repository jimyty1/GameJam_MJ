using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    private void OnTab(InputValue value)
    {
        if(value.Get<float>() != 0){
            Debug.Log("tab");
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
