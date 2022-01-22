using UnityEngine;

public class Buff
{
    public float endsAt;
    public GameObject icon;
    public string type;
    
    public Buff(float _endsAt, string _type, GameObject _icon)
    {
        endsAt = _endsAt;
        type = _type;
        icon = _icon;
    }
}
