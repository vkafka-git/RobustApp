using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.LoggerService
{
    public interface ILogger
    {
        //void LogInformation(LogLevels eventLevel, string information, Exception ex = null, params object[] values);
        void LogInformation(string information);
        void LogError(string information, Exception ex);


    }
}
