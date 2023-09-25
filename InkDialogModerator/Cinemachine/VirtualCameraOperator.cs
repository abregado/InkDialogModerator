using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class VirtualCameraOperator : MonoBehaviour {
    public string operatorName = "default";
    private CinemachineVirtualCamera _camera;

    private void Awake() {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void MakePrimary() {
        _camera.m_Priority = 10;
    }

    public void MakeSecondary() {
        _camera.m_Priority = 0;
    }
}
