using System;

namespace Wrench.Services
{
    public class AdapterSensorEventArgs : EventArgs
    {
        public AdapterSensorEventArgs(bool adapterState) => AdapterState = adapterState;

        public bool AdapterState { get; private set; }
    }
}