using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        private float m_tilt;

        private void Update()
        {
            float mouseRotation = Input.GetAxis("Mouse Y");
            m_tilt = Mathf.Clamp(m_tilt - mouseRotation, -15f, 15f);
            
            transform.localRotation = Quaternion.Euler(m_tilt, 0 ,0);
        }
    }