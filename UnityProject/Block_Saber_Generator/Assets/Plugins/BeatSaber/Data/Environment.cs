using System;
using UnityEngine;

namespace BeatSaber.Data
{
    /// <summary> Environment data </summary>
    [Serializable]
    public class Environment
    {
        /// <summary> Environment name </summary>
        [SerializeField] private string _id;
        /// <summary> Look up method </summary>
        [SerializeField] private string _lookupMethod;
        /// <summary> Is active </summary>
        [SerializeField] private bool _active;


        /// <inheritdoc cref="_id" />
        public string Id { get => _id; set => _id = value; }

        /// <inheritdoc cref="_lookupMethod" />
        public string LookupMethod { get => _lookupMethod; set => _lookupMethod = value; }

        /// <inheritdoc cref="_active" />
        public bool Active { get => _active; set => _active = value; }
    }
}
