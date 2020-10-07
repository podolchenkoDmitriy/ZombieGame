using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;

    private Vector2 pointForMoveA;
    private Vector2 pointForMoveB;
    private Vector2 pointForAimA;
    private Vector2 pointForAimB;
    public float _maxSpeed = 5f;

    private float _forceToMove;
    private const float _maxForceToMove = 40f;
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
    private void Acceleration(float speed, Vector3 dir)
    {
        anim.SetFloat("Speed_f", speed);
        Vector3 direction = Vector3.zero;
        if (CameraSwitch.topView)
        {
            direction = new Vector3(dir.x, 0, dir.y);
        }
        else
        {
             direction = new Vector3(offset.x, 0, offset.y);

        }
        float moveSpeed = (speed * 2f);

        moveSpeed = Mathf.Clamp(moveSpeed, 0, _maxSpeed);

        if (CameraSwitch.topView)
        {
            transform.Translate(-direction.normalized * Time.fixedDeltaTime * moveSpeed, Space.World);
        }
        else
        {
            transform.Translate(-direction.normalized * Time.fixedDeltaTime * moveSpeed, Space.World);

        }



    }
    private void SetJoyPosition(Transform placeholder, Transform cirle, Vector3 pos)
    {
        placeholder.transform.position = pos;
        cirle.transform.position = pos;
    }

    private Vector2 offsetForAim;
    private Vector2 directionForAim;
    private float force;
    private Vector2 offset;
    private Vector2 direction;

    private void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                _forceToMove = 0;

                if (Input.GetTouch(i).position.x > Screen.width / 2)
                {
                    pointForAimA = Input.GetTouch(i).position;
                    if (CameraSwitch.topView)
                    {
                        SetJoyPosition(shootPlaceHolder, shootCircle, pointForAimA);

                        UIInstance.instance.SetOnOffJoyStickForAim(true);
                    }

                    anim.SetBool("Shoot_b", true);
                    anim.SetInteger("WeaponType_int", 2);

                }

                if (Input.GetTouch(i).position.x < Screen.width / 2)
                {
                    pointForMoveA = Input.GetTouch(i).position;

                    SetJoyPosition(movePlaceHolder, moveCircle, pointForMoveA);

                    UIInstance.instance.SetOnOffJoyStickForMove(true);



                }

            }

            if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {

                if (Input.GetTouch(i).position.x > Screen.width / 2)
                {

                    pointForAimB = Input.GetTouch(i).position;


                    offsetForAim = pointForAimB - pointForAimA;
                    directionForAim = Vector2.ClampMagnitude(offsetForAim, 20f);
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
                            transform.eulerAngles = angles + new Vector3(0, (Mathf.Atan2(-offsetForAim.x, -offsetForAim.y) * 360 / Mathf.PI), 0);

                        }
                        angles = Vector3.zero;
                    }
                }

                if (Input.GetTouch(i).position.x < Screen.width / 2)
                {

                    pointForMoveB = Input.GetTouch(i).position;

                    offset = pointForMoveB - pointForMoveA;
                    direction = Vector2.ClampMagnitude(offset, 20f);

                    _forceToMove = Mathf.Abs(offset.x) + Mathf.Abs(offset.y);

                    force = _forceToMove / _maxForceToMove;

                    Acceleration(force, offset);

                    moveCircle.transform.position = (new Vector2(pointForMoveA.x + direction.x, pointForMoveA.y + direction.y));
                }
            }

            if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                if (Input.GetTouch(i).position.x > Screen.width / 2)
                {
                    if (CameraSwitch.topView)
                    {
                        UIInstance.instance.SetOnOffJoyStickForAim(false);
                    }


                }
                else if (Input.GetTouch(i).position.x < Screen.width / 2)
                {
                    UIInstance.instance.SetOnOffJoyStickForMove(false);
                    Acceleration(0, Vector3.zero);

                }

            }

        }

    }

    private Vector3 angles = Vector3.zero;
}

