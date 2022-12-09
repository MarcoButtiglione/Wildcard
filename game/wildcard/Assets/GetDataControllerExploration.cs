using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GetDataControllerExploration : MonoBehaviour
{
    //string filePath = @"D:\Data";
    //string delimiter = ",";
    //The future path of the files
    //private float passedTime = 0f;
    string filePath;
    private int isPointingRight = 0;
    private int isFocusingRight = 0;
    public string sceneName;
    private float initializationTime;
    private bool levelFinished = false;
    private bool canEnterFocus = false;
    private GameObject pictures;

    public class DataToCollect
    {
        string timeFromStart;
        int isFocusing;
        int isPointing;

        public DataToCollect(string v1, int v2, int v3)
        {
            timeFromStart = v1;
            isFocusing = v2;
            isPointing = v3;
        }
        public string GetTime()
        {
            return timeFromStart;
        }
        public int GetFocusing()
        {
            return isFocusing;
        }
        public int GetPointing()
        {
            return isPointing;
        }
    }

    public List<DataToCollect> myDataList = new List<DataToCollect>();

    private void Awake()
    {
        pictures = GameObject.Find("Picture");
    }

    // Start is called before the first frame update
    void Start()
    {
        //passedTime = 0f;
        filePath = Application.persistentDataPath + "/Exploration/Exploration_Session_Binary_" + sceneName + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm") + ".csv";
        initializationTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        //passedTime += Time.fixedDeltaTime;
        //if (passedTime > 0.1f)
        //{



        if (!levelFinished)
        {
            for (int i = 0; i < pictures.transform.childCount; i++)
            {
                if (pictures.transform.GetChild(i).gameObject.GetComponent<FocusController>().getFocused())
                {
                    canEnterFocus = true;
                }
            }

            if (canEnterFocus)
            {
                isFocusingRight = 1;
            }
            else
            {
                isFocusingRight = 0;
            }
            //TimeSpan.FromSeconds(Time.timeSinceLevelLoad - initializationTime).ToString(@"%s\.ff");

            myDataList.Add(new DataToCollect(TimeSpan.FromSeconds(Time.timeSinceLevelLoad - initializationTime).ToString(@"mm\:ss\.ff"), isFocusingRight, isPointingRight));
            isFocusingRight = 0;
            canEnterFocus = false;

        }

        //WriteCSV();
        //passedTime = 0;
        //}
        //StartCoroutine("Wait");
    }

    public void WriteCSV()
    {

        if (myDataList.Count > 0)
        {
            TextWriter tw = new StreamWriter(filePath, false);
            tw.WriteLine("TimeStamp,IsFocusing,IsPointing");
            tw.Close();

            tw = new StreamWriter(filePath, true);

            for (int i = 1; i < myDataList.Count; i++)
            {
                tw.WriteLine(myDataList[i].GetTime() +
                "," + myDataList[i].GetFocusing() +
                "," + myDataList[i].GetPointing());
            }
            tw.Close();

        }

    }

    /*public IEnumerator Wait(){
        myDataList.Add(new DataToCollect(DateTime.Now.ToString("mm.ss.ff"),1,0));
        WriteCSV();
        yield return new WaitForSecondsRealtime(0.1f);
    }*/

    public void IsPointing()
    {
        isPointingRight = 1;
    }
    public void IsNotPointing()
    {
        isPointingRight = 0;
    }

    public void FinishLevel()
    {
        levelFinished = true;
        isFocusingRight = 1;
        isPointingRight = 1;
        myDataList.Add(new DataToCollect(TimeSpan.FromSeconds(Time.timeSinceLevelLoad - initializationTime).ToString(@"mm\:ss\.ff"), isFocusingRight, isPointingRight));
        WriteCSV();
    }
}
