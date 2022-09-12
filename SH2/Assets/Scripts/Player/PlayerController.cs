using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float mainSPEED;
    public float x_sensi;
    public float y_sensi;
    public new GameObject camera;
    public Slider Xsencislider;
    public Slider Ysencislider;
    public GameObject XSenciText;
    public GameObject YSenciText;
    Text xsencitext;
    Text ysencitext;

    void Start()
    {
        xsencitext = XSenciText.GetComponent<Text>();
        ysencitext = YSenciText.GetComponent<Text>();
        xsencitext.text = x_sensi.ToString();
        ysencitext.text = y_sensi.ToString();
    }

    void Update()
    {
        movecon();
        cameracon();
    }

    void movecon()
    {
        Transform trans = transform;
        transform.position = trans.position;
        trans.position += trans.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * mainSPEED * 0.1f;
        trans.position += trans.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") * mainSPEED * 0.1f;
    }

    void cameracon()
    {
        float x_Rotation = Input.GetAxis("Mouse X");
        float y_Rotation = Input.GetAxis("Mouse Y");
        x_Rotation = x_Rotation * x_sensi;
        y_Rotation = y_Rotation * y_sensi;
        this.transform.Rotate(0, x_Rotation, 0);
        camera.transform.Rotate(-y_Rotation, 0, 0);
    }

    public void XSenciChange()
    {
        x_sensi = Xsencislider.value;
        xsencitext.text = x_sensi.ToString("F1");
    }

    public void YSenciChange()
    {
        y_sensi = Ysencislider.value;
        ysencitext.text = y_sensi.ToString("F1");
    }
}
