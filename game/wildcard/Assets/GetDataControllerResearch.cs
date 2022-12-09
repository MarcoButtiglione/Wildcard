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
    string filePath;
    [SerializeField] private GameObject objToAnalyze;
    private ResearchManager_1 toAnalyze;
    private int isClickingRight = 0;
    private int isFocusingRight = 0;
    private int currentState = 0;
    public string sceneName;
    private GameObject researchObj;
    private float initializationTime;
    private bool finishedLevel = false;

    public class DataToCollect
    {
        string timeFromStart;
        int isFocusing;
        int isClicking;

        public DataToCollect(string v1, int v2, int v3)
        {
            timeFromStart = v1;
            isFocusing = v2;
            isClicking = v3;
        }
        public string GetTime()
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

    void Awake()
    {
        researchObj = GameObject.Find("ResearchObj");
    }

    // Start is called before the first frame update
    void Start()
    {
        //passedTime = 0f;
        toAnalyze = objToAnalyze.GetComponent<ResearchManager_1>();
        filePath = Application.persistentDataPath + "/Research/Research_Session_" + sceneName + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm") + ".csv";
        initializationTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        //passedTime += Time.fixedDeltaTime;
        //if (passedTime > 0.1f)
        //{
        if (!finishedLevel)
        {
            currentState = toAnalyze.GetCurrentState();
            if (isFocusingRight != 1)
            {
                if (GameObject.Find("ResearchObj").transform.GetChild(currentState).gameObject.GetComponent<FocusController>().getFocused())
                {
                    isFocusingRight = 1;
                }
                else
                {
                    isFocusingRight = 0;
                }
            }

            myDataList.Add(new DataToCollect(TimeSpan.FromSeconds(Time.timeSinceLevelLoad - initializationTime).ToString(@"mm\:ss\.ff"), isFocusingRight, isClickingRight));
            isFocusingRight = 0;
            isClickingRight = 0;
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
            tw.WriteLine("TimeStamp,IsFocusing,IsClicking");
            tw.Close();

            tw = new StreamWriter(filePath, true);

            for (int i = 1; i < myDataList.Count; i++)
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

    public void isFocusing()
    {
        isFocusingRight = 1;
    }
    public void FinishLevel()
    {
        finishedLevel = true;
        isClickingRight = 1;
        isFocusingRight = 1;
        myDataList.Add(new DataToCollect(TimeSpan.FromSeconds(Time.timeSinceLevelLoad - initializationTime).ToString(@"mm\:ss\.ff"), isFocusingRight, isClickingRight));
        WriteCSV();
    }
}
