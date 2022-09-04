using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueManager : MonoBehaviour
{
    private Text xaxisvalue, yaxisvalue;
    private int xvalue, yvalue;
    private Dropdown ddtmp;
    private string objectname;

    // Start is called before the first frame update
    void Start()
    {
        //テキストのGameObjectを探し、それぞれの座標を表す変数を0で初期化
        xaxisvalue = GameObject.Find("XAxisValue").GetComponent<Text>();
        yaxisvalue = GameObject.Find("YAxisValue").GetComponent<Text>();
        xvalue = 0;
        yvalue = 0;

        //X座標、Y座標それぞれについて初期化した0を表示
        xaxisvalue.text = xvalue.ToString();
        yaxisvalue.text = yvalue.ToString();

        ddtmp = GameObject.Find("ObjectChooseDropdown").GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void XAxisAdd()
    {
        if(0 <= xvalue && xvalue < 99)
        {
            xvalue++;
            xaxisvalue.text = xvalue.ToString();
        }
    }

    public void XAxisSub()
    {
        if (0 < xvalue && xvalue <= 99)
        {
            xvalue--;
            xaxisvalue.text = xvalue.ToString();
        }
    }

    public void YAxisAdd()
    {
        if (0 <= xvalue && xvalue < 99)
        {
            yvalue++;
            yaxisvalue.text = yvalue.ToString();
        }
    }

    public void YAxisSub()
    {
        if (0 < xvalue && xvalue <= 99)
        {
            yvalue--;
            yaxisvalue.text = yvalue.ToString();
        }
    }

    public int Sendxvalue()
    {
        return xvalue;
    }

    public int Sendyvalue()
    {
        return yvalue;
    }

    public string Sendobjectname()
    {
        objectname = ddtmp.options[ddtmp.value].text;
        return objectname;
    }
}
