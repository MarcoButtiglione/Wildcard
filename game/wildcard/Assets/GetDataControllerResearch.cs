using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections;

public class GetDataControllerResearch : MonoBehaviour
{
    //string filePath = @"D:\Data";
    //string delimiter = ",";
    //The future path of the files
    //private float passedTime = 0f;
    string filePath = @"D:\WildcardData\";
    [SerializeField] private GameObject objToAnalyze;
    private ResearchManager_1 toAnalyze;
    private int isClickingRight = 0;
    private int isFocusingRight = 0;
    private int currentState = 0;

    public class DataToCollect
    {
        String timeFromStart;
        int isFocusing;
        int isClicking;

        public DataToCollect(string v1, int v2, int v3)
        {
            timeFromStart = v1;
            isFocusing = v2;
            isClicking = v3;
        }
        public String GetTime()
        {
            return timeFromStart;
        }
        public int GetFocusing()
        {
            return isFocusing;
        }
        public int GetClicking()
        {
            return isClicking;
        }
    }

    public List<DataToCollect> myDataList = new List<DataToCollect>();

    // Start is called before the first frame update
    void Start()
    {
        //passedTime = 0f;
        toAnalyze = objToAnalyze.GetComponent<ResearchManager_1>();
        filePath = filePath + "Research Session " + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".csv";
    }

    // Update is called once per frame
    void Update()
    {
        //passedTime += Time.fixedDeltaTime;
        //if (passedTime > 0.1f)
        //{
        currentState = toAnalyze.getCurrentState();
        if (GameObject.Find("ResearchObj").transform.GetChild(currentState).gameObject.GetComponent<FocusController>().getFocused())
        {
            isFocusingRight = 1;
        }
        else
        {
            isFocusingRight = 0;
        }
        if (isClickingRight == 1)
        {
            myDataList.Add(new DataToCollect(DateTime.Now.ToString("mm.ss.ff"), isFocusingRight, isClickingRight));
            isClickingRight = 0;
        }
        else
        {
            myDataList.Add(new DataToCollect(DateTime.Now.ToString("mm.ss.ff"), isFocusingRight, isClickingRight));
        }

        WriteCSV();
        //passedTime = 0;
        //}
        //StartCoroutine("Wait");
    }

    public void WriteCSV()
    {

        if (myDataList.Count > 0)
        {
            TextWriter tw = new StreamWriter(filePath, false);
            tw.WriteLine("TimeStamp,IsFocusing,IsWatching");
            tw.Close();

            tw = new StreamWriter(filePath, true);

            for (int i = 0; i < myDataList.Count; i++)
            {
                tw.WriteLine(myDataList[i].GetTime() +
                "," + myDataList[i].GetFocusing() +
                "," + myDataList[i].GetClicking());
            }
            tw.Close();

        }

    }

    /*public IEnumerator Wait(){
        myDataList.Add(new DataToCollect(DateTime.Now.ToString("mm.ss.ff"),1,0));
        WriteCSV();
        yield return new WaitForSecondsRealtime(0.1f);
    }*/

    public void isClicking()
    {
        isClickingRight = 1;
    }

}
