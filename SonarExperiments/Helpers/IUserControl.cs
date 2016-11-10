using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarExperiments.Helpers
{
    public interface IUserControl
    {
        ISonarData GetRawData();
    }
}
