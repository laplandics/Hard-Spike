using System;
using System.Collections.Generic;

namespace State
{
    [Serializable]
    public class Project
    {
        public Preferences preferences;
        public List<Station> stations;
    }
}