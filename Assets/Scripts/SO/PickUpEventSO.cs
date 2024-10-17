
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="EventSO",menuName ="Event/EventSO")]
public class PickUpEventSO : ScriptableObject
{
    public UnityAction<ElementType> OnEventRise;

    public void RiseEvent(ElementType type)
    {
        OnEventRise?.Invoke(type);
    }

}
