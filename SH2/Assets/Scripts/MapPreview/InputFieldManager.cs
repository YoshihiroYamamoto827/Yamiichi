using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldManager : MonoBehaviour
{
    public TMP_InputField JsonField, ImgField;
    private string Jsonstr, Imgstr;

    // Start is called before the first frame update
    void Start()
    {
        JsonField = GameObject.Find("JsonFileInputField").GetComponent<TMP_InputField>();
        ImgField = GameObject.Find("ImgDirectoryInputField").GetComponent<TMP_InputField>();
    }

    public void GetJsonFIle()
    {
        Jsonstr = JsonField.text;
    }

    public void GetImgDirectory()
    {
        Imgstr = ImgField.text;
    }

    public string SendJsonFile()
    {
        return Jsonstr;
    }

    public string SendImgDirectory()
    {
        return Imgstr;
    }
}
