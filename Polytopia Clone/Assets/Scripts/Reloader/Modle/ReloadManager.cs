using System;
using System.Collections.Generic;
using System.Globalization;

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

        
        public void AddToTroopDic(View.Identity identity)
        {

        }
    }
}
