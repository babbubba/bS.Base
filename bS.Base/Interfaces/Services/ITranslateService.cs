using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bS.Base.Interfaces.Services
{
    public interface ITranslateService
    {
        string Translate(string text);

        string Translate(string text, params object[] objs);
    }
}
