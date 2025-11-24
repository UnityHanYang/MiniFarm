using UnityEngine;

public class LandPurchaseManager : MonoBehaviour
{
    #region public 변수
    public Transform installGroundPos;
    public GameObject groundPrefab;
    public Transform groundParent;
    #endregion

    #region private 변수
    private int installGroundCount = 0;
    private int maxSideConut = 4;
    private int maxOutSideConut = 16;
    private int rotateMultiple = 0;
    #endregion

    void Start()
    {

    }

    void Update()
    {

    }

    public void GroundPurchase()
    {
        GameObject ground = Instantiate(groundPrefab, groundParent);
        ground.transform.position = installGroundPos.position;
        installGroundCount++;
        SetGroundRotation(ground);
        installGroundPos = ground.transform.GetChild(1);
    }
    private void SetGroundRotation(GameObject ground)
    {
        if (installGroundCount == maxOutSideConut)
        {
            rotateMultiple = 0;
            installGroundCount = 0;
            maxOutSideConut += 8;
            maxSideConut += 2;
            ground.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else
        {
            if (installGroundCount != 0 && installGroundCount % maxSideConut == 0)
            {
                rotateMultiple++;
            }
            ground.transform.rotation = Quaternion.Euler(0, rotateMultiple * 90f, 0);
        }
    }
}
