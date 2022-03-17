using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamwork_Hackathon.Extensions
{
public class Source
{
    public string type { get; set; }
    public string id { get; set; }
}

public class Metadata
{
    public bool primary { get; set; }
    public Source source { get; set; }
}

public class Photo
{
    public Metadata metadata { get; set; }
    public string url { get; set; }
}

public class GoogleImageResponse
{
    public string resourceName { get; set; }
    public string etag { get; set; }
    public List<Photo> photos { get; set; }
}
}
