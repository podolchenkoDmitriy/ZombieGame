using UnityEngine;
using UnityEngine.EventSystems;

public class PointerDown : MonoBehaviour,  IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{

    public static bool buttonPressed;

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;

    }
}
