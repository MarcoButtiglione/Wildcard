using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GetDataControllerStory : MonoBehaviour
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
    private int numOfObjects;
    public string sceneName;
    private GameObject researchObj;
    private List<bool> objectsSeen;
    private float initializationTime;

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
        numOfObjects = researchObj.transform.childCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        //passedTime = 0f;
        toAnalyze = objToAnalyze.GetComponent<ResearchManager_1>();
        filePath = Application.persistentDataPath + "/Research/Research_Session_" + sceneName + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm") + ".csv";
        objectsSeen = new List<bool>(researchObj.transform.childCount);
        initializationTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        //passedTime += Time.fixedDeltaTime;
        //if (passedTime > 0.1f)
        //{


        currentState = toAnalyze.getCurrentState();
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

        if (isClickingRight == 1)
        {
            myDataList.Add(new DataToCollect((Time.timeSinceLevelLoad-initializationTime).ToString("mm.ss.ff"), isFocusingRight, isClickingRight));
            if (isFocusingRight == 1 && currentState == numOfObjects - 1)
            {
                WriteCSV();
            }
            isFocusingRight = 0;
            isClickingRight = 0;
        }
        else
        {
            myDataList.Add(new DataToCollect((Time.timeSinceLevelLoad-initializationTime).ToString("mm.ss.ff"), isFocusingRight, isClickingRight));
            isFocusingRight = 0;
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

    public void isFocusing()
    {
        isFocusingRight = 1;
    }
}
