using DVDLibrary.Data.Interfaces;
using DVDLibrary.Data.Repositories;
using System;

namespace DVDLibrary.Data
{
    public class RepositoryFactory
    {
        private static string _repositoryType = Settings.GetRepositoryType();

        public static IDvdRepository GetRepository()
        {
            switch(_repositoryType)
            {
                case "Mock":
                    return new MockRepository();
                case "ADO":
                    return new ADORepository();
                case "DP":
                    return new DPRepository();
                case "EF":
                    return new EFRepository();
                default:
                    throw new Exception("A valid repository could not be found.");
            }
        }
    }
}
