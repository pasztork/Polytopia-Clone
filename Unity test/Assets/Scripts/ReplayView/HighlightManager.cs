using System.Collections.Generic;
using UnityEngine;

namespace ReplayView
{
    public class HighlightManager : MonoBehaviour
    {
        private static HighlightManager instance;
        public static HighlightManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<HighlightManager>();
                }
                return instance;
            }
        }

        private List<Tile> tilesToHighLight = new List<Tile>();

        public void Add(Tile tile, Color color)
        {
            tile.Highlight(color);
            tilesToHighLight.Add(tile);
        }

        public void Clear()
        {
            foreach (Tile tile in tilesToHighLight)
            {
                tile.ResetColor();
            }
            tilesToHighLight.Clear();
        }
    }
}
