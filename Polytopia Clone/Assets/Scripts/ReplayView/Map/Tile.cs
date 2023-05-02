using System.Collections.Generic;
using UnityEngine;

namespace ReplayView
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        public Vector3 Offset { get => offset; private set => offset = value; }
        public List<Tile> Neighbors { get; } = new List<Tile>();

        protected Color startColor;
        protected Color CurrentColor
        {
            get => GetComponent<Renderer>().material.color;
            set => GetComponent<Renderer>().material.color = value;
        }

        public void Awake()
        {
            startColor = CurrentColor;
        }

        public void Highlight(Color color)
        {
            CurrentColor = color;
        }

        public void ResetColor()
        {
            CurrentColor = startColor;
        }
    }
}
