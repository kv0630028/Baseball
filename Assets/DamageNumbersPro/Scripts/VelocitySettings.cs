﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageNumbersPro
{
    [System.Serializable]
    public struct VelocitySettings
    {
        public VelocitySettings(float customDefault)
        {
            minX = -1f;
            maxX = 1f;
            minY = 4f;
            maxY = 5f;

            dragX = 0.1f;
            dragY = 1f;
            gravity = 3f;
        }

        [Header("Velocity:")]
        [Tooltip("Minimum of horizontal velocity.")]
        public float minX;
        [Tooltip("Maximum of horizontal velocity.")]
        public float maxX;
        [Tooltip("Minimum of vertical velocity.")]
        public float minY;
        [Tooltip("Maximum of vertical velocity.")]
        public float maxY;

        [Header("Drag:")]
        [Tooltip("Reduces horizontal velocity over time.")]
        public float dragX;
        [Tooltip("Reduces vertical velocity over time.")]
        public float dragY;

        [Header("Gravity:")]
        [Tooltip("Increases vertical velocity downwards.")]
        public float gravity;
    }
}