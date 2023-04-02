namespace View
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
    }
}
