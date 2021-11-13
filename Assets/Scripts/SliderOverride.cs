using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderOverride : MonoBehaviour, IEndDragHandler
{
    Slider slider;

    private void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(ChangeSomeValue(slider.value, 0, 0.7f));
    }

    IEnumerator ChangeSomeValue(float oldValue, float newValue, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            slider.value = Mathf.Lerp(oldValue, newValue, t / duration);
            yield return null;
        }
        slider.value = newValue;
    }
}
