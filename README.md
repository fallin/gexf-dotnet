# gexf-dotnet

A .NET/C# library to generate [GEXF](https://gephi.org/gexf/format/) (Graph Exchange XML Format) files

## Getting Started

Install using the NuGet Package Manager

```
PM> Install-Package gexf-dotnet
```

### Example Usage

```c#
void Main()
{
	var dragon = new Location(1, 40.589649m, -105.045624m, "Horse & Dragon Brewing Company");
	var odell = new Location(2, 40.589476m, -105.063186m, "Odell Brewing Company");
	var belgium = new Location(3, 40.593238m, -105.068600m, "New Belgium Brewing Company");
	var equinox = new Location(4, 40.586356m, -105.075812m, "Equinox Brewing");

	var locations = new[] { dragon, odell, belgium, equinox };

	var gexf = new GexfDocument();
	gexf.Meta.LastModified = DateTimeOffset.Now;
	gexf.Meta.Creator = Environment.UserName;

	gexf.Graph.IdType = GexfIdType.Integer;

	GexfId lat = "lat";
	GexfId lon = "lon";

	gexf.Graph.NodeAttributes.AddRange(
		new GexfAttribute(lat, "latitude", GexfDataType.Double),
		new GexfAttribute(lon, "longitude", GexfDataType.Double)
		);

	gexf.Graph.Nodes.AddRange(locations.Select(location =>
		new GexfNode(location.Id)
		{
			Label = location.Label,
			AttrValues =
			{
				new GexfAttributeValue(lat, location.Lat),
				new GexfAttributeValue(lon, location.Lon)
			}
		}));

	gexf.Graph.Edges.AddRange(
		new GexfEdge(1, dragon.Id, odell.Id),
		new GexfEdge(2, odell.Id, belgium.Id),
		new GexfEdge(3, belgium.Id, equinox.Id)
		);

	string path = Path.Combine(
		Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
		"breweries.gexf");

	gexf.Save(path);
}

class Location
{
    public Location(int id, decimal lat, decimal lon, string label)
    {
        Id = id;
        Lat = lat;
		Lon = lon;
		Label = label;
	}

	public int Id { get; }
	public decimal Lat { get; }
	public decimal Lon { get; }
	public string Label { get; }
}
```

### Change Log

The change log is available [here](https://github.com/fallin/gexf-dotnet/blob/master/ChangeLog.md).