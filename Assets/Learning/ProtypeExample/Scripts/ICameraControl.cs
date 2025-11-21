using UnityEngine;

namespace Learning.Prototype {
    public interface ICameraControl {
        Camera CameraRefReference { get;}
        float MoveSpeed { get; }
        float LookSpeed { get; }
    }
}
