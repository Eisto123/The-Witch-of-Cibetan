using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

[CreateAssetMenu(fileName ="SynthesisMethodSO",menuName ="Card/SynthesisMethodSO")]
public class SynthesisMethodSO : ScriptableObject
{
    public ElementType element1;
    public ElementType element2;
    private SpellName spellName;

    // return the spell name or SO?
    public SpellName SynthesisMethod(){
        switch(element1){
            case ElementType.Water:
            if(element2 == ElementType.Water){
                spellName = SpellName.Wave;
            }
            if(element2 == ElementType.Fire){
                spellName = SpellName.Storm;
            }
            return spellName;
            
            case ElementType.Fire:
            if(element2 == ElementType.Water){
                spellName = SpellName.Storm;
            }
            if(element2 == ElementType.Fire){
                spellName = SpellName.Fireball;
            }
            return spellName;

        }
        return spellName;
    }


}
