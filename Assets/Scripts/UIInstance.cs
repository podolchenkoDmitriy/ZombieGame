using UnityEngine;
using UnityEngine.UI;

public class UIInstance : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIInstance instance;
    public Text countStartText;
    public Image shootButton;
    [Header("MoveJoyTransform")]
    [Space]
    public Transform movePlaceHolder;
    public Transform moveCircle;
    [Header("ShootJoyTransform")]
    [Space]
    public Transform shootPlaceHolder;
    public Transform shootCircle;

    [Header("UIPanels")]
    [Space]
    public Transform winPanel;
    public Transform losePanel;

    [Header("UIItems")]
    [Space]
    public Transform[] lives;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this);
        }

    }

    private void Start()
    {
        SetOnOffJoyStickForMove(false);
        SetOnOffJoyStickForAim(false);
        countOfLives = lives.Length - 1;
        if (CameraSwitch.topView)
        {
            shootButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public void SetOnOffJoyStickForMove(bool state)
    {
        movePlaceHolder.gameObject.SetActive(state);
        moveCircle.gameObject.SetActive(state);
    }
    public void SetOnOffJoyStickForAim(bool state)
    {
        shootPlaceHolder.gameObject.SetActive(state);
        shootCircle.gameObject.SetActive(state);
    }

    private int countOfLives;
    public void SetOfLives()
    {
        lives[countOfLives].gameObject.SetActive(false);
        countOfLives--;
        if (countOfLives < 0)
        {
            losePanel.gameObject.SetActive(true);
            
            Animator anim =  FindObjectOfType<PlayerController>().GetComponent<Animator>();
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);

            Time.timeScale = 0;
        }
    }

}
