using System.Collections;
using UnityEngine;

public class Weed : MonoBehaviour
{
    private Camera mainCamera;
    public float targetOrthographicSize = 1.0f; // 목표 Orthographic Size
    public float lerpSpeed = 1.0f; // 부드러운 이동 속도

    private Vector3 initialPosition;
    private float initialOrthographicSize;

    private void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position;
        initialOrthographicSize = mainCamera.orthographicSize;
    }
    public void StartFocus()
    {
        // 카메라의 위치와 orthographicSize를 부드럽게 변경
        StopAllCoroutines();
        StartCoroutine(FocusOnTarget());
    }
    public void StopFocus()
    {
        // 카메라의 위치와 orthographicSize를 초기 상태로 되돌림
        StopAllCoroutines();
        StartCoroutine(ReturnToInitial());
    }

    IEnumerator FocusOnTarget()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);

        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 1f /*|| Mathf.Abs(Camera.main.orthographicSize - targetOrthographicSize) > 1f*/)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * lerpSpeed);
            //Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetOrthographicSize, Time.deltaTime * lerpSpeed);
            yield return null;
        }
    }

    private System.Collections.IEnumerator ReturnToInitial()
    {
        while (Vector3.Distance(transform.position, initialPosition) > 0.01f || Mathf.Abs(Camera.main.orthographicSize - initialOrthographicSize) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * lerpSpeed);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, initialOrthographicSize, Time.deltaTime * lerpSpeed);
            yield return null;
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.transform.position.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        while(Vector3.Distance(mainCamera.transform.position, worldPosition) > 0.5f )
        {
            mainCamera.transform.position = Vector3.Slerp(mainCamera.transform.position, worldPosition, 0.1f);
        }
    }   
}
