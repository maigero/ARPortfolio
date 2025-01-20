using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


//sgs9:
//->1080p:  1846.012 | 1851.736 | 945.903 | 596.237
//->720p:  1236.828 | 1240.663 | 633.755 | 399.4788

namespace CUAS
{
    /// <summary>
    /// Interface for video sources (frame and calibration data)
    /// </summary>
    public interface IVideoSource
    {

        public Texture2D GetFrame();

        public Vector2 GetFocalLengths();

        public Vector2 GetPrincipalPoint();

        public Vector4 GetDistortion();

    }

}
