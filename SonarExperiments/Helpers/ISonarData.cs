using System.Collections.Generic;

namespace SonarExperiments.Helpers
{
    public interface ISonarData
    {
        List<string> HeaderInfo();
        List<string> BodyInfo();
    }
}
