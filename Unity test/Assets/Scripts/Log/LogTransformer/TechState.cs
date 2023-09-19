using Model;
using System;

namespace LogView.LogTransformer
{
    public class TechState
    {
        public string Name { get; set; } = String.Empty;
        public CostState CostState { get; set; } = new();
    }
}
