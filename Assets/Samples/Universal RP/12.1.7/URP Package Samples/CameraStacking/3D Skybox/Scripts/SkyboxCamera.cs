using UnityEngine;

namespace Assets.Samples.Universal_RP._12._1._7.URP_Package_Samples.CameraStacking._3D_Skybox.Scripts
{
    public class SkyboxCamera : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera m_MainCamera;
        [SerializeField] private float m_SkyboxScale = 1f;

        private Vector3 mainCamStartPos;
        private Vector3 skyboxCamStartPos;

        // Start is called before the first frame update
        void Start()
        {
            if (m_MainCamera == null)
            {
                m_MainCamera = UnityEngine.Camera.main;
            }
            mainCamStartPos = m_MainCamera.transform.position;
            skyboxCamStartPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 mainCamDeltaPos = m_MainCamera.transform.position - mainCamStartPos;
            transform.position = skyboxCamStartPos + mainCamDeltaPos * m_SkyboxScale;

            transform.rotation = m_MainCamera.transform.rotation;
        }
    }
}
