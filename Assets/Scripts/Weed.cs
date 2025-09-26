using System.Collections;
using TMPro.Examples;
using UnityEngine;

public class Weed : MonoBehaviour
{
    private Transform mainCameraParent;
    private CameraController cameraController;

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
                    if (cameraController != null && cameraController.weed == this) return;

                    cameraController.weed = this;
                    StartCoroutine(FocusOnTarget(Input.GetTouch(0).position));
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
        cameraController.weedRemoveUI.SetActive(true);
        cameraController.weedRemoveUI.transform.position = transform.position + new Vector3(0.25f, 0.25f, 0);
        mainCameraParent.transform.position = worldPos;
    }
}
