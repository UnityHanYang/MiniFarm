using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    #region public 변수
    public CameraController cameraController;
    public bool isWeedRemove;
    public static TouchManager instance;
    public bool isFocusing = false;
    #endregion

    #region private 변수
    private Weed weed;
    private float tapTimeThreshold = 0.2f;
    private float dragDistanceThreshold = 50f;
    private Vector2 touchStartPos;
    private float touchStartTime;
    public float touchSensitivity = 0.5f;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                touchStartTime = Time.time;
                cameraController.weedRemoveUI.SetActive(false);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float touchDistance = Vector2.Distance(touchStartPos, touch.position);
                if (touchDistance > dragDistanceThreshold)
                {
                    // deltaPosition에 민감도를 곱해서 움직임 감소
                    cameraController.MoveCamera(touch.deltaPosition * touchSensitivity);
                    weed = null;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                float touchDuration = Time.time - touchStartTime;
                float touchDistance = Vector2.Distance(touchStartPos, touch.position);

                if (touchDuration < tapTimeThreshold && touchDistance < dragDistanceThreshold)
                {
                    HandleTap(touch.position);
                }
            }
        }
    }

    private void HandleTap(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Weed weed = hit.collider.GetComponent<Weed>();

            if (weed != null)
            {
                this.weed = weed;
                cameraController.SetFocusTarget(this.weed.transform, touchPosition);
            }
        }
    }
}
