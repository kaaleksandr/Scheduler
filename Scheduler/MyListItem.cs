using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    class MyListItem<T>
    {
        public string DisplayString
        {
            get;
            set;
        }

        public T Tag
        {
            get;
            set;
        }

        public override string ToString()
        {
            return DisplayString;
        }
    }
}
