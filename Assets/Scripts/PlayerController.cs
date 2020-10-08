using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;

    private Vector2 pointForMoveA;
    private Vector2 pointForMoveB;
    private Vector2 pointForAimA;
    private Vector2 pointForAimB;
    public float _maxSpeed = 5f;


    // Start is called before the first frame update
    [Header("MoveJoyTransform")]
    private Transform movePlaceHolder;
    private Transform moveCircle;
    [Header("ShootJoyTransform")]
    private Transform shootPlaceHolder;
    private Transform shootCircle;

    private void Start()
    {
        Input.multiTouchEnabled = true;
        movePlaceHolder = UIInstance.instance.movePlaceHolder;
        moveCircle = UIInstance.instance.moveCircle;
        shootPlaceHolder = UIInstance.instance.shootPlaceHolder;
        shootCircle = UIInstance.instance.shootCircle;

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Acceleration()
    {
        Vector3 dir = new Vector3(offset.x, 0, offset.y);

        if (CameraSwitch.topView)
        {
            transform.Translate(-dir.normalized * Time.fixedDeltaTime * _maxSpeed, Space.World);
            //transform.position = Vector3.Lerp(transform.position, transform.position - dir.normalized, Time.fixedDeltaTime * _maxSpeed);
        }
        else
        {
            transform.Translate(dir.normalized * Time.fixedDeltaTime * _maxSpeed, Space.Self);

        }



    }
    private void SetJoyPosition(Transform placeholder, Transform cirle, Vector3 pos)
    {
        placeholder.transform.position = pos;
        cirle.transform.position = pos;
    }

    private Vector2 offsetForAim;
    private Vector2 directionForAim;
    private Vector2 offset;
    private Vector2 direction;
    public float sens = 2f;
    private void FixedUpdate()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(i).position.x > Screen.width / 2)
                {
                    pointForAimA = Input.GetTouch(i).position;
                    if (CameraSwitch.topView)
                    {
                        SetJoyPosition(shootPlaceHolder, shootCircle, pointForAimA);

                        UIInstance.instance.SetOnOffJoyStickForAim(true);
                    }
                    anim.SetFloat("Speed_f", 0);

                    anim.SetBool("Shoot_b", true);
                    anim.SetInteger("WeaponType_int", 2);

                }

                if (Input.GetTouch(i).position.x < Screen.width / 2)
                {
                    pointForMoveA = Input.GetTouch(i).position;

                    SetJoyPosition(movePlaceHolder, moveCircle, pointForMoveA);

                    UIInstance.instance.SetOnOffJoyStickForMove(true);
                    anim.SetFloat("Speed_f", _maxSpeed);



                }

            }

            if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {

                if (Input.GetTouch(i).position.x > Screen.width / 2)
                {

                    pointForAimB = Input.GetTouch(i).position;


                    offsetForAim = pointForAimB - pointForAimA;
                    directionForAim = Vector2.ClampMagnitude(offsetForAim, 50f);
                    if (CameraSwitch.topView)
                    {
                        shootCircle.transform.position = (new Vector2(pointForAimA.x + directionForAim.x, pointForAimA.y + directionForAim.y));
                    }
                    if (offsetForAim != Vector2.zero)
                    {

                        offsetForAim = Vector2.ClampMagnitude(offsetForAim, 50f);
                        if (CameraSwitch.topView)
                        {
                            transform.eulerAngles = angles + new Vector3(0, (Mathf.Atan2(-offsetForAim.x, -offsetForAim.y) * 180 / Mathf.PI), 0);
                        }
                        else
                        {
                            if (offsetForAim.magnitude > 20f)
                            {
                                float xRot = (transform.eulerAngles.y + Mathf.Atan2(offsetForAim.x, offsetForAim.y) * 180 / Mathf.PI);
                                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, xRot, 0), 0.01f*offsetForAim.magnitude * Time.fixedDeltaTime);

                            }



                        }
                        angles = Vector3.zero;
                    }
                }

                if (Input.GetTouch(i).position.x < Screen.width / 2)
                {

                    pointForMoveB = Input.GetTouch(i).position;

                    offset = pointForMoveB - pointForMoveA;
                    direction = Vector2.ClampMagnitude(offset, 50f);
                    Acceleration();

                    moveCircle.transform.position = (new Vector2(pointForMoveA.x + direction.x, pointForMoveA.y + direction.y));
                }
            }
            if (Input.GetTouch(i).phase == TouchPhase.Stationary)
            {
                if (Input.GetTouch(i).position.x < Screen.width / 2)
                {

                    pointForMoveB = Input.GetTouch(i).position;

                    offset = pointForMoveB - pointForMoveA;
                    direction = Vector2.ClampMagnitude(offset, 50f);
                    Acceleration();

                    moveCircle.transform.position = (new Vector2(pointForMoveA.x + direction.x, pointForMoveA.y + direction.y));
                }

            }
            if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                if (Input.GetTouch(i).position.x > Screen.width / 2)
                {
                    UIInstance.instance.SetOnOffJoyStickForAim(false);
                    anim.SetBool("Shoot_b", false);
                    anim.SetInteger("WeaponType_int", 0);

                    anim.SetFloat("Speed_f", 0);


                }
                else if (Input.GetTouch(i).position.x < Screen.width / 2)
                {
                    UIInstance.instance.SetOnOffJoyStickForMove(false);
                    anim.SetFloat("Speed_f", 0);

                }

            }

        }
    }

    private Vector3 angles = Vector3.zero;
}

