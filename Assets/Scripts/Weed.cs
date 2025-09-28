using System.Collections;
using UnityEngine;

public class Weed : MonoBehaviour
{
    private Transform mainCameraParent;
    private CameraController cameraController;
    private bool isRemoveTouch = false;
    private int removeTouchCount = 0;
    private int removeTouchMax = 5;

    private void Start()
    {
        mainCameraParent = Camera.main.transform.parent.GetComponent<Transform>();
        cameraController = mainCameraParent.GetComponent<CameraController>();
    }

    private void Update()
    {
        CheckTouch();
    }

    private void CheckTouch()
    {
        if (Input.touchCount > 0 && !cameraController.isMove && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.Equals(gameObject))
                {
                    if (!isRemoveTouch)
                    {
                        if (cameraController != null && cameraController.weed == this) return;

                        cameraController.weed = this;
                        StartCoroutine(FocusOnTarget(Input.GetTouch(0).position));
                    }
                    else
                    {
                        removeTouchCount++;
                        if(removeTouchMax == removeTouchCount)
                        {
                            gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    IEnumerator FocusOnTarget(Vector3 touchPos)
    {
        yield return new WaitForSeconds(0.25f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, touchPos.z));

        while (Vector3.Distance(mainCameraParent.transform.position, worldPos) > 0.01f)
        {
            mainCameraParent.transform.position = Vector3.Slerp(mainCameraParent.transform.position, worldPos, 0.2f);
            yield return null;
        }
        cameraController.removeBtn.onClick.RemoveAllListeners();
        cameraController.weedRemoveUI.SetActive(true);
        cameraController.weedRemoveUI.transform.position = transform.position + new Vector3(0.25f, 0.25f, 0);
        cameraController.removeBtn.onClick.AddListener(RemoveClick);
        mainCameraParent.transform.position = worldPos;
    }

    public void RemoveClick()
    {
        isRemoveTouch = true;
        cameraController.weedRemoveUI.SetActive(false);
    }
}
