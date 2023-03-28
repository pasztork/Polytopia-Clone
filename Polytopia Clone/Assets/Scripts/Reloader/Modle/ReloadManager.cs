using Assets.Scripts.Logger.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReloadManager
    {
        private ReloadManager instance;
        public ReloadManager Instance
        {
            get
            {
                instance ??= new ReloadManager();
                return instance;
            }
        }

        
        public void AddToTroopDic(Identity identity)
        {

        }
    }
}
