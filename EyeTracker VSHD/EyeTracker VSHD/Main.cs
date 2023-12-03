using System.Numerics;
using System.Runtime.InteropServices;

namespace EyeTracker_VSHD;

class EyeTracker
{

    static void Main()
    {
        ViewPointVSHD eye = new();
        eye.Launch();
        eye.Measure();
        eye.Close();
    }
}
