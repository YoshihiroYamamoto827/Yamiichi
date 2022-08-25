using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    private CSVWriter CSVWriter;
    private ValueManager Valuemanager;

    private int Rxvalue, Ryvalue;
    private string Robjectname;

    // Start is called before the first frame update
    void Start()
    {
        CSVWriter = GameObject.Find("CSVWriter").GetComponent<CSVWriter>();
        Valuemanager = GameObject.Find("ValueManager").GetComponent<ValueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteMapData()
    {
        Rxvalue = Valuemanager.Sendxvalue();
        Ryvalue = Valuemanager.Sendyvalue();
        Robjectname = Valuemanager.Sendobjectname();
        CSVWriter.WriteCSV(Rxvalue.ToString() + "," + Ryvalue.ToString() + "," + Robjectname);
        Debug.Log("Successed");
    }
}
