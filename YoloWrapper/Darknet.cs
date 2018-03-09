using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloWrapper
{
    /// <summary>
    /// Represents a Darknet detector which can be used to analyze multiple image sources
    /// </summary>
    public class Darknet
    {
        private string cfgPath;
        private string weightPath;
        private int gpuID = 0;
        
        /// <summary>
        /// Creates a new instance of the Darknet detector and initializes it
        /// </summary>
        /// <param name="cfgPath"></param>
        /// <param name="weightPath"></param>
        public Darknet(string cfgPath, string weightPath)
        {
            this.cfgPath = cfgPath;
            this.weightPath = weightPath;
            Setup();
        }
        /// <summary>
        /// Creates a new instance of the Darknet detector and initializes it
        /// </summary>
        /// <param name="cfgPath">The *.cfg file</param>
        /// <param name="weightPath">The *.weights file</param>
        /// <param name="gpuID">The index of the CUDA compatible GPU</param>
        public Darknet(string cfgPath, string weightPath, int gpuID) : this(cfgPath, weightPath)
        {
            this.gpuID = gpuID;
            Setup();
        }
        /// <summary>
        /// Used to initialize the detector internally
        /// </summary>
        private void Setup()
        {
            Wrapper.initDetector(cfgPath, weightPath, gpuID=0);
        }
        /// <summary>
        /// Used to detect classes on a cv::Mat object, as a list of NetResults
        /// </summary>
        /// <param name="mat">The frame to analyze</param>
        /// <param name="thresh">Threshold at which a class should be confirmed</param>
        /// <param name="use_mean">Unknonwn parameter</param>
        /// <returns></returns>
        public unsafe List<NetResult> Detect(Mat mat, float thresh = 0.2f, bool use_mean = false)
        {

            Wrapper.detectOpenCV(mat.Data, out var elems, out var resultSize, mat.Rows, mat.Cols, thresh, use_mean);

            List<NetResult> _results = new List<NetResult>(resultSize);
            for(int i=0; i<resultSize; i++)
            {
                _results.Add(new NetResult(elems[i].x, elems[i].y, elems[i].w, elems[i].h, elems[i].prob, elems[i].obj_id, elems[i].track_id, elems[i].frames_counter));
            }

            return _results;
        }

        /// <summary>
        /// Closes the detector
        /// </summary>
        public void Close()
        {
            Wrapper.resetDetector();
        }
    }
}
