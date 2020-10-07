using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera thirdPerson;
    public Camera Top;
    public static bool topView = true;
    private void Start()
    {
        if (topView)
        {
            TopView();

        }
        else
        {
            ThirdPersonView();

        }
    }
    public void PressButton()
    {
        if (thirdPerson.gameObject.activeInHierarchy)
        {
            TopView();
            topView = true;

        }
        else
        {
            ThirdPersonView();
            topView = false;
        }
    }
    void ThirdPersonView()
    {
        thirdPerson.gameObject.SetActive(true);
        Top.gameObject.SetActive(false);
        UIInstance.instance.shootButton.gameObject.SetActive(true);

    }

    void TopView()
    {
        thirdPerson.gameObject.SetActive(false);
        Top.gameObject.SetActive(true);
        UIInstance.instance.shootButton.gameObject.SetActive(false);

    }

    public void Reload()
    {
        Time.timeScale = 1;
        SpawnManager.normalZombies.Clear();
        SceneManager.LoadScene(0);

    }
}
