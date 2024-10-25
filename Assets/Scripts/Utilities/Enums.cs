public enum ElementType{
    Water,Fire,Wood,Earth
}
public enum SpellName{
    Wave,Storm,Fireball
}

public enum CastMethod{
    SkillShot, Range, Cone, Self
}
public static class Extensions{
    public static T GetRandomEnum<T>()
	{
	    System.Array A = System.Enum.GetValues(typeof(T));
	    T V = (T)A.GetValue(UnityEngine.Random.Range(0,A.Length));
	    return V;
	}

}