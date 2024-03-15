using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider)
                {
                    GameObject selectedObject = hit.collider.gameObject;

                    if (selectedObject.CompareTag("Factory"))
                    {
                        CameraController.Instance.ChangeCamera();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ActionManager.Instance.OnIncreaseOrderAndProductValue?.Invoke();
            ActionManager.Instance.OnIncreaseOrderCount?.Invoke();
        }
    }
}
