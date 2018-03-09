using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloWrapper
{
    public class NetResult
    {
        public uint x { get; set; }
        public uint y {get; set;}
        public uint w { get; set; }
        public uint h { get; set; }    // (x,y) - top-left corner, (w, h) - width & height of bounded box
        public float prob;                 // confidence - probability that the object was found correctly
        public uint obj_id;        // class of object - from range [0, classes-1]
        public uint track_id;      // tracking id for video (0 - untracked, 1 - inf - tracked object)
        public uint frames_counter;// counter of frames on which the object was detected

        public NetResult(uint x, uint y, uint w, uint h, float prob, uint obj_id, uint track_id, uint frames_counter)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.prob = prob;
            this.obj_id = obj_id;
            this.track_id = track_id;
            this.frames_counter = frames_counter;
        }
    }
}
