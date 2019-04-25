using Clock.ViewModels;

namespace Clock.Infrastucture
{
    class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
         
        }
        
    }
}
