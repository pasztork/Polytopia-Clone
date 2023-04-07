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

        [SerializeField] private SerializableDictionary<string, View.TechTreeItem> techTreeItems = new();
        public SerializableDictionary<string, View.TechTreeItem> TechTreeItems { get => techTreeItems; private set => techTreeItems = value; }

        public Dictionary<string, Model.TechTreeItemBase> GetNewTechTree()
        {
            Dictionary<string, Model.TechTreeItemBase> modelItems = new Dictionary<string, Model.TechTreeItemBase>();
            foreach (var viewItem in TechTreeItems)
            {
                var modelItem = viewItem.Value.ToModel();
                modelItems[modelItem.HashCode] = modelItem;
            }
            return Model.GameManager.Get<Model.TechTreeManagerBase>().ConnectTree(modelItems);
        }

        public void LearnTech(string techName)
        {
            Controller.GameManager.Get<Controller.TechTreeManagerBase>().UnlockTech(TechTreeItems[techName].ToModel());
        }
    }
}
