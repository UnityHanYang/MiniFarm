using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Weed weed;
    public bool isMove = false;

    private float speed = 3f;
    private Vector2 lastTouchPosition;

    private void Update()
    {
        CheckClick();
    }

    private void CheckClick()
    {
        if (Input.touchCount > 1 || Input.touchCount == 0) return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            isMove = false;
            lastTouchPosition = Input.GetTouch(0).position;
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            isMove = true;
            weed = null;

            Vector3 delta = Input.GetTouch(0).position - lastTouchPosition;
            delta = delta.normalized;
            transform.Translate(-delta.x * speed * Time.deltaTime, -delta.y * speed * Time.deltaTime, 0);
            //transform.position = StandardPos();
            lastTouchPosition = Input.GetTouch(0).position;
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
