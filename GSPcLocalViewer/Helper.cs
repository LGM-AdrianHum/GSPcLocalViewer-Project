// NAME : Adrian Hum (Casandra)
// FILE : GSPcLocalViewer/GSPcLocalViewer [Helper.cs]
// ----------------------------------------------------------------------------------------------------------
// Created  :  2018-08-06  12:03 PM
// Modified :  2018-08-06  12:03 PM
// ----------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
    public static class Helper
    {
        public static void InvokeIfRequired(this ISynchronizeInvoke obj,
            MethodInvoker action)
        {
            if (obj.InvokeRequired)
            {
                var args = new object[0];
                obj.Invoke(action, args);
            }
            else
            {
                action();
            }
        }
    }
}