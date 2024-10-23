using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="CastSpellEventSO",menuName ="Event/CastSpellEventSO")]
public class CastSpellEventSO : ScriptableObject
{
    public UnityAction<SpellCards> OnEventRise;

    public void RiseEvent(SpellCards cards)
    {
        OnEventRise?.Invoke(cards);
    }

}
