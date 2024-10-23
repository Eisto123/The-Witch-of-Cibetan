using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpellCardSO",menuName ="Card/SpellCardSO")]
public class SpellCardSO : ScriptableObject
{
    public SpellName spellName;
    public Sprite image;
    public float damage;
    public CastMethod castMethod;

}
