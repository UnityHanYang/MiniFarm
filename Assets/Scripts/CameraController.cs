using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Weed weed;
    public bool isMove = false;

    private float speed = 3f;
    private Vector2 lastTouchPosition;
    private float minX = 3f;
    private float maxX = 5.1f;
    private float minY = 3f;
    private float maxY = 5.4f;

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

            Vector3 delta = Input.GetTouch(0).position - lastTouchPosition;
            delta = delta.normalized;
            if(transform.position.x >= 3 && transform.position.x <= 5.1 && transform.position.y <= 5.4 && transform.position.y >= 3)
            {
                transform.Translate(-delta.x * speed * Time.deltaTime, -delta.y * speed * Time.deltaTime, 0);
                lastTouchPosition = Input.GetTouch(0).position;
            }
            else
            {
                transform.position = StandardPos();
            }
        }
    }

    private Vector3 StandardPos()
    {
        Vector3 standardVec = transform.position;
        standardVec.x = Mathf.Clamp(standardVec.x, 3.1f, 6f);
        standardVec.y = Mathf.Clamp(standardVec.y, 3.1f, 6.3f);

        return standardVec;
    }
}
