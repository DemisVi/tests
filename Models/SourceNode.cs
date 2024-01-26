using System;
using System.Collections.Generic;

namespace PackageManager.Models;

public class SourceNode
{
    public SourceNode? Parent { get; set; }
    public string Title { get; set; } = string.Empty;
    public IEnumerable<SourceNode>? SubNodes { get; set; }
}
