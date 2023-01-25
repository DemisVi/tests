using System;

public enum AdapterSensorState
{
    Close,
    Open,
}

    public class AdapterSensorEventArgs : EventArgs
    {
        public AdapterSensorEventArgs(bool sensorState) => SensorState = sensorState;

        public bool SensorState { get; private set; }
    }