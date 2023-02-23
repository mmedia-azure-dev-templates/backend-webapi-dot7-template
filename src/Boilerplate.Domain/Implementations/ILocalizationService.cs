using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Implementations;
public interface ILocalizationService
{
    public LocalizedString GetLocalizedHtmlString(string key);
}
