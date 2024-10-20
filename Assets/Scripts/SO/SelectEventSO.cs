using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="EventSO",menuName ="Event/SelectEventSO")]
public class SelectEventSO : ScriptableObject
{
    public UnityAction<ElementCards> OnEventRise;

    public void RiseEvent(ElementCards cards)
    {
        OnEventRise?.Invoke(cards);
    }

}
