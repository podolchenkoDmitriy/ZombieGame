using UnityEngine;
using UnityEngine.EventSystems;

public class PointerDown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public static bool buttonPressed;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonPressed = false;
    }


}
