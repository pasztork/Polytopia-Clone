using System.Collections.Generic;
using UnityEngine;

namespace ReplayView
{
    public class TechTreeManager : MonoBehaviour
    {
        private static TechTreeManager instance;
        public static TechTreeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<TechTreeManager>();
                }
                return instance;
            }
        }

        [SerializeField] private List<View.TechTreeItem> techTreeItems = new List<View.TechTreeItem>();
        public List<View.TechTreeItem> TechTreeItems { get => techTreeItems; private set => techTreeItems = value; }

        public Dictionary<string, Model.TechTreeItemBase> GetNewTechTree()
        {
            Dictionary<string, Model.TechTreeItemBase> modelItems = new Dictionary<string, Model.TechTreeItemBase>();
            foreach (var viewItem in TechTreeItems)
            {
                var modelItem = viewItem.ToModel();
                modelItems[modelItem.HashCode] = modelItem;
            }
            return Model.GameManager.Get<Model.TechTreeManagerBase>().ConnectTree(modelItems);
        }
    }
}
