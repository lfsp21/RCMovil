namespace Clock.Interfaces
{
    using System.Globalization;


    public interface ILocalize
    {
        CultureInfo GetCultureInfo();
        void SetLocate(CultureInfo ci);
    }
}
