using System.Collections.Generic;
using UnityEngine;

namespace ReplayView
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        public Vector3 Offset { get => offset; private set => offset = value; }
        public List<Tile> Neighbors { get; } = new List<Tile>();

        public Color StartColor { get; private set; }
        public Color TileColor
        {
            get => GetComponent<Renderer>().material.color;
            set => GetComponent<Renderer>().material.color = value;
        }

        private void Awake()
        {
            StartColor = TileColor;
        }
    }
}
