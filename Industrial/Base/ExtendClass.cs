using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial.Base
{
    /// <summary>
    /// The extend class.
    /// </summary>
    public static class ExtendClass
    {
        public static float ByteArrsyToFloat(this byte[] value)
        {
            float fValue = 0f;
            uint nRest = ((uint)value[2]) * 256
                + ((uint)value[3]) +
                65536 * (((uint)value[0]) * 256 + ((uint)value[1]));
            unsafe
            {
                float* ptemp;
                ptemp = (float*)(&nRest);
                fValue = *ptemp;
            }
            return fValue;
        }
    }
}
