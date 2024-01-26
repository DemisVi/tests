using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls.Chrome;
using PackageManager.Models;

namespace PackageManager.Services;

public class SourceNodeProvider
{
    public IEnumerable<SourceNode> GetSourceNodes()
    {
        var fwProvider = new FirmwareProvider();

        FirmwareSources = new FirmwareSourcesProvider().GetSources();

        return /* var topLevelNodes =  */ FirmwareSources.Select(x => new SourceNode()
            {
                Title = x.Name,
                SubNodes = fwProvider.GetFirmware(x).Select(y => new SourceNode()
                {
                    Title = y.ModelName,
                    SubNodes = y.Packages?.Select(z => new SourceNode() { Title = z.VersionName })
                })
            });
    }

    public IEnumerable<FirmwareSource>? FirmwareSources { get; set; }
    public IEnumerable<IEnumerable<Firmware>>? FirmwareCollection { get; set; }
}
