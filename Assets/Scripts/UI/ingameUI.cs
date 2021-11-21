using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingameUI : MonoBehaviour
{
    public void changeDisplay (bool shouldBeDisplayed) {
        gameObject.SetActive(shouldBeDisplayed);
    }    
}
