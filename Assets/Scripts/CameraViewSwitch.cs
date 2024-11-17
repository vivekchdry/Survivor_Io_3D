using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraViewSwitch : MonoBehaviour
    {
        [SerializeField] private Transform isometricView;
        [SerializeField] private Transform topDownView;

        [ProButton]
        public void SwitchToTopDownView()
        {
            transform.position = topDownView.position;
            transform.rotation = topDownView.rotation;
        }
        [ProButton]
        public void SwitchToIsometricView()
        {
            transform.position = isometricView.position;
            transform.rotation = isometricView.rotation;
        }
        
        [ProButton]
        public void SwitchCameraProjection()
        {
            if (Camera.main.orthographic)
            {
                Camera.main.orthographic = false;
            }
            else
            {
                Camera.main.orthographic = true;
            }
        }
    }
}