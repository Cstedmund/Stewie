using UnityEngine;
using UnityEngine.EventSystems;

namespace CnControls
{
    /// Simple button class
    /// Handles press, hold and release, like a normal button
    public class SimpleButton : MonoBehaviour
        // some weird stuff here
        // we have to support Unity Remote with Multi Touch (which is not currently supported with uGUI)
        // so we just completely override the input system for the Editor, making it behave like it would normally do in builds
#if !UNITY_EDITOR
        , IPointerUpHandler, IPointerDownHandler
#endif
    {
        /// The name of the button
        public string ButtonName = "switch";

        /// Utility object that is registered in the system
        private VirtualButton _virtualButton;

        // if we are in the editor, add an input helper component
#if UNITY_EDITOR
        private void Awake()
        {
            gameObject.AddComponent<ButtonInputHelper>();
        }
#endif
        
        /// When we enable, we register our button in the input system
        private void OnEnable()
        {
            _virtualButton = _virtualButton ?? new VirtualButton(ButtonName);
            CnInputManager.RegisterVirtualButton(_virtualButton);
        }

        /// When we disable, we unregister our button
        private void OnDisable()
        {
            CnInputManager.UnregisterVirtualButton(_virtualButton);
        }

        /// uGUI Event system stuff
        /// It's also utilised by the editor input helper
        /// <param name="eventData">Data of the passed event</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            _virtualButton.Release();
        }

        /// uGUI Event system stuff
        /// It's also utilised by the editor input helper
        /// <param name="eventData">Data of the passed event</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            _virtualButton.Press();
        }
    }
}