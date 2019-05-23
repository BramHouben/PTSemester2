using Model;

namespace Data.Interfaces
{
    public interface IMedewerkerContext
    {
        Medewerker GetMedewerkerId(string id);
    }
}
