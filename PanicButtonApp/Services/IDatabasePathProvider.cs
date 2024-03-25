using System;
using System.Collections.Generic;
using System.Text;

namespace PanicButtonApp.Services
{
    internal interface IDatabasePathProvider
    {
        string GetDatabasePath(string filename);

    }
}
