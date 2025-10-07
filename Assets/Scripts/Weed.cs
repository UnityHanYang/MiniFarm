using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Weed : MonoBehaviour
{
    #region public 변수
    public bool isWeedRemove = false;
    #endregion

    #region private 변수
    private Transform mainCameraParent;
    private CameraController cameraController;
    private int removeTouchCount = 0;
    private int removeTouchMax = 5;
    #endregion

    private void Start()
    {
        mainCameraParent = Camera.main.transform.parent.GetComponent<Transform>();
        cameraController = mainCameraParent.GetComponent<CameraController>();
    }

    public void ShowInteractUI()
    {
        if (cameraController != null)
        {
            cameraController.removeBtn.onClick.AddListener(RemoveClick);
        }
    }

    public void RemoveClick()
    {
        isWeedRemove = true;
        if (cameraController.slider != null)
        {
            cameraController.slider.gameObject.SetActive(true);
            cameraController.slider.transform.position = transform.position + new Vector3(0, 0.27f, 0);
        }
        if (cameraController.weedRemoveUI != null) cameraController.weedRemoveUI.SetActive(false);
    }

    public void AddRemoveTouchCount()
    {
        if (!isWeedRemove) return;

        removeTouchCount++;
        cameraController.slider.value = removeTouchCount;

        if (removeTouchCount >= removeTouchMax)
        {
            RemoveWeed();
        }
    }

    private void RemoveWeed()
    {
        isWeedRemove = false;
        if (cameraController.slider != null)
        {
            cameraController.slider.value = 0;
            cameraController.slider.gameObject.SetActive(false);
        }
        cameraController.ResetFocus();
        gameObject.SetActive(false);
    }
}
