using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CustomToggleGroup : UIBehaviour
    {
        [SerializeField] private bool m_AllowSwitchOff = false;
       
        public bool allowSwitchOff { get { return m_AllowSwitchOff; } set { m_AllowSwitchOff = value; } }

        public List<Toggle> toggles = new List<Toggle>();

        protected override void Start()
        {
            EnsureValidState();
            base.Start();
            for(int i = 0; i < toggles.Count; i++)
            {
            //toggles[i].onValueChanged += 
            }
        }

        protected override void OnEnable()
        {
            EnsureValidState();
            base.OnEnable();
        }

        private void ValidateToggleIsInGroup(Toggle toggle)
        {
            if (toggle == null || !toggles.Contains(toggle))
                throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", new object[] { toggle, this }));
        }

        /// <summary>
        /// Notify the group that the given toggle is enabled.
        /// </summary>
        /// <param name="toggle">The toggle that got triggered on.</param>
        /// <param name="sendCallback">If other toggles should send onValueChanged.</param>
        public void NotifyToggleOn(Toggle toggle, bool sendCallback = true)
        {
            ValidateToggleIsInGroup(toggle);
            // disable all toggles in the group
            for (var i = 0; i < toggles.Count; i++)
            {
                if (toggles[i] == toggle)
                    continue;

                if (sendCallback)
                    toggles[i].isOn = false;
                else
                    toggles[i].SetIsOnWithoutNotify(false);
            }
        }

        /// <summary>
        /// Unregister a toggle from the group.
        /// </summary>
        /// <param name="toggle">The toggle to remove.</param>
        public void UnregisterToggle(Toggle toggle)
        {
            if (toggles.Contains(toggle))
                toggles.Remove(toggle);
        }

        /// <summary>
        /// Register a toggle with the toggle group so it is watched for changes and notified if another toggle in the group changes.
        /// </summary>
        /// <param name="toggle">The toggle to register with the group.</param>
        public void RegisterToggle(Toggle toggle)
        {
            if (!toggles.Contains(toggle))
                toggles.Add(toggle);
        }

        /// <summary>
        /// Ensure that the toggle group still has a valid state. This is only relevant when a ToggleGroup is Started
        /// or a Toggle has been deleted from the group.
        /// </summary>
        public void EnsureValidState()
        {
            if (!allowSwitchOff && !AnyTogglesOn() && toggles.Count != 0)
            {
                toggles[0].isOn = true;
                NotifyToggleOn(toggles[0]);
            }

            IEnumerable<Toggle> activeToggles = ActiveToggles();

            if (activeToggles.Count() > 1)
            {
                Toggle firstActive = GetFirstActiveToggle();

                foreach (Toggle toggle in activeToggles)
                {
                    if (toggle == firstActive)
                    {
                        continue;
                    }
                    toggle.isOn = false;
                }
            }
        }

        /// <summary>
        /// Are any of the toggles on?
        /// </summary>
        /// <returns>Are and of the toggles on?</returns>
        public bool AnyTogglesOn()
        {
            return toggles.Find(x => x.isOn) != null;
        }

        /// <summary>
        /// Returns the toggles in this group that are active.
        /// </summary>
        /// <returns>The active toggles in the group.</returns>
        /// <remarks>
        /// Toggles belonging to this group but are not active either because their GameObject is inactive or because the Toggle component is disabled, are not returned as part of the list.
        /// </remarks>
        public IEnumerable<Toggle> ActiveToggles()
        {
            return toggles.Where(x => x.isOn);
        }

        /// <summary>
        /// Returns the toggle that is the first in the list of active toggles.
        /// </summary>
        /// <returns>The first active toggle from m_Toggles</returns>
        /// <remarks>
        /// Get the active toggle for this group. As the group
        /// </remarks>
        public Toggle GetFirstActiveToggle()
        {
            IEnumerable<Toggle> activeToggles = ActiveToggles();
            return activeToggles.Count() > 0 ? activeToggles.First() : null;
        }

        /// <summary>
        /// Switch all toggles off.
        /// </summary>
        /// <remarks>
        /// This method can be used to switch all toggles off, regardless of whether the allowSwitchOff property is enabled or not.
        /// </remarks>
        public void SetAllTogglesOff(bool sendCallback = true)
        {
            bool oldAllowSwitchOff = m_AllowSwitchOff;
            m_AllowSwitchOff = true;

            if (sendCallback)
            {
                for (var i = 0; i < toggles.Count; i++)
                    toggles[i].isOn = false;
            }
            else
            {
                for (var i = 0; i < toggles.Count; i++)
                    toggles[i].SetIsOnWithoutNotify(false);
            }

            m_AllowSwitchOff = oldAllowSwitchOff;
        }
    }
