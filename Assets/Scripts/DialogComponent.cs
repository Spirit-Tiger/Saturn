using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogComponent : MonoBehaviour
{
    public string WriterNameString;
    public string TextString;

    public TextMeshProUGUI WriterNameObject;
    public TextMeshProUGUI TextObject;

    private void Awake()
    {
        WriterNameObject = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        WriterNameObject.text = WriterNameString;
        TextObject = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }


}
