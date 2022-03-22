using UnityEngine;

namespace BeatSaber.Data
{
    public class MapEditor
    {
        /// <summary> Last edited by </summary>
        [SerializeField] private string _lastEditedBy;


        /// <inheritdoc cref="_lastEditedBy" />
        public string LastEditedBy { get => _lastEditedBy; set => _lastEditedBy = value; }
    }
}
