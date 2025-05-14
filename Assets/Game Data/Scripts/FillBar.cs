using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    public float FillValue;
    public Image FillImage;

    private float currentFill;
    private float Speed = 1.12f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        currentFill = 0;
        FillImage.fillAmount = currentFill;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFill < FillValue)
        {
            currentFill += (Time.deltaTime * Speed);
            FillImage.fillAmount = currentFill;
        }
    }
}
