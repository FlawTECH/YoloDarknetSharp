using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YoloWrapper
{
    public static class Wrapper
    {
        public struct bbox_t
        {
            public uint x, y, w, h;    // (x,y) - top-left corner, (w, h) - width & height of bounded box
            public float prob;                 // confidence - probability that the object was found correctly
            public uint obj_id;        // class of object - from range [0, classes-1]
            public uint track_id;      // tracking id for video (0 - untracked, 1 - inf - tracked object)
            public uint frames_counter;// counter of frames on which the object was detected
        };


        [DllImport(@"YoloCppAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void initDetector(string cfgFile, string weightFile, int gpuId = 0);

        [DllImport(@"YoloCppAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void resetDetector();

        [DllImport(@"YoloCppAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe bbox_t* detect(string image_filename, int* arraySize, float thresh = 0.2f, bool use_mean = false);
        [DllImport(@"YoloCppAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void detectOpenCV(IntPtr matPtr, out bbox_t* elems, out int arraySize, int matRows, int matCols, float thresh = 0.2f, bool use_mean = false);


    }
}
