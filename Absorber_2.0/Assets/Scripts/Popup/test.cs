using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public CSVReader CSVReader;
    public List<Dictionary<string, object>> data1;
    public List<Dictionary<string, object>> data2;
    public List<Dictionary<string, object>> data3;
    public List<Dictionary<string, object>> data4;
    public Dictionary<string, string> Status_data;
    // Start is called before the first frame update
    void Start()
    {
        // CSVReader = transform.GetComponent<CSVReader>();
        // data1 = CSVReader.Read("LevelUpTable1");
        // data2 = CSVReader.Read("LevelUpTable2");
        // data3 = CSVReader.Read("LevelUpTable3");
        // data4 = CSVReader.Read("LevelUpTable4");
        // Status_data = CSVReader.ReadCsvFile("StatusTable");
    }
}
