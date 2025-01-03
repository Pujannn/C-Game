using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Zetcil
{

    public class FPSCameraController : MonoBehaviour
    {
        [Header("Main Settings")]
        public Camera TargetCamera;
        public float Sensitivity;

        [Header("Mouse Settings")]
        public KeyCode MouseButton = KeyCode.Mouse1;
        public bool CursorVisible;
        public bool CursorLocked;
        private float X;
        private float Y;

        RaycastHit RaycastResult;

        bool MouseLookActive = true;

        public static bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }
        ///Returns 'true' if we touched or hovering on Unity UI element.
        public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                    return true;
            }
            return false;
        }
        ///Gets all event systen raycast results of current mouse or touch position.
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = CursorVisible;
            Cursor.lockState = CursorLockMode.None;
            if (CursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Vector3 euler = TargetCamera.transform.rotation.eulerAngles;
            X = euler.x;
            Y = euler.y;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.LeftAlt))
            {
                CursorVisible = true;
                Cursor.visible = true;
                MouseLookActive = false;
                Cursor.lockState = CursorLockMode.None;
            }

            if (Input.GetKey(KeyCode.Mouse0) && !IsPointerOverUIElement())
            {
                MouseLookActive = true;
                if (CursorLocked)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }

            if ((Input.GetKey(MouseButton) || MouseButton == KeyCode.None) && MouseLookActive)
            {
                const float minX = 0.0f;
                const float maxX = 360.0f;
                const float minY = -90.0f;
                const float maxY = 90.0f;

                X += Input.GetAxis("Mouse X") * (Sensitivity * Time.deltaTime);
                if (X < minX) X += maxX;
                else if (X > maxX) X -= maxX;
                Y -= Input.GetAxis("Mouse Y") * (Sensitivity * Time.deltaTime);
                if (Y < minY) Y = minY;
                else if (Y > maxY) Y = maxY;

                TargetCamera.transform.rotation = Quaternion.Euler(Y, X, 0.0f);
            }
        }
    }
}
