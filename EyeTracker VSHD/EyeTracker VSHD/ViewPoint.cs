using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;
using System.ComponentModel;

namespace EyeTracker_VSHD;

public class ViewPointVSHD

    /*------------------------------------------------------------------------------------
       ViewPoint class, creates a ViewPoint communications object for communication
       between VSHD, ViewPoint and your computer.
    /------------------------------------------------------------------------------------*/
{



    // VPX constants
    
    const string dllPath = "C:\\Program Files\\295311-USB-60x3-x64-SilverBox\\VPX_InterApp_64.dll";
    const string appPath = "C:\\Program Files\\295311-USB-60x3-x64-SilverBox\\ViewPoint-USB-60x3_64.exe";
    const string settingsFilePupil = "C:\\Program Files\\295311-USB-60x3-x64-SilverBox\\Settings\\SettingsAccuracyAndPrecisionPupil.txt";
    const string settingsFilePupilGlint = "C:\\Programs Files\\295311-USB-60x3-x64-SilverBox\\Settings\\SettingsAccuracyAndPrecisionGlintPupil.txt";
    const string imagePath = "C:\\Users\\Administrator\\Documents\\PrecisionAndAccuracy\\StimulusPoints_bmp\\";
    string dataFile = "";
    string tracking = "";

    float[] gazePointA = new float[] { 1.1f, 1.1f };
    float[] gazePointB = new float[] { 1.1f, 1.1f };
    float[] stimulusPoint = new float[] { 1.1f, 1.1f };

    public float gpA_X = 0; //GazePoint Eye A x coordinate. 0, when nothing is read
    public float gpA_Y = 0; // y coordinate
    public float gpB_X = 0; //GazePoint Eye B x coordinate.
    public float gpB_Y = 0; // y coordinate
    public int EyeCode = 0;


    int EYE_A = 0;
    int EYE_B = 1;
    int VPX_STATUS_ViewPointIsRunning = 1;

    int ROI_NO_EVENT = -9999;



    /*------------------------------------------------------------------------------------
    	FUNCTIONS FROM VPX

        do not call these
    /------------------------------------------------------------------------------------*/
    [DllImport(dllPath)]
    public static extern int VPX_LaunchApp(string appNameArg, string cmdLineArg);
    [DllImport(dllPath)]
    public static extern int VPX_GetStatus();
    [DllImport(dllPath)]
    private static extern int VPX_SendCommand(string cmdLineArg);
    [DllImport(dllPath)]
    private static extern int VPX_GetGazePoint2(int Eye, float[] gp);
    [DllImport(dllPath)]
    private static extern int VPX_GetStatus(int operation);
    [DllImport(dllPath)]
    private static extern int VPX_GetCalibrationStimulusPoint2(int EYE, int ixPt, float[] sp);
    [DllImport(dllPath)]
    private static extern int VPX_GetDataQuality2(int EYE, int EyeCode);



    /*------------------------------------------------------------------------------------
        FUNCTIONS FROM VPX to call
    /------------------------------------------------------------------------------------*/
    public void Launch() //Opens the recording and prepares for measuring
    {
        
        Thread.Sleep(5000);  // Let viewpoint start up before sending other commands
        Console.WriteLine("Enter file name: ");
        string name = Console.ReadLine();
        dataFile = name + ".txt";
        
        Thread.Sleep(1000);
        VPX_SendCommand("setStimulusDisplay 1");
        VPX_SendCommand("stimulusGraphicsOptions -POG");
    }
    

    public void Close() //Closes the recordings
    {

        VPX_SendCommand("dataFile_Close");
        VPX_SendCommand("eventsFile_close");
        VPX_SendCommand("setWindow STIMULUS HIDE");
    
    }


   
 

    public void ShowCalibrationPoint(int point)
    { 
        VPX_SendCommand($"calibrationRedoPoint {point}");
    }

    public void InsertMarker(string L)
    {
        VPX_SendCommand($"dataFile_InsertMarker {L}");
    }

   
   
    public void Measure() //Opens stimulus window and shows 16 points while recording to file specified in Launch()
    {

        
        Thread.Sleep(2000);
        VPX_SendCommand($"dataFile_NewName '{dataFile}' ");
        VPX_SendCommand($"eventsFile_NewName '{dataFile}events' ");
        VPX_SendCommand("setStimulusDisplay 1");
        VPX_SendCommand("setWindow STIMULUS SHOW");
        List<string> Letters = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
        Thread.Sleep(7000);
        for (int i = 1; i < 17; i++)
        {

            VPX_SendCommand($"stimulus_LoadImageFile '{imagePath + $"{i}_96.bmp"}' ");
            VPX_SendCommand("stimulus_ImageShape Fit");
            string l = Letters[i - 1];
            InsertMarker(l);
            Thread.Sleep(3000);
        }

        


    }

}
