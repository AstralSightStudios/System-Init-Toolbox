using System;
using System.Windows.Threading;
using System.Security.Permissions;

//  立即退出程序 Environment.Exit(0);

namespace System_Init_Toolbox
{
    public static class DispatcherHelper
    {
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrames), frame);
            try 
            { 
                Dispatcher.PushFrame(frame); 
            }
            catch (InvalidOperationException) 
            {

            }
        }
        private static object ExitFrames(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }
}