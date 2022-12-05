using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GetDataControllerResearch : MonoBehaviour
{
    //string filePath = @"D:\Data";
    //string delimiter = ",";
    //The future path of the files
    string filePath = @"D:\Data\";

    public class DataToCollect{
        String timeFromStart;
        int isFocusing;
        int isClicking;

        public DataToCollect(string v1, int v2, int v3)
        {
            timeFromStart = v1;
            isFocusing = v2;
            isClicking = v3;
        }
        public String GetTime(){
            return timeFromStart;
        }
        public int GetFocusing(){
            return isFocusing;
        }
        public int GetClicking(){
            return isClicking;
        }
    }

    public List<DataToCollect> myDataList = new List<DataToCollect>();

    // Start is called before the first frame update
    void Start()
    {
        filePath = filePath +"Research Session " + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".csv";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myDataList.Add(new DataToCollect(DateTime.Now.ToString("mm-ss"),1,0));
        WriteCSV();
    }

    public void WriteCSV(){

        if(myDataList.Count>0){
            TextWriter tw = new StreamWriter(filePath, false);
            tw.WriteLine("TimeStamp,IsFocusing,IsWatching");
            tw.Close();

            tw = new StreamWriter(filePath, true);

            for(int i=0; i<myDataList.Count;i++){
                tw.WriteLine(myDataList[i].GetTime() +
                ","+ myDataList[i].GetFocusing() +
                ","+ myDataList[i].GetClicking());
            }
            tw.Close();

        }

    }
}
