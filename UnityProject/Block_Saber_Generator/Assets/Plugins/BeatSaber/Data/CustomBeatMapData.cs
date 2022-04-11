using System;
using UnityEngine;

namespace BeatSaber.Data
{
    [Serializable]
    public class CustomBeatMapData
    {
        /// <summary> Color of left beat </summary>
        [SerializeField] private CustomColor _colorLeft;
        /// <summary> Color of right beat </summary>
        [SerializeField] private CustomColor _colorRight;
        /// <summary> Left environment color </summary>
        [SerializeField] private CustomColor _envColorLeft;
        /// <summary> Right environment color </summary>
        [SerializeField] private CustomColor _envColorRight;
        /// <summary> Left environment color boost </summary>
        [SerializeField] private CustomColor _envColorLeftBoost;
        /// <summary> Right environment color boost </summary>
        [SerializeField] private CustomColor _envColorRightBoost;
        /// <summary> Obstacle color </summary>
        [SerializeField] private CustomColor _obstacleColor;
        /// <summary> Custom label for this difficulty </summary>
        [SerializeField] private string _difficultyLabel;
        /// <summary> Level warnings </summary>
        [SerializeField] private string[] _warnings;
        /// <summary> Other infomation </summary>
        [SerializeField] private string[] _information;
        /// <summary> Level suggestions </summary>
        [SerializeField] private string[] _suggestions;
        /// <summary> Level requirements </summary>
        [SerializeField] private string[] _requirements;
        /// <summary> Editor offset </summary>
        [SerializeField] private float _editorOffset;
        /// <summary> Old editor offset </summary>
        [SerializeField] private float _editorOldOffset;


        /// <inheritdoc cref="_colorLeft" />
        public CustomColor ColorLeft { get => _colorLeft; set => _colorLeft = value; }

        /// <inheritdoc cref="_colorRight" />
        public CustomColor ColorRight { get => _colorRight; set => _colorRight = value; }

        /// <inheritdoc cref="_envColorLeft" />
        public CustomColor EnvColorLeft { get => _envColorLeft; set => _envColorLeft = value; }

        /// <inheritdoc cref="_envColorRight" />
        public CustomColor EnvColorRight { get => _envColorRight; set => _envColorRight = value; }

        /// <inheritdoc cref="_envColorLeftBoost" />
        public CustomColor EnvColorLeftBoost { get => _envColorLeftBoost; set => _envColorLeftBoost = value; }

        /// <inheritdoc cref="_envColorRightBoost" />
        public CustomColor EnvColorRightBoost { get => _envColorRightBoost; set => _envColorRightBoost = value; }

        /// <inheritdoc cref="_obstacleColor" />
        public CustomColor ObstacleColor { get => _obstacleColor; set => _obstacleColor = value; }

        /// <inheritdoc cref="_difficultyLabel" />
        public string DifficultyLabel { get => _difficultyLabel; set => _difficultyLabel = value; }

        /// <inheritdoc cref="_warnings" />
        public string[] Warnings { get => _warnings; set => _warnings = value; }

        /// <inheritdoc cref="_information" />
        public string[] Information { get => _information; set => _information = value; }

        /// <inheritdoc cref="_suggestions" />
        public string[] Suggestions { get => _suggestions; set => _suggestions = value; }

        /// <inheritdoc cref="_requirements" />
        public string[] Requirements { get => _requirements; set => _requirements = value; }

        /// <inheritdoc cref="_editorOffset" />
        public float EditorOffset { get => _editorOffset; set => _editorOffset = value; }

        /// <inheritdoc cref="_editorOldOffset" />
        public float EditorOldOffset { get => _editorOldOffset; set => _editorOldOffset = value; }
    }
}
