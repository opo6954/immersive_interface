using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ViewpointRecorder : MonoBehaviour
{
    public Transform viewCamera;
    public Tasc.PlatformSelector ps;

    public string dataStack="";
    public float saveFreq = 0.05f;
    public float prevTime=0;
    public int timeIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("Actor-PlatformSelector").GetComponent<Tasc.PlatformSelector>();
        string modeStr = "";

        if (ps.currentPlatform == Tasc.Platform.VRHTCVive)
        {
            viewCamera = GameObject.Find("VRCamera").transform;
            modeStr = "VR";
        }
        else if (ps.currentPlatform == Tasc.Platform.Desktop)
        {
            viewCamera = GameObject.Find("RigidBodyFPSController").transform;
            modeStr = "Desktop";
        }
        prevTime = Time.time;

        dataStack = modeStr + "\nTime slot, time value, cameraPosX, cameraPosY, cameraPosZ, cameraRotX, cameraRotY, cameraRotZ\n";
    }

    void loggingOneLine(int timeIdx, float timevalue, Transform viewCamera)
    {
        Vector3 cameraRot = viewCamera.rotation.eulerAngles;
        dataStack = dataStack + timeIdx.ToString() + "," + Time.time.ToString() + ",";
        dataStack = dataStack + viewCamera.position.x.ToString() + "," + viewCamera.position.y.ToString() + "," + viewCamera.position.z.ToString() + ",";
        dataStack = dataStack + cameraRot.x.ToString() + "," + cameraRot.y.ToString() + "," + cameraRot.z.ToString() + "\n";
    }

    void saveLogging()
    {
        string fileName = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        fileName = "recording/" + fileName + ".csv";

        FileStream f = new FileStream(fileName, FileMode.Append, FileAccess.Write);

        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);

        writer.Write(dataStack);

        writer.Close();
    }

    private void OnDestroy()
    {
        saveLogging();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.time - prevTime;

        if (deltaTime > saveFreq)
        {
            loggingOneLine(timeIdx, Time.time, viewCamera);

            timeIdx += 1;
            prevTime = Time.time;
        }

    }
}
