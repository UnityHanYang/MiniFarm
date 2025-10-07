using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    #region public 변수
    public Weed weed;
    public bool isMove = false;
    public GameObject weedRemoveUI;
    public Button removeBtn;
    public Slider slider;
    public Transform target;
    public bool isFocusing = false;
    #endregion

    #region private 변수
    private float cameraMoveSpeed = 1.8f;
    #endregion

    public void MoveCamera(Vector2 deltaPosition)
    {
        if (weed != null && weed.isWeedRemove || isFocusing) return;
        this.target = null;
        deltaPosition = deltaPosition.normalized;
        transform.Translate(-deltaPosition.x * cameraMoveSpeed * Time.deltaTime, -deltaPosition.y * cameraMoveSpeed * Time.deltaTime, 0);
    }

    public void SetFocusTarget(Transform target, Vector3 touchPos)
    {
        weed = target.GetComponent<Weed>();
        if (this.target != null && this.target == target && !weed.isWeedRemove)
        {
            weedRemoveUI.SetActive(true);
            weedRemoveUI.transform.position = target.position + new Vector3(0.25f, 0.25f, 0);
            return;
        }
        this.target = target;

        if (weed.isWeedRemove)
        {
            weed.AddRemoveTouchCount();
        }
        else
        {
            isFocusing = true;
            if (weed != null)
            {
                removeBtn.onClick.RemoveAllListeners();
                weed.ShowInteractUI();
            }

            StartCoroutine(FocusOnTarget(this.target, touchPos));
        }
    }

    IEnumerator FocusOnTarget(Transform target, Vector3 touchPos)
    {
        yield return new WaitUntil(() => !TouchManager.instance.isFocusing);
        TouchManager.instance.isFocusing = true;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, touchPos.z));

        while (Vector3.Distance(transform.position, worldPos) > 0.01f)
        {
            transform.position = Vector3.Slerp(transform.position, worldPos, 0.2f);

            yield return null;
        }
        weedRemoveUI.SetActive(true);
        weedRemoveUI.transform.position = target.position + new Vector3(0.25f, 0.25f, 0);
        transform.position = worldPos;
        TouchManager.instance.isFocusing = false;
        isFocusing = false;
    }

    public void ResetFocus()
    {
        target = null;
    }
}
