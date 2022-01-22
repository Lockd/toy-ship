using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffContainer : MonoBehaviour
{
    List<Buff> ActiveBuffs;
    public GameObject magnetIcon;
    public GameObject timeRewindIcon;
    Vector3 positionDifference = new Vector3 (0f, 120f, 0f);

    void Start()
    {
        ActiveBuffs = new List<Buff>();
        // addBuff(7f, "Magnet", magnetIcon);
        // addBuff(-1, "Rewind", timeRewindIcon);
    }

    void Awake()
    {
        ActiveBuffs = new List<Buff>();
    }

    void Update()
    {
        for (int i = 0; i < ActiveBuffs.Count; i++)
        {
            Buff buff = ActiveBuffs[i];
            if (Time.time > buff.endsAt && buff.endsAt != -1)
            {
                ActiveBuffs.RemoveAt(i);
                updateUI();
            }
        }
    }

    void updateUI ()
    {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < ActiveBuffs.Count; i++)
        {
            GameObject buffIcon = Instantiate(ActiveBuffs[i].icon, transform);
            buffIcon.transform.position -= positionDifference * i;
        }
    }

    public void addBuff(float endsAt, string type, GameObject icon)
    {
        ActiveBuffs.Add(new Buff(endsAt, type, icon));
        updateUI();
    }

    public void removeBuff(string type)
    {
        for (int i = 0; i < ActiveBuffs.Count; i++)
        {
            Buff buff = ActiveBuffs[i];
            if (buff.type == type)
            {
                ActiveBuffs.RemoveAt(i);
            }
        }
        updateUI();
    }
}
