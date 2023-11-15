using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] Transform carTrans;
    [SerializeField] Transform dirTrans;

    public float forwardSpeed;
    public float carRotateSpeed;
    public float dirRotateSpeed;
    public float dirReAlignSpeed;
    public float brakeForce;

    public float maxCarRotX;

    private Quaternion targetCarRot;
    private float lerpCarRot;
    public float lerpDirRot;
    public float lerpDirReAlign;

    private Touch currTouch;
    private float startTouchX;


    private void Update()
    {
        bool flag = true;
        float deltaX = 0f;


        if (Input.touchCount > 0)
        {
            currTouch = Input.GetTouch(0);


            if (currTouch.phase == TouchPhase.Began)
            {
                startTouchX = currTouch.position.x;
                lerpCarRot = 0f;
                lerpDirRot = 0f;
                lerpDirReAlign = 0f;
            }
            else if (currTouch.phase == TouchPhase.Moved || currTouch.phase == TouchPhase.Stationary)
            {
                deltaX = (currTouch.position.x - startTouchX) / Screen.width;

                if (deltaX > maxCarRotX) deltaX = maxCarRotX;
                else if (deltaX < -maxCarRotX) deltaX = -maxCarRotX;

                float rotationAngle = (deltaX * carRotateSpeed) * Time.deltaTime;

                targetCarRot = Quaternion.Euler(0f, (carTrans.localEulerAngles.y + rotationAngle), 0f);

                lerpCarRot += Time.deltaTime * 1.5f;
                carTrans.localRotation = Quaternion.Slerp(carTrans.localRotation, targetCarRot, lerpCarRot);
                flag = false;
            }
            else if (currTouch.phase == TouchPhase.Ended)
            {
                lerpCarRot = 0f;
                lerpDirRot = 0f;
                lerpDirReAlign = 0f;
            }
        }
        if (flag)
        {
            lerpDirReAlign += Time.deltaTime * dirReAlignSpeed;
            dirTrans.rotation = Quaternion.Slerp(dirTrans.rotation, carTrans.rotation, lerpDirReAlign);
        }
        else
        {
            //lerpDirRot += Time.deltaTime * dirRotateSpeed;
            dirTrans.rotation = Quaternion.Slerp(dirTrans.rotation, carTrans.rotation, (Time.deltaTime * dirRotateSpeed));
        }
        //Debug.Log("Check = " + )
        transform.Translate((dirTrans.forward.normalized) * Time.deltaTime * (forwardSpeed - Mathf.Abs(brakeForce * deltaX)), Space.Self);
    }
}
