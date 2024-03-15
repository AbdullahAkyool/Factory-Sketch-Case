using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    public CinemachineVirtualCamera[] cameras;
    private int cameraIndex;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeCamera()
    {
        foreach (var cam in cameras)
        {
            cam.enabled = false;
        }

        cameraIndex++;

        if (cameraIndex >= cameras.Length)
        {
            cameraIndex = 0;
        }
        
        cameras[cameraIndex].enabled = true;
        UIController.Instance.ActivateMainPanel();
    }
}
