using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Weed weed;
    public bool isMove = false;
    public GameObject weedRemoveUI;
    public bool isTouch = false;
    public Button removeBtn;
    public Slider slider;

    private bool isMoveTouch = false;
    private float speed = 4f;
    private Vector2 lastTouchPosition;
    private float touchTime = 0f;

    private void Update()
    {
        CheckClick();

        if(isTouch)
        {
            CheckTouch();
        }
    }

    private void CheckClick()
    {
        if (Input.touchCount > 1 || Input.touchCount == 0 || EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            isMove = false;
            isTouch = true;
            touchTime = 0f;
            lastTouchPosition = Input.GetTouch(0).position;
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Moved /*&& isMoveTouch*/)
        {
            isMove = true;
            weed = null;
            weedRemoveUI.SetActive(false);
            Vector3 delta = Input.GetTouch(0).position - lastTouchPosition;
            delta = delta.normalized;   
            transform.Translate(-delta.x * speed * Time.deltaTime, -delta.y * speed * Time.deltaTime, 0);
            //transform.position = StandardPos();
            lastTouchPosition = Input.GetTouch(0).position;
        }
        else if(Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isTouch = false;
            isMoveTouch = false;
            touchTime = 0f;
        }
    }

    private void CheckTouch()
    {
        touchTime += Time.deltaTime;

        if (touchTime > 0.5f)
        {
            isMoveTouch = true;
        }
    }

    private Vector3 StandardPos()
    {
        Vector3 standardVec = transform.position;
        standardVec.x = Mathf.Clamp(standardVec.x, 0f, 6f);
        standardVec.y = Mathf.Clamp(standardVec.y, 0f, 6.3f);
        standardVec.z = -2.21f;

        return standardVec;
    }
}
