using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Joystick : Terminus
    {
        Vector3 value;
        const string variableName = "leverCoord";
        public float leverLength { get; set; }
        public Transform pivotPoint { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            value = Vector3.zero;
        }

        public void SetPivot(Transform pivot)
        {
            pivotPoint = pivot;
        }

        public void SetLength(float length)
        {
            leverLength = length;
        }

        public void Log()
        {
            //Logging system
            if (GlobalLogger.isLogging == true)
            {
                GlobalLogger.addLogDataOnce(new GlobalLogger.LogDataFormat(name, GlobalLogger.DataType.VEC3, value));
            }
        }

        public override Transform Control(Transform terminus, Vector3 contactPoint, Quaternion controlRotation, bool givenFromDesktop = false) {
            if (givenFromDesktop)
            {
                //value += new Vector3(controlVector.x, controlVector.y, 0);
                Vector3 valueRotate = new Vector3(contactPoint.x, contactPoint.y, 0);

                value += new Vector3(contactPoint.x, contactPoint.y, 0);
                
                if (value.x > -87 && value.x < 87 && value.y > -87 && value.y < 87 )
                {
                    //terminus.transform.position = pivotPoint.transform.position + ((Quaternion.AngleAxis(-value.x + 90, Vector3.forward) * Vector3.right + Quaternion.AngleAxis(value.y - 90, Vector3.right) * Vector3.forward) - Vector3.up) * leverLength; //(Quaternion.AngleAxis(value.y - 90, Vector3.right))) * Vector3.right  * leverLength ;
                    //.rotation =  Quaternion.AngleAxis(value.y, Vector3.right) * Quaternion.AngleAxis(-value.x, Vector3.forward);

                    // modify rotation method
                    terminus.transform.RotateAround(pivotPoint.position, Vector3.right, valueRotate.y);
                    terminus.transform.RotateAround(pivotPoint.position, Vector3.forward, -valueRotate.x);
                    
                }
                Send();

                return terminus;
            }
            else
            {
                Vector3 projected = (contactPoint - pivotPoint.transform.position);
                //Debug.Log("projected = [" + projected.x + ", " + projected.y + ", " + projected.z + "]");
                float angleX = -Vector3.Angle(projected, Vector3.forward) + 90;
                float angleY = Vector3.Angle(projected, Vector3.right) - 90;
                if (angleX > -87 && angleX < 87 && angleY > -87 && angleY < 87)
                {
                    // terminus.transform.position = pivotPoint.transform.position + projected.normalized * leverLength;
                    terminus.transform.rotation = Quaternion.AngleAxis(angleX, Vector3.right) * Quaternion.AngleAxis(angleY, Vector3.forward);

                    // modify rotation method(Not tested in VR)
                    terminus.transform.RotateAround(pivotPoint.position, Vector3.right, angleX);
                    terminus.transform.RotateAround(pivotPoint.position, Vector3.forward, angleY);

                    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);
                }
                value = new Vector3(-angleY, angleX);
                Send();

                return terminus;
            }            
        }

        public override void Send()
        {
            ConditionPublisher.Instance.Send(new VectorVariableState(this,variableName,value));
        }

        public override string ToString()
        {
            return base.ToString() + ": Value (" + (int)(value.x) + ", " + (int)(value.y) + ", " + (int)(value.z) + ")";
        }

        private void Start()
        {
            Initialize();
        }
    }
}
